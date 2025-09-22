using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class NewsCategoryConstant
    {
        [DisplayName("Tạo mới")]
        public static string TaoMoi { get; set; } = "NEW";

        [DisplayName("Đã duyệt")]
        public static string Duyet { get; set; } = "APPROVED";

        [DisplayName("Tin hỏi đáp (Dịch vụ công)")]
        public static string HoiDapDVC { get; set; } = "hoi-dap";

        [DisplayName("Tin trang chủ (Dịch vụ công)")]
        public static string HomeDVC { get; set; } = "tin-tuc";

        [DisplayName("Tin Cảnh báo (Dịch vụ công)")]
        public static string CanhBaoDVC { get; set; } = "canh-bao";

        [DisplayName("Tin Thông báo (Dịch vụ công)")]
        public static string ThongBaoDVC { get; set; } = "thong-bao";

        [DisplayName("Liên hệ (Dịch vụ công)")]
        public static string LienHeDVC { get; set; } = "lien-he";
    }
}