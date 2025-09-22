using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class PhanAnhInfoConstant
    {
        [DisplayName("Mới đăng")]
        [Color(BgColor = "#bdc3c7")]
        public static string MoiDang { get; set; } = "MoiDang";

        [DisplayName("Đang xử lý")]
        [Color(BgColor = "#f1c40f")]
        public static string ChoXuLy { get; set; } = "ChoXuLy";

        [DisplayName("Đã xử lý xong")]
        [Color(BgColor = "#3498db")]
        public static string DaXuLy { get; set; } = "DaXuLyXong";

        [DisplayName("Bị từ chối")]
        [Color(BgColor = "#e74c3c")]
        public static string BiTuChoi { get; set; } = "BiTuChoi";
    }

    public class PhanAnhTypeConstant
    {
        [DisplayName("Chưa Thông báo")]
        public static string Tbao { get; set; } = "NOT_NOTIFIED";

        [DisplayName("Chưa đăng ký")]
        public static string Dky { get; set; } = "NOT_REGISTER";

        [DisplayName("Chưa đăng ký, thông báo")]
        public static string DkyTbo { get; set; } = "NOT_REGISTER_NOTIFIED";
    }
}