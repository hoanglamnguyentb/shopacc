using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DonHangGiaTriThuocTinhService;
using Hinet.Service.DonHangService;
using Hinet.Service.GianHangService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.MaGiamGiaService;
using Hinet.Service.NotificationService;
using Hinet.Service.SiteConfigService;
using Hinet.Service.TelegramService;
using Hinet.Service.ThuocTinhGianHangService;
using Hinet.Service.VatPhamService;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using Hinet.Web.Models.GianHangVM;
using System;
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
        private readonly IGiaoDichService _giaoDichService;
        private readonly IAppUserService _appUserService;
        private readonly INotificationService _notificationService;
        private readonly ITelegramService _telegramService;

        public GianHangController(IGianHangService gianHangService,
            IVatPhamService vatPhamService, IThuocTinhGianHangService thuocTinhGianHangService,
            IMaGiamGiaService maGiamGiaService, IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IDonHangService donHangService, IDonHangGiaTriThuocTinhService donHangGiaTriThuocTinhService,
            ISiteConfigService siteConfigService, IGiaoDichService giaoDichService,
            IAppUserService appUserService, INotificationService notificationService, ITelegramService telegramService)
        {
            _gianHangService = gianHangService;
            _vatPhamService = vatPhamService;
            _thuocTinhGianHangService = thuocTinhGianHangService;
            _maGiamGiaService = maGiamGiaService;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _donHangService = donHangService;
            _donHangGiaTriThuocTinhService = donHangGiaTriThuocTinhService;
            _siteConfigService = siteConfigService;
            _giaoDichService = giaoDichService;
            _appUserService = appUserService;
            _notificationService = notificationService;
            _telegramService = telegramService;
        }

        // GET: Game
        [AllowAnonymous]
        [Route("{slug?}")]
        public ActionResult Index(string slug = null)
        {
            RefreshSession();
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
            RefreshSession();
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
            };
            _donHangService.Create(donHang);
            var maGiaoDich = $"{LoaiGiaoDichConstant.NAPTOPUP}W{donHang.Id}W{CurrentUserId}";
            donHang.MaGiaoDich = maGiaoDich;
            donHang.NoiDungChuyenKhoan = maGiaoDich;
            donHang.QrUrl = $"https://img.vietqr.io/image/{siteConfig.BankCode}-{siteConfig.AccountNumber}-qr_only.png?amount={donHang.TongTien}&addInfo={HttpUtility.UrlEncode(donHang.MaGiaoDich)}&accountName={HttpUtility.UrlEncode(siteConfig.AccountName)}";
            _donHangService.Update(donHang);

            //Lưu giao dịch
            var newGiaoDich = new GiaoDich
            {
                NguoiGiaoDich = CurrentUserId.GetValueOrDefault(),
                DoiTuongId = donHang.Id,
                LoaiDoiTuong = LoaiDoiTuongConstant.NAPTOPUP,
                LoaiGiaoDich = LoaiGiaoDichConstant.NAPTOPUP,
                TrangThai = TrangThaiGiaoDichConstant.KHOITAO,
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                NgayGiaoDich = DateTime.Now,
                SoTien = donHang.TongTien,
                MaGiaoDich = donHang.MaGiaoDich,
                NoiDungChuyenKhoan = donHang.MaGiaoDich,
            };
            _giaoDichService.Create(newGiaoDich);

            return Redirect("/don-hang/" + donHang.Id);
        }

        [HttpPost]
        public ActionResult ThanhToanDonHang(int donHangId)
        {
            var donHang = _donHangService.GetById(donHangId);
            if (donHang == null)
            {
                return ApiResponse.ErrorResponse("Không tìm thấy đơn hàng. Vui lòng thử lại.");
            }
            //Xử lý đơn hàng và giao dịch
            var user = _appUserService.GetById(CurrentUserId);
            if (user.Balance < donHang.TongTien)
            {
                return ApiResponse.ErrorResponse("Số dư của bạn không đủ để thanh toán đơn hàng này.");
            }
            //Trừ tiền
            user.Balance -= donHang.TongTien;
            _appUserService.Update(user);
            donHang.TrangThai = TrangThaiDonHangConstant.DATHANHTOAN;
            _donHangService.Update(donHang);
            var giaoDich = new GiaoDich
            {
                NguoiGiaoDich = CurrentUserId.GetValueOrDefault(),
                DoiTuongId = donHangId,
                LoaiDoiTuong = nameof(DonHang),
                LoaiGiaoDich = LoaiGiaoDichConstant.NAPTOPUP,
                TrangThai = TrangThaiGiaoDichConstant.CHOXULY,
                PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                NgayGiaoDich = DateTime.Now,
                NgayXuLy = DateTime.Now,
                SoTien = -donHang.TongTien,
                NoiDung = donHang.MaGiaoDich,
                NoiDungChuyenKhoan = donHang.MaGiaoDich,
                MaGiaoDich = donHang.MaGiaoDich,
                MaGiaoDichDoiTac = $"USER_ID#{CurrentUserId.ToString()}",
            };
            _giaoDichService.Create(giaoDich);
            var notiMessage = $"📌 Đơn hàng <strong>#{donHang.MaGiaoDich}</strong> đã được thanh toán thành công và đang trong quá trình xử lý " +
                             "Hệ thống sẽ hoàn tất trong ít phút, vui lòng kiểm tra trạng thái trong lịch sử giao dịch.";
            _notificationService.CreateNoti(
                CurrentUserId.GetValueOrDefault(),
                $"/don-hang/{donHang.Id}",
                notiMessage
            );
            _telegramService.SendTelegramMessage(giaoDich);
            return ApiResponse.SuccessResponse($"Thanh toán đơn hàng nạp topup #{donHang.Id} thành công!");
        }

        [HttpDelete]
        public async Task HuyDonHang(int donHangId)
        {
            //Xóa đon  hàng
            var donHang = _donHangService.FindBy(x => x.Id == donHangId).FirstOrDefault();
            if (donHang != null && donHang.TrangThai == TrangThaiGiaoDichConstant.KHOITAO)
            {
                //Xóa giao dịch
                var giaoDich = _giaoDichService.FindBy(x => x.MaGiaoDich == donHang.MaGiaoDich).FirstOrDefault();
                if (giaoDich != null)
                {
                    _giaoDichService.Delete(giaoDich);
                }
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

            if (model.VatPhamId > 0)
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