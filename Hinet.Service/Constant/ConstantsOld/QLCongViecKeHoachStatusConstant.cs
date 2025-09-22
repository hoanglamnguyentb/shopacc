using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLCongViecKeHoachStatusConstant
    {
        //[DisplayName("Lưu tạm")]
        //[Color(BgColor = "#bdc3c7")]
        //public static string LuuTam { get; set; } = "LuuTam";
        [DisplayName("Mới tạo")]
        [Color(BgColor = "#f1c40f")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName("Chờ duyệt")]
        [Color(BgColor = "#3498db")]
        public static string ChoDuyet { get; set; } = "ChoDuyet";

        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#2ecc71")]
        public static string DaDuyet { get; set; } = "DaDuyet";

        [DisplayName("Từ chối")]
        [Color(BgColor = "#2ecc71")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName("Yêu cầu chỉnh sửa")]
        [Color(BgColor = "#34495e")]
        public static string YeuCauChinhSua { get; set; } = "YeuCauChinhSua";

        [DisplayName("Đã kết thúc")]
        [Color(BgColor = "#16a085")]
        public static string DaKetThuc { get; set; } = "DaKetThuc";
    }
}