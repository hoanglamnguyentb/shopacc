using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QuanLyQuangCaoStatusConstant
    {
        [DisplayName("Chờ đăng")]
        [Color(BgColor = "#abbac3")]
        public static string ChoDang { get; set; } = "ChoDang";

        [DisplayName("Đã đăng")]
        [Color(BgColor = "#00e676")]
        public static string DaDang { get; set; } = "DaDang";

        [DisplayName("Đã gỡ")]
        [Color(BgColor = "#d15b47")]
        public static string DaGo { get; set; } = "DaGo";

        [DisplayName("Đã kết thúc")]
        [Color(BgColor = "#e62000")]
        public static string DaKetThuc { get; set; } = "DaKetThuc";
    }
}