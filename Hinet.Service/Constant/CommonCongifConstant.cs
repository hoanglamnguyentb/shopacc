using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class CommonConfigConstant
    {
        [DisplayName("Địa chỉ phản ánh")]
        public static string ADDRESS_PHAN_ANH { get; set; } = "ADDRESS_PHAN_ANH";

        [DisplayName("Email phản ánh")]
        public static string EMAIL_PHAN_ANH { get; set; } = "EMAIL_PHAN_ANH";

        [DisplayName("Số điện thoại phản ánh")]
        public static string PHONE_PHAN_ANH { get; set; } = "PHONE_PHAN_ANH";

        [DisplayName("Tên phản ánh")]
        public static string NAME_PHAN_ANH { get; set; } = "NAME_PHAN_ANH";

        [DisplayName("Số lượng sản phẩm")]
        public static string AMOUNT_OF_PRODUCT { get; set; } = "AMOUNT_OF_PRODUCT";

        [DisplayName("Cấp id hồ sơ đánh giá tín nhiệm")]
        public static string CAP_ID_HS_DANHGIATINNHIEM { get; set; } = "CAP_ID_HS_DANHGIATINNHIEM";

        [DisplayName("Cấp id hồ sơ ứng dụng")]
        public static string CAP_ID_HS_APP { get; set; } = "CAP_ID_HS_APP";

        [DisplayName("Cấp id hồ sơ website")]
        public static string CAP_ID_HS_WEBSITE { get; set; } = "CAP_ID_HS_WEBSITE";

        [DisplayName("Cho phép gửi mail")]
        public static string ALLOW_SEND_MAIL { get; set; } = "ALLOW_SEND_MAIL";

        [DisplayName("Mã loại phản ánh chưa đăng ký")]
        public static string CODE_PHANANH_CHUADANGKY { get; set; } = "CODE_PHANANH_CHUADANGKY";

        [DisplayName("Tuổi nghỉ hưu nữ")]
        public static string TUOINGHIHUU_NU { get; set; } = "TUOINGHIHUU_NU";

        [DisplayName("Tuổi nghỉ hưu nam")]
        public static string TUOINGHIHUU_NAM { get; set; } = "TUOINGHIHUU_NAM";
    }
}