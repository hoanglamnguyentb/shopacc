using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class NotificationTypeConstant
    {
        [DisplayName("Hệ thống")]
        public static string System { get; set; } = "System";

        [DisplayName("Tất cả")]
        public static string Global { get; set; } = "Global";
    }
}