using AutoMapper;
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
using Hinet.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUA,
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
                    var taiKhoan = _taiKhoanService.GetRanDomByDMGameId(id);
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
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUA,
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
        public ActionResult NapTopup(GiaoDichTopupVM model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            var EntityModel = _mapper.Map<GiaoDich>(model);
            EntityModel.LoaiGiaoDich = LoaiGiaoDichConstant.NAPTOPUP;
            EntityModel.TrangThai = TrangThaiGiaoDichConstant.CHOXULY;
            EntityModel.PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG;
            EntityModel.NgayGiaoDich = DateTime.Now;
            EntityModel.NgayThanhToan = DateTime.Now;
            EntityModel.UserId = CurrentUserId ?? 0;
            var generatedCode = GenertePopUpCode();
            EntityModel.NoiDung = generatedCode;
            _giaoDichService.Create(EntityModel);

            var configSite = _siteConfigService.GetActiveConfig();

            string qrUrl = $"https://img.vietqr.io/image/{configSite.BankCode}-{configSite.AccountNumber}-compact2.png?amount={model.SoTien}&addInfo={generatedCode}&accountName=${configSite.AccountName}";

            return Json(new
            {
                success = true,
                transactionId = EntityModel.Id,
                amount = model.SoTien,
                qrData = qrUrl,
                content = generatedCode
            });
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult NapTien(GiaoDichTopupVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

        //    var EntityModel = _mapper.Map<GiaoDich>(model);
        //    EntityModel.LoaiGiaoDich = LoaiGiaoDichConstant.NAPTOPUP;
        //    EntityModel.TrangThai = TrangThaiGiaoDichConstant.CHOXULY;
        //    EntityModel.PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG;
        //    EntityModel.NgayGiaoDich = DateTime.Now;
        //    EntityModel.NgayThanhToan = DateTime.Now;
        //    EntityModel.UserId = CurrentUserId ?? 0;
        //    var generatedCode = GenertePopUpCode();
        //    EntityModel.NoiDung = generatedCode;
        //    _giaoDichService.Create(EntityModel);

        //    var configSite = _siteConfigService.GetActiveConfig();

        //    string qrUrl = $"https://img.vietqr.io/image/{configSite.BankCode}-{configSite.AccountNumber}-compact2.png?amount={model.SoTien}&addInfo={generatedCode}&accountName=${configSite.AccountName}";

        //    return Json(new
        //    {
        //        success = true,
        //        transactionId = EntityModel.Id,
        //        amount = model.SoTien,
        //        qrData = qrUrl,
        //        content = generatedCode
        //    });
        //}


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmTopup(SePayTransaction transaction)
        {
            try
            {
                var now = DateTime.Now;
                var code = transaction.Content.Trim().ToUpper();
                var existingTrans = _giaoDichService.FindBy(x => x.NoiDung.Trim().ToUpper() == code).FirstOrDefault();
                var currentUser = existingTrans != null ? _appUserService.GetById(existingTrans.UserId) : null;
                var notification = $"[{now:dd/MM/yyyy HH:mm:ss}] THÔNG BÁO: Giao dịch nạp topup {existingTrans?.NoiDung} của {currentUser?.UserName} với mệnh giá {existingTrans?.SoTien}đ";

                // Không tìm thấy giao dịch hợp lệ
                if (existingTrans == null)
                {
                    await TelegramHelper.SendAsync($"{notification} thất bại, lý do: không tìm thấy giao dịch hợp lệ");
                    if (currentUser != null)
                    {
                        _notificationService.CreateNoti(
                            currentUser.Id,
                            "/lich-su-giao-dich",
                            $"Giao dịch không hợp lệ: Không tìm thấy mã giao dịch {code}."
                        );
                    }

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, message = "Transaction not found" }, JsonRequestBehavior.AllowGet);
                }

                // Giao dịch đã được xử lý hoặc huỷ
                if (existingTrans.TrangThai != TrangThaiGiaoDichConstant.CHOXULY)
                {
                    await TelegramHelper.SendAsync($"{notification} thất bại, lý do: giao dịch đã được xử lý hoặc bị huỷ");
                    _notificationService.CreateNoti(
                        currentUser.Id,
                        "/lich-su-giao-dich",
                        $"Giao dịch {existingTrans.NoiDung} đã được xử lý hoặc bị huỷ trước đó. Vui lòng kiểm tra lại lịch sử giao dịch."
                    );

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, message = "Transaction already processed" }, JsonRequestBehavior.AllowGet);
                }


                // Số tiền không khớp
                var tranferAmount = int.TryParse(transaction.TransferAmount.ToString(), out var res) ? res : 0;
                if (existingTrans.SoTien != tranferAmount)
                {
                    await TelegramHelper.SendAsync($"{notification} thất bại, lý do: số tiền gửi không khớp với giá trị nạp");
                    existingTrans.TrangThai = TrangThaiGiaoDichConstant.THATBAI;
                    _giaoDichService.Update(existingTrans);
                    _notificationService.CreateNoti(
                        currentUser.Id,
                        "/lich-su-giao-dich",
                        $"Giao dịch {existingTrans.NoiDung} thất bại do số tiền chuyển không khớp ({transaction.TransferAmount:N0}đ)."
                    );

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, message = "Invalid amount transaction expired" }, JsonRequestBehavior.AllowGet);
                }

                // Thành công
                existingTrans.TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN;
                _giaoDichService.Update(existingTrans);
                var userDto = _appUserService.GetDtoById(currentUser.Id);
                SessionManager.SetValue(SessionManager.USER_INFO, userDto);
                await TelegramHelper.SendAsync($"{notification} thành công");

                _notificationService.CreateNoti(
                    userDto.Id,
                    "/lich-su-giao-dich",
                    $"Nạp tiền thành công! Giao dịch {existingTrans.NoiDung} với mệnh giá {existingTrans.SoTien:N0}đ đã được cộng vào tài khoản của bạn."
                );
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                await TelegramHelper.SendAsync($"Giao dịch thất bại, lý do:Lỗi xác nhận giao dịch nạp tiền {ex}");
                _notificationService.CreateNoti(
                    CurrentUserId ?? 0,
                    "/lich-su-giao-dich",
                    $"Có lỗi xảy ra trong quá trình xác nhận giao dịch {ex}. Hệ thống đang kiểm tra, vui lòng thử lại sau."
                );
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { success = false, message = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CheckTransactionStatus(string code)
        {
            var success = _giaoDichService
                .FindBy(x => x.NoiDung.Trim().ToUpper() == code && x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                .Any();
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }
        private string GenertePopUpCode(string prefix = "PAY", int randomChars = 5)
        {
            var ts = DateTime.UtcNow.ToString("yyyyMMddHHmmss"); // dùng UTC để nhất quán
            var randomPart = RandomBase36(randomChars);
            return $"{prefix}{ts}{randomPart}";
        }

        private string RandomBase36(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var data = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }
            var sb = new StringBuilder(length);
            foreach (var b in data)
            {
                sb.Append(chars[b % chars.Length]);
            }
            return sb.ToString();
        }
    }
}