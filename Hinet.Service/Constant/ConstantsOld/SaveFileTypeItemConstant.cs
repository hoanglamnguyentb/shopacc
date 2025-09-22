using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class SaveFileTypeItemConstant
    {
        [DisplayName("Website cung cấp dịch vụ logo")]
        public static string RegisterWebsite { get; set; } = "RegisterWebsite";

        public static string RegisterWebsiteDoc { get; set; } = "RegisterWebsiteDoc";

        [DisplayName("Website thông báo")]
        public static string NotifyWebsite { get; set; } = "NotifyWebsite";

        public static string NotifyWebsiteDoc { get; set; } = "NotifyWebsiteDoc";

        [DisplayName("Ứng dụng cung cấp dịch vụ logo")]
        public static string RegisterApp { get; set; } = "RegisterApp";

        public static string RegisterAppDoc { get; set; } = "RegisterAppDoc";

        [DisplayName("Ứng dụng thông báo")]
        public static string NotifyApp { get; set; } = "NotifyApp";

        public static string NotifyAppDoc { get; set; } = "NotifyAppDoc";

        [DisplayName("Đánh giá tín nhiệm icon")]
        public static string DGTNIcon { get; set; } = "DGTNIcon";

        public static string DGTNDoc { get; set; } = "DGTNDoc";
        public static string DocumentReport { get; set; } = "DocumentReport";
        public static string WorkFlow { get; set; } = "WorkFlow";
        public static string ProductInfo { get; set; } = "ProductInfo";
        public static string PhanAnhFileAttach { get; set; } = "PhanAnhFileAttach";

        #region register account

        [DisplayName("Tệp tin đăng ký kinh doanh công ty")]
        public static string DKKinhDoanh { get; set; } = "DKKinhDoanh";

        [DisplayName("Tệp tin quyết định thành lập tổ chức")]
        public static string QDThanhLap { get; set; } = "QDThanhLap";

        public static string Company { get; set; } = "Company";
        public static string Personal { get; set; } = "Personal";
        public static string Organization { get; set; } = "Organization";
        public static string DotBaoCao { get; set; } = "DotBaoCao";

        #endregion register account

        [DisplayName("Đăng ký tin bài")]
        public static string RegisterTinBai { get; set; } = "RegisterTinBai";
    }
}