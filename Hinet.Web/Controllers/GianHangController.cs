using DocumentFormat.OpenXml.Office2010.Excel;
using Hinet.Model;
using Hinet.Model.Entities;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.DanhMucGameTaiKhoanService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DonHangGiaTriThuocTinhService;
using Hinet.Service.DonHangService;
using Hinet.Service.GameService;
using Hinet.Service.GianHangService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.MaGiamGiaService;
using Hinet.Service.SiteConfigService;
using Hinet.Service.TaiKhoanService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.ThuocTinhGianHangService;
using Hinet.Service.VatPhamService;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using Hinet.Web.Models.GianHangVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web.Controllers
{
    [RoutePrefix("gian-hang")]  // Đặt prefix chung
    public class GianHangController : EndUserController
    {
        private readonly IGianHangService _gianHangService;
        private readonly IVatPhamService _vatPhamService;
        private readonly IThuocTinhGianHangService _thuocTinhGianHangService;
        private readonly IMaGiamGiaService _maGiamGiaService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IDonHangService _donHangService;
        private readonly IDonHangGiaTriThuocTinhService _donHangGiaTriThuocTinhService;
        private readonly ISiteConfigService _siteConfigService;

        public GianHangController(IGianHangService gianHangService, IVatPhamService vatPhamService, IThuocTinhGianHangService thuocTinhGianHangService, IMaGiamGiaService maGiamGiaService, IDM_DulieuDanhmucService dM_DulieuDanhmucService, IDonHangService donHangService, IDonHangGiaTriThuocTinhService donHangGiaTriThuocTinhService, ISiteConfigService siteConfigService)
        {
            _gianHangService = gianHangService;
            _vatPhamService = vatPhamService;
            _thuocTinhGianHangService = thuocTinhGianHangService;
            _maGiamGiaService = maGiamGiaService;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _donHangService = donHangService;
            _donHangGiaTriThuocTinhService = donHangGiaTriThuocTinhService;
            _siteConfigService = siteConfigService;
        }

        // GET: Game
        [AllowAnonymous]
        [Route("{slug?}")]
        public ActionResult Index(string slug = null)
        {
            var gianHang = _gianHangService.FindBy(x => x.Slug == slug).FirstOrDefault();
            var vm = new IndexVM
            {
                GianHang = gianHang,
                ListVatPhat = _vatPhamService.FindBy(x => x.GianHangId == gianHang.Id).ToList(),
                ThuocTinhs = _thuocTinhGianHangService.GetDaTaByGianHangId(gianHang.Id),
            };
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CheckMaGiamGia(string code, int gianHangId)
        {
            if (string.IsNullOrWhiteSpace(code))
                return ApiResponse.ErrorResponse("Vui lòng nhập mã giảm giá");

            var maGiam = _maGiamGiaService.FindBy(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
            if (maGiam == null)
                return ApiResponse.ErrorResponse("Mã giảm giá không tồn tại");

            var fakeModel = new OrderCreateVM
            {
                GianHangId = gianHangId,
                MaGiamGiaId = maGiam.Id,
                VatPhamId = 0, // nếu cần có thể bỏ qua check vật phẩm
                SoLuong = 1
            };

            var kiemTra = KiemTraDonHang(fakeModel);
            if (!kiemTra.Status)
                return ApiResponse.ErrorResponse(kiemTra.Message);

            var data = new
            {
                Id = maGiam.Id,
                Code = maGiam.Code,
                ThongTin = maGiam.ThongTin,
                KieuGiamGia = maGiam.KieuGiam,
                GiaTriGiam = maGiam.GiaTriGiam,
            };

            return ApiResponse.SuccessResponse(data, "Áp dụng mã giảm giá thành công");
        }

        [Route("~/don-hang/{id?}")]
        public ActionResult DonHang(long id)
        {
            var siteConfig = _siteConfigService.GetActiveConfig();
            var donHang = _donHangService.GetDtoById(id);
            return View(donHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ThanhToan(OrderCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var kiemTra = KiemTraDonHang(model);
            if (!kiemTra.Status)
            {
                return ApiResponse.ErrorResponse(kiemTra.Message);
            }

            var siteConfig = _siteConfigService.GetActiveConfig();
            var vatPham = _vatPhamService.GetById(model.VatPhamId);
            decimal total = vatPham.GiaGoc * model.SoLuong;

            // Áp dụng mã giảm giá (nếu có)
            if (model.MaGiamGiaId > 0)
            {
                var voucher = _maGiamGiaService.GetById(model.MaGiamGiaId);
                if (voucher != null)
                {
                    decimal discountValue = 0;
                    if (voucher.KieuGiam == KieuGiamGiaConstant.PERCENT)
                        discountValue = total * (voucher.GiaTriGiam / 100);
                    else if (voucher.KieuGiam == KieuGiamGiaConstant.AMOUNT)
                        discountValue = voucher.GiaTriGiam;

                    total = Math.Max(total - discountValue, 0);
                }
            }

            var donHang = new DonHang
            {
                GianHangId = model.GianHangId,
                VatPhamId = model.VatPhamId,
                SoLuong = model.SoLuong,
                TongTien = (int)total,
                MaGiamGiaId = model.MaGiamGiaId,
                TrangThai = TrangThaiDonHangConstant.KHOITAO,
                GiaGoc = vatPham.GiaGoc * model.SoLuong,
                GiaKhuyenMai = (int)total,
                NoiDungChuyenKhoan = $"Thanh toán đơn hàng {Guid.NewGuid().ToString().Substring(0, 8)}",
            };
            _donHangService.Create(donHang);

            donHang.MaGiaoDich = $"{LoaiGiaoDichConstant.NAPTOPUP}W{donHang.Id}W{CurrentUserId}";
            donHang.QrUrl = $"https://img.vietqr.io/image/{siteConfig.BankCode}-{siteConfig.AccountNumber}-qr_only.png?amount={donHang.TongTien}&addInfo={HttpUtility.UrlEncode(donHang.MaGiaoDich)}&accountName={HttpUtility.UrlEncode(siteConfig.AccountName)}";
            _donHangService.Update(donHang);

            //Lưu giao dịch
            var newGiaoDich = new GiaoDich
            {
                UserId = CurrentUserId.GetValueOrDefault(),
                DoiTuongId = donHang.Id,
                LoaiDoiTuong = LoaiDoiTuongConstant.NAPTOPUP,
                LoaiGiaoDich = LoaiGiaoDichConstant.NAPTOPUP,
                TrangThai = TrangThaiGiaoDichConstant.KHOITAO,
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                NgayGiaoDich = DateTime.Now,
                SoTien = donHang.TongTien,
            };

            return Redirect("/don-hang/" + donHang.Id);
        }


        [HttpDelete]
        public async Task HuyDonHang(int donHangId)
        {
            var donHang = _donHangService.FindBy(x => x.Id == donHangId).FirstOrDefault();
            if (donHang != null && donHang.TrangThai == TrangThaiGiaoDichConstant.KHOITAO)
            {
                _donHangService.Delete(donHang);
            }
        }

        public CheckerResult KiemTraDonHang(OrderCreateVM model)
        {
            // 1. Gian hàng
            var gianHang = _gianHangService.GetById(model.GianHangId);
            if (gianHang == null)
            {
                return CheckerResult.Error("Gian hàng không hợp lệ");
            }

            if(model.VatPhamId > 0)
            {
                // 2. Vật phẩm
                var vatPham = _vatPhamService.GetById(model.VatPhamId);
                if (vatPham == null)
                {
                    return CheckerResult.Error("Vật phẩm không hợp lệ");
                }
            }

            // 3. Mã giảm giá
            if (model.MaGiamGiaId > 0)
            {
                var maGiam = _maGiamGiaService.GetById(model.MaGiamGiaId);
                if (maGiam == null)
                {
                    return CheckerResult.Error("Mã giảm giá không tồn tại");
                }

                if (!maGiam.TrangThai)
                {
                    return CheckerResult.Error("Mã giảm giá đã bị khóa");
                }

                var now = DateTime.Now;
                if (maGiam.TuNgay.HasValue && now < maGiam.TuNgay.Value)
                {
                    return CheckerResult.Error("Mã giảm giá chưa bắt đầu áp dụng");
                }
                if (maGiam.DenNgay.HasValue && now > maGiam.DenNgay.Value)
                {
                    return CheckerResult.Error("Mã giảm giá đã hết hạn");
                }

                if (maGiam.SoLuong.HasValue && maGiam.SoLuong.Value <= 0)
                {
                    return CheckerResult.Error("Mã giảm giá đã hết lượt sử dụng");
                }

                if (!(maGiam.ToanHeThong ?? false))
                {
                    if (string.IsNullOrEmpty(maGiam.GianHangApDung))
                    {
                        return CheckerResult.Error("Mã giảm giá không áp dụng cho gian hàng này");
                    }

                    var gianHangIds = maGiam.GianHangApDung
                        .Split(',')
                        .Select(id => int.Parse(id.Trim()))
                        .ToList();

                    if (!gianHangIds.Contains(model.GianHangId))
                    {
                        return CheckerResult.Error("Mã giảm giá không áp dụng cho gian hàng này");
                    }
                }
            }

            return CheckerResult.Success();
        }
    }
}