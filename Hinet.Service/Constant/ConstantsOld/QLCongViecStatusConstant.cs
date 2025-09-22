using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLCongViecStatusConstant
    {
        [DisplayName("Đang thực hiện")]
        [Color(BgColor = "#3498db")]
        public static string DangThucHien { get; set; } = "DangThucHien";

        [DisplayName("Đã hoàn thành")]
        [Color(BgColor = "#2ecc71")]
        public static string DaHoanThanh { get; set; } = "DaHoanThanh";

        [DisplayName("Dừng thực hiện")]
        [Color(BgColor = "#e74c3c")]
        public static string DungThucHien { get; set; } = "DungThucHien";

        [DisplayName("Chưa thực hiện")]
        [Color(BgColor = "#34495e")]
        public static string ChuaThucHien { get; set; } = "ChuaThucHien";
    }
}