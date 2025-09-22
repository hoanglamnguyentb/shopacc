using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeChamCongSangChieuConstant
    {
        [DisplayName("Cả ngày")]
        [Color(BgColor = "#3498db")]
        public static string CaNgay { get; set; } = "CaNgay";

        [DisplayName("Sáng")]
        [Color(BgColor = "#3498db")]
        public static string DiLamSang { get; set; } = "DiLamSang";

        [DisplayName("Chiều")]
        [Color(BgColor = "#3498db")]
        public static string DiLamChieu { get; set; } = "DiLamChieu";
    }
}