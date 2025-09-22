using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLThongBaoConstant
    {
        [DisplayName("Thông báo nội bộ")]
        [Color(BgColor = "#3498db")]
        public static string System { get; set; } = "System";

        [DisplayName("Thông báo hệ thống")]
        [Color(BgColor = "#2ecc71")]
        public static string Global { get; set; } = "Global";

        [DisplayName("Thông báo đặc biệt")]
        [Color(BgColor = "#e74c3c")]
        public static string SpecialNotice { get; set; } = "SpecialNotice";
    }
}