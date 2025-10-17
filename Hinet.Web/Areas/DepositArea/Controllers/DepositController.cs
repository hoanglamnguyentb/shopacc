using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DepositService;
using Hinet.Service.DepositService.Dto;
using Hinet.Service.GiaoDichService;
using Hinet.Service.NotificationService;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DepositArea.Controllers
{

    public class DepositController : BaseController
    {
        private IDepositService _depositService;
        private IAppUserService _appUserService;
        private IGiaoDichService _transactionService;
        private ILog _iLog;
        private INotificationService _notificationService;

        public DepositController(
            IDepositService depositService,
            IAppUserService appUserService,
            ILog iLog,
            IGiaoDichService transactionService,
            INotificationService notificationService)
        {
            _depositService = depositService;
            _appUserService = appUserService;
            _iLog = iLog;
            _notificationService = notificationService;
            _transactionService = transactionService;
            _notificationService = notificationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Confirmation(SePayTransaction transaction)
        {
            try
            {
                var now = DateTime.Now;
                var code = transaction.Content.Trim().ToUpper();
                var existingTrans = _depositService.FindBy(x => x.Code.ToUpper() == code).FirstOrDefault();
                var currentUser = existingTrans != null ? _appUserService.GetById(existingTrans.UserId) : null;
                var notification = $"[{now:dd/MM/yyyy HH:mm:ss}] THÔNG BÁO: Giao dịch nạp thẻ {existingTrans?.Code} của {currentUser?.UserName} với mệnh giá {existingTrans?.Amount}đ";

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
                if (existingTrans.Status != DepositConstant.PENDING)
                {
                    await TelegramHelper.SendAsync($"{notification} thất bại, lý do: giao dịch đã được xử lý hoặc bị huỷ");

                    _notificationService.CreateNoti(
                        currentUser.Id,
                        "/lich-su-giao-dich",
                        $"Giao dịch {existingTrans.Code} đã được xử lý hoặc bị huỷ trước đó. Vui lòng kiểm tra lại lịch sử giao dịch."
                    );

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, message = "Transaction already processed" }, JsonRequestBehavior.AllowGet);
                }

                // Giao dịch quá hạn
                if (existingTrans.Expiry <= now)
                {
                    await TelegramHelper.SendAsync($"{notification} thất bại, lý do: giao dịch quá hạn");
                    existingTrans.Status = DepositConstant.EXPIRED;

                    _notificationService.CreateNoti(
                        currentUser.Id,
                        "/lich-su-giao-dich",
                        $"Giao dịch {existingTrans.Code} đã quá hạn và không thể thực hiện. Vui lòng thực hiện lại giao dịch mới."
                    );

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, message = "Transaction expired" }, JsonRequestBehavior.AllowGet);
                }

                // Số tiền không khớp
                if (existingTrans.Amount != transaction.TransferAmount)
                {
                    await TelegramHelper.SendAsync($"{notification} thất bại, lý do: số tiền gửi không khớp với giá trị nạp");
                    existingTrans.Status = DepositConstant.EXPIRED;

                    _notificationService.CreateNoti(
                        currentUser.Id,
                        "/lich-su-giao-dich",
                        $"Giao dịch {existingTrans.Code} thất bại do số tiền chuyển không khớp ({transaction.TransferAmount:N0}đ). Vui lòng kiểm tra lại và nạp đúng số tiền."
                    );

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { success = false, message = "Invalid amount transaction expired" }, JsonRequestBehavior.AllowGet);
                }

                // Thành công
                existingTrans.Status = DepositConstant.SUCCESS;
                currentUser.Balance += existingTrans.Amount;
                var newTrans = new GiaoDich()
                {
                    NguoiGiaoDich = currentUser.Id,
                    DoiTuongId = 2,
                    LoaiDoiTuong = "NapTien",
                    LoaiGiaoDich = LoaiGiaoDichConstant.NAPTHUONG,
                    TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                    PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                    NgayGiaoDich = now,
                    NgayXuLy = now,
                    SoTien = (int)existingTrans.Amount,
                    NoiDung = transaction.Content
                };
                _transactionService.Create(newTrans);

                _appUserService.Update(currentUser);
                var userDto = _appUserService.GetDtoById(currentUser.Id);
                SessionManager.SetValue(SessionManager.USER_INFO, userDto);
                _depositService.Update(existingTrans);
                // Tạo thêm dữ liệu giao dịch
                var giaoDich = new GiaoDich
                {
                    NguoiGiaoDich = currentUser.Id,
                    DoiTuongId = existingTrans.Id,
                    LoaiDoiTuong = nameof(Deposit),
                    LoaiGiaoDich = LoaiGiaoDichConstant.NAPTHUONG,
                    TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                    PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                    NgayGiaoDich = now,
                    NgayXuLy = now,
                    SoTien = (int)existingTrans.Amount,
                };

                await TelegramHelper.SendAsync($"{notification} thành công");

                _notificationService.CreateNoti(
                    currentUser.Id,
                    "/lich-su-giao-dich",
                    $"Nạp tiền thành công! Giao dịch {existingTrans.Code} với mệnh giá {existingTrans.Amount:N0}đ đã được cộng vào tài khoản của bạn."
                );

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                await TelegramHelper.SendAsync($"Giao dịch thất bại, lý do:Lỗi xác nhận giao dịch nạp tiền {ex}");
                _iLog.Error("Lỗi xác nhận giao dịch nạp tiền", ex);

                // Gửi noti lỗi hệ thống cho user (nếu lấy được userId từ mã giao dịch)
                try
                {
                    var code = transaction.Content.Trim().ToUpper();
                    var trans = _depositService.FindBy(x => x.Code.ToUpper() == code).FirstOrDefault();
                    if (trans != null)
                    {
                        _notificationService.CreateNoti(
                            trans.UserId,
                            "/lich-su-giao-dich",
                            $"Có lỗi xảy ra trong quá trình xác nhận giao dịch {trans.Code}. Hệ thống đang kiểm tra, vui lòng thử lại sau."
                        );
                    }
                }
                catch { /* bỏ qua lỗi phát sinh trong catch */ }

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { success = false, message = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GenerateCode(long amount)
        {
            try
            {
                var now = DateTime.Now;
                var newDeposit = new Deposit()
                {
                    UserId = CurrentUserId.Value,
                    Code = GenertePopUpCode(),
                    Amount = amount,
                    Expiry = now.AddMinutes(10)
                };
                _depositService.Create(newDeposit);
                Response.StatusCode = (int)HttpStatusCode.Created;
                return Json(new { success = true, code = newDeposit.Code });
            }
            catch (Exception ex)
            {
                _iLog.Error("Lỗi xác nhận giao dịch nạp tiền", ex);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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