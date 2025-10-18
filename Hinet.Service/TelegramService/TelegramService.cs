using AutoMapper;
using CommonHelper.Number;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.RoleRepository;
using Hinet.Repository.UserRoleRepository;
using Hinet.Service.Constant;
using Hinet.Service.NotificationService;
using Hinet.Service.SiteConfigService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hinet.Service.TelegramService
{
    public class TelegramService : ITelegramService
    {
        private readonly ISiteConfigService _siteConfigService;
        private readonly INotificationService _notificationService;
        private static readonly HttpClient _httpClient = new HttpClient();
        ILog _loger;

        public TelegramService(ISiteConfigService siteConfigService, INotificationService notificationService, ILog loger)
        {
            _siteConfigService = siteConfigService;
            _notificationService = notificationService;
            _loger = loger;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }

        public async Task SendTelegramMessage(GiaoDich giaoDich)
        {
            var baseInfo = $"- Giao dịch ID: {giaoDich.Id}\n" +
                           $"- UserId: {giaoDich.NguoiGiaoDich}\n" +
                           $"- Số tiền: {NumberHelper.FormatMoneyVN(giaoDich.SoTien)}\n" +
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
            else if (giaoDich.LoaiGiaoDich.Equals(LoaiGiaoDichConstant.NAPTOPUP, StringComparison.OrdinalIgnoreCase))
            {
                if(giaoDich.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                {
                    message = $"[ADMIN THÔNG BÁO]\n" +
                              $"⚡ GIAO DỊCH NẠP TOPUP\n" +
                              $"🟢 Trạng thái: Đã thanh toán & xử lý thành công\n" +
                              $"{baseInfo}";
                } else
                {
                    message = $"[ADMIN THÔNG BÁO]\n" +
                              $"⚡ GIAO DỊCH NẠP TOPUP\n" +
                              $"🕒 Trạng thái: Đang chờ xử lý\n" +
                              $"{baseInfo}";
                }
            }
            else
            {
                message = $"[ADMIN THÔNG BÁO]\n" +
                          $"🔔 Giao dịch khác được thực hiện\n"
                          + baseInfo;
            }

            await SendTelegramMessageAsync(message);
        }

        private async Task SendTelegramMessageAsync(string message)
        {
            try
            {
                var siteConfig = _siteConfigService.GetTelegramInfo();
                if (siteConfig == null || string.IsNullOrEmpty(siteConfig.TelegramBotToken) || string.IsNullOrEmpty(siteConfig.TelegramChatId))
                {
                    _loger.Warn("Thiếu cấu hình Telegram, không thể gửi tin nhắn.");
                    return;
                }

                var encodedMsg = System.Net.WebUtility.UrlEncode(message);
                var url = $"https://api.telegram.org/bot{siteConfig.TelegramBotToken}/sendMessage" +
                          $"?chat_id={siteConfig.TelegramChatId}&text={encodedMsg}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _loger.Error($"Gửi Telegram thất bại. Status: {response.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                _loger.Error("Lỗi khi gửi Telegram", ex);
            }
        }

    }
}