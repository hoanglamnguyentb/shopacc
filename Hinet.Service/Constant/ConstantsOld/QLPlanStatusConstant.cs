using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLPlanStatusConstant
    {
        [DisplayName("Mới tạo")]
        [Color(BgColor = "#34495e")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName("Chờ duyệt")]
        [Color(BgColor = "#3498db")]
        public static string ChoDuyet { get; set; } = "ChoDuyet";

        [DisplayName("Yêu cầu chỉnh sửa")]
        [Color(BgColor = "#f1c40f")]
        public static string YeuCauChinhSua { get; set; } = "YeuCauChinhSua";

        [DisplayName("Từ chối")]
        [Color(BgColor = "#e74c3c")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName("Đã phê duyệt")]
        [Color(BgColor = "#2ecc71")]
        public static string DaPheDuyet { get; set; } = "DaPheDuyet";
    }
}