using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class HighLightConstant
    {
        [DisplayName("Yêu cầu giải trình")]
        public static string HL_YeuCauGiaiTrinh { get; set; } = "HL_YeuCauGiaiTrinh";
    }

    public class HighLightContentConstant
    {
        [DisplayName("Bạn có yều cầu giải trình phản ánh website!")]
        public static string HL_YeuCauGiaiTrinhWebsiteContent { get; set; } = "HL_YeuCauGiaiTrinhWebsiteContent";

        [DisplayName("Bạn có yều cầu giải trình phản ánh ứng dụng!")]
        public static string HL_YeuCauGiaiTrinhAppContent { get; set; } = "HL_YeuCauGiaiTrinhAppContent";
    }
}