using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Hinet.Web.Common
{
    
    public class TelegramHelper
    {
        private readonly static string BOT_TOKEN = WebConfigurationManager.AppSettings["Telegram_BotToken"];
        private readonly static string CHAT_ID = WebConfigurationManager.AppSettings["Telegram_ChatId"];
        public  async static Task<HttpResponseMessage> SendAsync(string message)
        {
            // Bắt buộc kích hoạt TLS 1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (var httpClient = new HttpClient())
            {
                // Encode message để tránh lỗi ký tự đặc biệt
                var encodedMessage = Uri.EscapeDataString(message);
                var url = $"https://api.telegram.org/bot{BOT_TOKEN}/sendMessage?chat_id={CHAT_ID}&text={encodedMessage}";
                return await httpClient.GetAsync(url);
            }
        }
    }
}