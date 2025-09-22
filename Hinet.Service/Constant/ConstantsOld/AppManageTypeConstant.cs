using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class AppManageTypeConstant
    {
        [DisplayName("Đăng ký Ứng dụng CCDV")]
        public static int DKWebCCDV { get; set; } = 1;

        [DisplayName("Thông báo Ứng dụng bán hàng")]
        public static int TBWebBanHang { get; set; } = 0;
    }

    public class AppManageDisplayConstant
    {
        [DisplayName("Ứng dụng cung cấp dịch vụ thương mại điện tử")]
        public static int DKWebCCDV { get; set; } = 1;

        [DisplayName("Ứng dụng thương mại điện tử bán hàng")]
        public static int TBWebBanHang { get; set; } = 0;
    }

    public class AppWebTypeConstant
    {
        public static string APP { get; set; } = "APP";
        public static string WEB { get; set; } = "WEB";
    }
}