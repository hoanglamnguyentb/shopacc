using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QlTaiSanStatusConstant
    {
        [DisplayName("Mua mới")]
        [Color(BgColor = "#3498db")]
        public static string MuaMoi { get; set; } = "MuaMoi";

        [DisplayName("Đang bảo hành")]
        [Color(BgColor = "#f1c40f")]
        public static string DangBaoHanh { get; set; } = "DangBaoHanh";

        [DisplayName("Đang sử dụng")]
        [Color(BgColor = "#1abc9c")]
        public static string DangSuDung { get; set; } = "DangSuDung";

        [DisplayName("Đã thanh lý")]
        [Color(BgColor = "#9b59b6")]
        public static string DaThanhLy { get; set; } = "DaThanhLy";

        [DisplayName("Đã loại bỏ")]
        [Color(BgColor = "#e74c3c")]
        public static string DaLoaiBo { get; set; } = "DaLoaiBo";
    }
}