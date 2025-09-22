using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LyLichStatusConstant
    {
        [DisplayName("Học tập")]
        public static string HocTap { get; set; } = "HocTap";

        [DisplayName("Thử việc")]
        [Color(BgColor = "#bdc3c7")]
        public static string ThuViec { get; set; } = "ThuViec";

        [DisplayName("Chính thức")]
        [Color(BgColor = "#2ecc71")]
        public static string ChinhThuc { get; set; } = "ChinhThuc";

        [DisplayName("Thôi việc")]
        [Color(BgColor = "#e74c3c")]
        public static string ThoiViec { get; set; } = "ThoiViec";

        [DisplayName("Nghỉ hưu")]
        public static string NghiHuu { get; set; } = "NghiHuu";
    }
}