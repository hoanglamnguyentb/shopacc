using AutoMapper;
using CommonHelper.Number;
using DocumentFormat.OpenXml.Office2016.Presentation.Command;
using DocumentFormat.OpenXml.Spreadsheet;
using Hinet.Model;
using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DepositService.Dto;
using Hinet.Service.GiaoDichService;
using Hinet.Service.NotificationService;
using Hinet.Service.SiteConfigService;
using Hinet.Service.TaiKhoanService;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using Hinet.Web.HubControl;
using Hinet.Web.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    [RoutePrefix("giao-dich")]  // Đặt prefix chung
    public class GiaoDichController : EndUserController
    {
        private readonly IGiaoDichService _giaoDichService;
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IAppUserService _appUserService;
        private readonly INotificationService _notificationService;
        private readonly IDanhMucGameService _danhMucGameService;
        private readonly IMapper _mapper;
        private readonly ISiteConfigService _siteConfigService;
        private readonly static string SePayApiKey = WebConfigurationManager.AppSettings["SePayApiKey"];

        public GiaoDichController(IGiaoDichService giaoDichService, ITaiKhoanService taiKhoanService, IAppUserService appUserService, INotificationService notificationService, IDanhMucGameService danhMucGameService, IMapper mapper, ISiteConfigService siteConfigService)
        {
            _giaoDichService = giaoDichService;
            _taiKhoanService = taiKhoanService;
            _appUserService = appUserService;
            _notificationService = notificationService;
            _danhMucGameService = danhMucGameService;
            _mapper = mapper;
            _siteConfigService = siteConfigService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("mua-acc/{id?}")]
        public ActionResult MuaAcc(int id)
        {
            using (var db = new DbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var taiKhoan = _taiKhoanService.GetById(id);
                    if (taiKhoan == null || taiKhoan.TrangThai != TrangThaiTaiKhoanConstant.CHUABAN)
                    {
                        return Json(new { success = false, message = "Tài khoản không tồn tại hoặc đã được bán." });
                    }

                    var giaBan = taiKhoan.GiaKhuyenMai ?? taiKhoan.GiaGoc;
                    if (CurrentUserInfo.Balance < giaBan)
                    {
                        return Json(new { success = false, message = "Số dư của bạn không đủ để mua tài khoản này." });
                    }

                    var user = _appUserService.FindBy(x => x.Id == CurrentUserId).FirstOrDefault();
                    if (user == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                    }
                    user.Balance -= giaBan;
                    _appUserService.Update(user);
                    var userDto = _appUserService.GetDtoById(user.Id);
                    SessionManager.SetValue(SessionManager.USER_INFO, userDto);

                    taiKhoan.TrangThai = TrangThaiTaiKhoanConstant.DABAN;
                    _taiKhoanService.Update(taiKhoan);

                    var giaoDich = new GiaoDich
                    {
                        UserId = CurrentUserId.GetValueOrDefault(),
                        DoiTuongId = id,
                        LoaiDoiTuong = nameof(TaiKhoan),
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUAACC,
                        TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                        PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                        NgayGiaoDich = DateTime.Now,
                        NgayThanhToan = DateTime.Now,
                        SoTien = -giaBan,
                        NoiDung = $"Mua tài khoản {taiKhoan.Code}",
                    };
                    _giaoDichService.Create(giaoDich);
                    _notificationService.CreateNoti(
                        CurrentUserId.GetValueOrDefault(),
                        $"/acc/{taiKhoan.Code}",
                        $"Mua tài khoản {taiKhoan.Code} thành công!"
                    );
                    transaction.Commit();

                    var message = $"Người dùng {CurrentUserInfo.FullName} #{CurrentUserId} đã thanh toán thành công tài khoản {taiKhoan.Id}\nXem chi tiết [tại đây](https://example.com/chi-tiet/{taiKhoan.Id})";
                    TelegramHelper.SendAsync($"Người dùng {CurrentUserInfo.FullName} #{CurrentUserId} đã thanh toán thành công tài khoản {taiKhoan.Id}");
                    return Json(new { success = true, message = "Thanh toán thành công!" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Đã xảy ra lỗi trong quá trình thanh toán: " + ex.Message });
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MuaAccRandom(int id) //Id của danh mục game
        {
            using (var db = new DbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var taiKhoan = _taiKhoanService.GetRandomByDMGameId(id);
                    if (taiKhoan == null || taiKhoan.TrangThai != TrangThaiTaiKhoanConstant.CHUABAN)
                    {
                        return Json(new { success = false, message = "Tài khoản không tồn tại hoặc đã được bán." });
                    }
                    var danhMucGame = _danhMucGameService.GetById(taiKhoan.DanhMucGameId);

                    var giaBan = danhMucGame.GiaKhuyenMai ?? danhMucGame.GiaGoc;
                    if (CurrentUserInfo.Balance < giaBan)
                    {
                        return Json(new { success = false, message = "Số dư của bạn không đủ để mua tài khoản này." });
                    }

                    var user = _appUserService.FindBy(x => x.Id == CurrentUserId).FirstOrDefault();
                    if (user == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                    }
                    user.Balance -= giaBan;
                    _appUserService.Update(user);
                    var userDto = _appUserService.GetDtoById(user.Id);
                    SessionManager.SetValue(SessionManager.USER_INFO, userDto);

                    taiKhoan.TrangThai = TrangThaiTaiKhoanConstant.DABAN;
                    _taiKhoanService.Update(taiKhoan);

                    var giaoDich = new GiaoDich
                    {
                        UserId = CurrentUserId.GetValueOrDefault(),
                        DoiTuongId = id,
                        LoaiDoiTuong = nameof(TaiKhoan),
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUAACC,
                        TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                        PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                        NgayGiaoDich = DateTime.Now,
                        NgayThanhToan = DateTime.Now,
                        SoTien = -giaBan,
                        NoiDung = $"Mua tài khoản {taiKhoan.Code}",
                    };
                    _giaoDichService.Create(giaoDich);
                    _notificationService.CreateNoti(
                        CurrentUserId.GetValueOrDefault(),
                        $"/acc/{taiKhoan.Code}",
                        $"Mua tài khoản {taiKhoan.Code} thành công!"
                    );
                    transaction.Commit();

                    TelegramHelper.SendAsync($"Người dùng {CurrentUserInfo.FullName} #{CurrentUserId} đã thanh toán thành công tài khoản {taiKhoan.Id}");
                    return Json(new { success = true, message = "Thanh toán thành công!" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Đã xảy ra lỗi trong quá trình thanh toán: " + ex.Message });
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> TaoGiaoDichVaQRCode(GiaoDich giaoDich)
        {
            try
            {
                var kiemTraResult = KiemTraGiaoDich(giaoDich);
                if(kiemTraResult.Status == false)
                {
                    return ApiResponse.ErrorResponse("Tạo giao dịch thất bại", kiemTraResult.Message);
                }

                var siteConfig = _siteConfigService.GetActiveConfig();
                var newGiaoDich = new GiaoDich
                {
                    UserId = CurrentUserId.GetValueOrDefault(),
                    DoiTuongId = giaoDich.DoiTuongId,
                    LoaiDoiTuong = giaoDich.LoaiDoiTuong,
                    LoaiGiaoDich = giaoDich.LoaiGiaoDich,
                    TrangThai = "KHOITAO",
                    PhuongThucThanhToan = giaoDich.PhuongThucThanhToan,
                    NgayGiaoDich = DateTime.Now,
                    SoTien = giaoDich.SoTien,
                    TenTaiKhoanCanNap = giaoDich.TenTaiKhoanCanNap,
                    MatKhauTaiKhoanNap = giaoDich.MatKhauTaiKhoanNap,
                };
                _giaoDichService.Create(newGiaoDich);
                var maGiaoDich = $"{giaoDich.LoaiGiaoDich}_{newGiaoDich.Id}_{CurrentUserId}";
                newGiaoDich.MaGiaoDich = maGiaoDich;
                newGiaoDich.NoiDungChuyenKhoan = maGiaoDich;
                _giaoDichService.Update(newGiaoDich);
                var qrUrl = $"https://img.vietqr.io/image/{siteConfig.BankCode}-{siteConfig.AccountNumber}-compact2.png?amount={newGiaoDich.SoTien}&addInfo={HttpUtility.UrlEncode(newGiaoDich.NoiDungChuyenKhoan)}&accountName={HttpUtility.UrlEncode(siteConfig.AccountName)}";
                var giaoDichVM = new GiaoDichVM
                {
                    NoiDungChuyenKhoan = newGiaoDich.NoiDungChuyenKhoan,
                    SoTien = newGiaoDich.SoTien,
                    QrUrl = qrUrl
                };

                return ApiResponse.SuccessResponse(giaoDichVM, "Tạo giao dịch thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse.ErrorResponse("Tạo giao dịch thất bại", ex.Message);
            }
        }


        [HttpDelete]
        public async Task HuyGiaoDich(string maGiaoDich)
        {
            var giaoDich = _giaoDichService.FindBy(x => x.MaGiaoDich == maGiaoDich).FirstOrDefault();
            if(giaoDich != null && giaoDich.TrangThai == TrangThaiGiaoDichConstant.KHOITAO)
            {
                _giaoDichService.Delete(giaoDich);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SePayCallback(SePayTransaction transaction)
        {
            try
            {
                string apiKeyInHeader = Request.Headers["Authorization"];
                var expectedPrefix = "Apikey ";
                if (string.IsNullOrEmpty(apiKeyInHeader) || !apiKeyInHeader.StartsWith(expectedPrefix))
                {
                    return Json(new { success = false, message = "Thiếu hoặc sai định dạng API Key" });
                }
                string apiKey = apiKeyInHeader.Substring(expectedPrefix.Length);
                if (apiKey != SePayApiKey)
                {
                    return ApiResponse.ErrorResponse("API Key không hợp lệ");
                }

                if (transaction == null || string.IsNullOrEmpty(transaction.Content))
                {
                    return ApiResponse.ErrorResponse("Dữ liệu không hợp lệ");
                }

                var code = transaction.Content.Trim().ToUpper();
                var giaoDich = _giaoDichService.FindBy(x => x.NoiDungChuyenKhoan.Trim().ToUpper() == code).FirstOrDefault();

                if (giaoDich == null)
                {
                    return ApiResponse.ErrorResponse("Không tìm thấy giao dịch");
                }

                // Kiểm tra trạng thái thành công từ SePay (giả sử transaction.Status == "SUCCESS")
                if (giaoDich.TrangThai == TrangThaiGiaoDichConstant.KHOITAO)
                {
                    giaoDich.TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN;
                    giaoDich.NgayThanhToan = DateTime.Now;
                    giaoDich.MaGiaoDichDoiTac = transaction.Id.ToString();
                    giaoDich.SoTien = (int)transaction.TransferAmount;
                    _giaoDichService.Update(giaoDich);

                    XuLySauKhiThanhToan(giaoDich);

                    return ApiResponse.SuccessResponse(null, "Giao dịch thành công");
                }
                else
                {
                    return ApiResponse.ErrorResponse("Trạng thái giao dịch không hợp lệ hoặc đã xử lý");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ErrorResponse("Lỗi xử lý", ex.Message);
            }
        }

        public void XuLySauKhiThanhToan(GiaoDich giaoDich)
        {
            var code = giaoDich.NoiDungChuyenKhoan;
            if (string.IsNullOrWhiteSpace(code)) return;
            var message = "";
            var url = "";

            var parts = code.Split('_');
            var loaiGiaoDich = parts[0];
            var id = parts.Length > 1 ? int.Parse(parts[1]) : 0; //Id đối tượng
            var userId = parts.Length > 2 ? int.Parse(parts[2]) : 0;

            if (loaiGiaoDich.Equals(LoaiGiaoDichConstant.MUAACCRANDOM, StringComparison.OrdinalIgnoreCase))
            {
                // Cập nhật trạng thái tài khoản random
                var taiKhoan = _taiKhoanService.GetRandomByDMGameId(id);
                if (taiKhoan != null)
                {
                    taiKhoan.TrangThai = TrangThaiTaiKhoanConstant.DABAN;
                    _taiKhoanService.Update(taiKhoan);

                    // Tạo giao dịch mua acc random cho user
                    var newGiaoDich = new GiaoDich
                    {
                        UserId = userId,
                        DoiTuongId = taiKhoan.Id,
                        LoaiDoiTuong = nameof(TaiKhoan),
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUAACC,
                        TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                        PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                        NgayGiaoDich = DateTime.Now,
                        NgayThanhToan = DateTime.Now,
                        SoTien = giaoDich.SoTien,
                        NoiDung = $"Mua tài khoản ngẫu nhiên #DM_{giaoDich.DoiTuongId} #TK_CODE_{taiKhoan.Code}",
                    };
                    _giaoDichService.Create(newGiaoDich);
                }
                message = $"🎉 Giao dịch thành công! Bạn đã sở hữu tài khoản <strong>#{taiKhoan.Code}</strong>. Hãy kiểm tra ngay nhé.";
                url = $"/acc/{taiKhoan.Code}";
            }
            else if (loaiGiaoDich.Equals(LoaiGiaoDichConstant.MUAACC, StringComparison.OrdinalIgnoreCase))
            {
                // Mua acc bình thường
                var taiKhoan = _taiKhoanService.GetById(id);
                if (taiKhoan != null)
                {
                    taiKhoan.TrangThai = TrangThaiTaiKhoanConstant.DABAN;
                    _taiKhoanService.Update(taiKhoan);
                }
                message = $"🎉 Thanh toán thành công! Tài khoản <strong>#{taiKhoan.Code}</strong> đã thuộc về bạn";
                url = $"/acc/{taiKhoan.Code}";
            }
            else if (loaiGiaoDich.Equals(LoaiGiaoDichConstant.NAPTHUONG, StringComparison.OrdinalIgnoreCase))
            {
                // Nạp tiền
                var user = _appUserService.GetById(userId);
                if (user != null)
                {
                    user.Balance += giaoDich.SoTien;
                    _appUserService.Update(user);
                }
                message = $"💰 Nạp tiền thành công! Tài khoản của bạn đã được cộng <strong>+{NumberHelper.FormatNumberVN(giaoDich.SoTien)} VNĐ</strong>";
                url = "/lich-su-giao-dich";
            }
            giaoDich.TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN;
            _giaoDichService.Update(giaoDich);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.Group(userId.ToString()).receiveNotification(new
            {
                message = message,
                url = url,
            });
            _notificationService.CreateNoti(userId, url,message);
            SendTelegramMessage(giaoDich);
        }

        [HttpGet]
        public ActionResult CheckTransactionStatus(string code)
        {
            var success = _giaoDichService
                .FindBy(x => x.NoiDung.Trim().ToUpper() == code && x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                .Any();
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        private async Task SendTelegramMessage(GiaoDich giaoDich)
        {
            var baseInfo = $"- Giao dịch ID: {giaoDich.Id}\n" +
                           $"- UserId: {giaoDich.UserId}\n" +
                           $"- Số tiền: {giaoDich.SoTien:N0} VNĐ\n" +
                           $"- Nội dung chuyển khoản: {giaoDich.NoiDungChuyenKhoan}\n" +
                           $"- Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                           $"- Mã giao dịch đối tác: {giaoDich.MaGiaoDichDoiTac}";

            string message = "";

            if (giaoDich.LoaiGiaoDich.Equals(LoaiGiaoDichConstant.MUAACC, StringComparison.OrdinalIgnoreCase) ||
                giaoDich.LoaiGiaoDich.Equals(LoaiGiaoDichConstant.MUAACCRANDOM, StringComparison.OrdinalIgnoreCase))
            {
                message = $"[ADMIN THÔNG BÁO]\n" +
                          $"💎 MUA ACC thành công!\n" +
                          $"- Loại: {(giaoDich.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACCRANDOM ? "Random" : "Mua ngay")}\n"
                          + baseInfo;
            }
            else if (giaoDich.LoaiGiaoDich.Equals(LoaiGiaoDichConstant.NAPTHUONG, StringComparison.OrdinalIgnoreCase))
            {
                message = $"[ADMIN THÔNG BÁO]\n" +
                          $"💰 NẠP TIỀN thành công!\n"
                          + baseInfo;
            }
            else
            {
                message = $"[ADMIN THÔNG BÁO]\n" +
                          $"🔔 Giao dịch khác được thực hiện\n"
                          + baseInfo;
            }

            // Gọi helper thực tế để gửi telegram
            await SendTelegramMessageAsync(message);
        }

        private async Task SendTelegramMessageAsync(string message)
        {
            try
            {
                var siteConfig = _siteConfigService.GetTelegramInfo();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                using (var httpClient = new HttpClient())
                {
                    var encodedMsg = System.Net.WebUtility.UrlEncode(message);
                    var url = $"https://api.telegram.org/bot{siteConfig.TelegramBotToken}/sendMessage?chat_id={siteConfig.TelegramChatId}&text={encodedMsg}";
                    await httpClient.GetAsync(url);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public GiaoDichCheckerResult KiemTraGiaoDich(GiaoDich giaoDich)
        {
            var loaiGiaoDich = giaoDich.LoaiGiaoDich;

            // Mua acc random
            if (loaiGiaoDich.Equals(LoaiGiaoDichConstant.MUAACCRANDOM, StringComparison.OrdinalIgnoreCase))
            {
                var taiKhoan = _taiKhoanService.GetRandomByDMGameId(giaoDich.DoiTuongId);
                if (taiKhoan == null)
                {
                    return GiaoDichCheckerResult.Error(
                        "⚠️ Rất tiếc, hiện tại không còn tài khoản phù hợp. Vui lòng thử lại sau hoặc chọn một danh mục khác nhé."
                    );
                }
            }
            // Mua acc cụ thể
            else if (loaiGiaoDich.Equals(LoaiGiaoDichConstant.MUAACC, StringComparison.OrdinalIgnoreCase))
            {
                var taiKhoan = _taiKhoanService.GetById(giaoDich.DoiTuongId);
                if (taiKhoan == null)
                {
                    return GiaoDichCheckerResult.Error(
                        "❌ Tài khoản bạn chọn không tồn tại hoặc đã bị gỡ. Vui lòng kiểm tra lại hoặc chọn tài khoản khác."
                    );
                }
                if (taiKhoan.TrangThai == TrangThaiTaiKhoanConstant.DABAN)
                {
                    return GiaoDichCheckerResult.Error(
                        $"⚠️ Rất tiếc, tài khoản <b>#{taiKhoan.Code}</b> đã được người khác mua trước. Vui lòng chọn một tài khoản khác nhé."
                    );
                }
            }

            return GiaoDichCheckerResult.Success();
        }


        [AllowAnonymous]
        public void TEST()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.Group("10").receiveNotification(new
            {
                message = "HELLO",
                url = "URL",
            });
        }

    }

    public class GiaoDichCheckerResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public GiaoDichCheckerResult(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public static GiaoDichCheckerResult Success(string msg = "Giao dịch hợp lệ")
            => new GiaoDichCheckerResult(true, msg);
        public static GiaoDichCheckerResult Error(string msg)
            => new GiaoDichCheckerResult(false, msg);
    }
}