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

        public GianHangController(IGianHangService gianHangService, IVatPhamService vatPhamService, IThuocTinhGianHangService thuocTinhGianHangService, IMaGiamGiaService maGiamGiaService, IDM_DulieuDanhmucService dM_DulieuDanhmucService, IDonHangService donHangService, IDonHangGiaTriThuocTinhService donHangGiaTriThuocTinhService)
        {
            _gianHangService = gianHangService;
            _vatPhamService = vatPhamService;
            _thuocTinhGianHangService = thuocTinhGianHangService;
            _maGiamGiaService = maGiamGiaService;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _donHangService = donHangService;
            _donHangGiaTriThuocTinhService = donHangGiaTriThuocTinhService;
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
            {
                return ApiResponse.ErrorResponse("Vui lòng nhập mã giảm giá");
            }

            // Tìm mã giảm giá theo code (case-insensitive)
            var maGiam = _maGiamGiaService.FindBy(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();

            if (maGiam == null)
            {
                return ApiResponse.ErrorResponse("Mã giảm giá không tồn tại");
            }

            // 1. Trạng thái
            if (!maGiam.TrangThai)
            {
                return ApiResponse.ErrorResponse("Mã giảm giá đã bị khóa");
            }

            // 2. Thời gian áp dụng
            var now = DateTime.Now;
            if (maGiam.TuNgay.HasValue && now < maGiam.TuNgay.Value)
            {
                return ApiResponse.ErrorResponse("Mã giảm giá chưa bắt đầu áp dụng");
            }
            if (maGiam.DenNgay.HasValue && now > maGiam.DenNgay.Value)
            {
                return ApiResponse.ErrorResponse("Mã giảm giá đã hết hạn");
            }

            // 3. Số lượng còn lại
            if (maGiam.SoLuong.HasValue && maGiam.SoLuong.Value <= 0)
            {
                return ApiResponse.ErrorResponse("Mã giảm giá đã hết lượt sử dụng");
            }

            // 4. Kiểm tra phạm vi áp dụng
            if (!(maGiam.ToanHeThong ?? false))
            {
                if (string.IsNullOrEmpty(maGiam.GianHangApDung))
                {
                    return ApiResponse.ErrorResponse("Mã giảm giá không áp dụng cho gian hàng này");
                }

                // GianHangApDung có thể là chuỗi chứa nhiều ID ngăn cách bởi dấu phẩy
                var gianHangIds = maGiam.GianHangApDung
                    .Split(',')
                    .Select(id => int.Parse(id.Trim()))
                    .ToList();

                if (!gianHangIds.Contains(gianHangId))
                {
                    return ApiResponse.ErrorResponse("Mã giảm giá không áp dụng cho gian hàng này");
                }
            }

            // ✅ Nếu tất cả hợp lệ → trả thông tin về cho client
            var data = new
            {
                Id = maGiam.Id,
                Code = maGiam.Code,
                ThongTin = maGiam.ThongTin,
                KieuGiamGia = maGiam.KieuGiam,
                GiaTriGiam = maGiam.GiaTriGiam,
                // có thể trả thêm kiểu giảm giá / giá trị nếu bạn mở rộng model sau này
            };

            return ApiResponse.SuccessResponse(data, "Áp dụng mã giảm giá thành công");
        }

        [Route("~/don-hang/{id?}")]
        public ActionResult DonHang(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ThanhToan(OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            // 1. Kiểm tra gian hàng có tồn tại không
            var gianHang = _gianHangService.GetById(model.GianHangId);
            if (gianHang == null)
            {
                return Json(new { success = false, message = "Gian hàng không hợp lệ" });
            }

            // 2. Kiểm tra vật phẩm
            var vatPham = _vatPhamService.GetById(model.VatPhamId);
            if (vatPham == null)
            {
                return Json(new { success = false, message = "Vật phẩm không hợp lệ" });
            }

            // 3. Tính giá trị đơn hàng gốc
            decimal total = vatPham.GiaGoc * model.SoLuong;

            // 4. Áp dụng mã giảm giá (nếu có)
            if (model.MaGiamGiaId > 0)
            {
                var voucher = _maGiamGiaService.GetById(model.MaGiamGiaId);
                if (voucher != null && voucher.TrangThai)
                {
                    var now = DateTime.Now;
                    if (voucher.TuNgay <= now && voucher.DenNgay >= now)
                    {
                        decimal discountValue = 0;

                        if (voucher.KieuGiam == "PERCENT")
                        {
                            discountValue = total * (voucher.GiaTriGiam / 100);
                        }
                        else if (voucher.KieuGiam == "AMOUNT")
                        {
                            discountValue = voucher.GiaTriGiam;
                        }

                        total = Math.Max(total - discountValue, 0);
                    }
                }
            }

            // 5. Tạo đối tượng DonHang để lưu DB
            var donHang = new DonHang
            {
                GianHangId = model.GianHangId,
                VatPhamId = model.VatPhamId,
                SoLuong = model.SoLuong,
                TongTien = (int)total,
                MaGiamGiaId = model.MaGiamGiaId,
                TrangThai = TrangThaiDonHangConstant.KHOITAO
            };
            _donHangService.Create(donHang);

            // 2Lưu giá trị thuộc tính động
            if (model.GiaTriThuocTinhs != null && model.GiaTriThuocTinhs.Any())
            {
                var listGiaTri = new List<DonHangGiaTriThuocTinh>();
                foreach (var item in model.GiaTriThuocTinhs)
                {
                    string giaTriTxt = item.GiaTri;
                    if (item.KieuDuLieu == KieuDuLieuThuocTinhGameConstant.DROPDOWN)
                    {
                        var duLieu = int.TryParse(item.GiaTri, out var guidId) ? _dM_DulieuDanhmucService.GetById(guidId) : null;
                        giaTriTxt = duLieu != null ? duLieu.Name : giaTriTxt;
                    }

                    if (item.ThuocTinhId > 0 && !string.IsNullOrEmpty(item.GiaTri))
                    {
                        listGiaTri.Add(new DonHangGiaTriThuocTinh
                        {
                            DonHangId = donHang.Id,
                            ThuocTinhId = item.ThuocTinhId,
                            ThuocTinhTxt = item.ThuocTinhTxt,
                            GiaTri = item.GiaTri,
                            KieuDuLieu = item.KieuDuLieu,
                            GiaTriTxt = giaTriTxt,
                        });
                    }
                }

                _donHangGiaTriThuocTinhService.InsertRange(listGiaTri);
            }
            // 6. Điều hướng sang trang thanh toán (hoặc cổng thanh toán ngoài)
            return RedirectToAction("ThanhToanThanhCong", new { id = donHang.Id });
        }
    }
}