using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLQuangCaoBaoInConstant
    {
        [Color(BgColor = "#34495e")]
        [DisplayName("Mới tạo")]
        public static string MoiTao { get; set; } = "MoiTao";

        [Color(BgColor = "#3498db")]
        [DisplayName("Chờ xử lý")]
        public static string ChoXuLy { get; set; } = "ChoXuLy";

        [Color(BgColor = "#2ecc71")]
        [DisplayName("Đã duyệt")]
        public static string DaDuyet { get; set; } = "DaDuyet";
    }
}