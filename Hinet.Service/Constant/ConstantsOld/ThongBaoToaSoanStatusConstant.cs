using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ThongBaoToaSoanStatusConstant
    {
        [DisplayName("Bản thảo")]
        [Color(BgColor = "#95a5a6")]
        public static string BanThao { get; set; } = "BanThao";

        [DisplayName("Chờ duyệt")]
        [Color(BgColor = "#1abc9c")]
        public static string ChoDuyet { get; set; } = "ChoDuyet";

        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#2980b9")]
        public static string DaDuyet { get; set; } = "DaDuyet";

        [DisplayName("Từ chối")]
        [Color(BgColor = "#00e676")]
        public static string TuChoi { get; set; } = "TuChoi";
    }
}