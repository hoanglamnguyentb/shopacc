using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DaoTaoLienTucStatusConstant
    {
        [DisplayName("Bản nháp")]
        public static string BanNhap { get; set; } = "BanNhap";

        [DisplayName("Chờ phê duyệt")]
        public static string ChoPheDuyet { get; set; } = "ChoPheDuyet";

        [DisplayName("Đã duyệt")]
        public static string DaDuyet { get; set; } = "DaDuyet";

        [DisplayName("Từ chối")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName("Hủy")]
        public static string Huy { get; set; } = "Huy";
    }
}