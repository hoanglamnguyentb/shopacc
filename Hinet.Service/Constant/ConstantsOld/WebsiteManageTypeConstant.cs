using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class WebsiteManageTypeConstant
    {
        [DisplayName("Website cung cấp dịch vụ thương mai điện tử")]
        public static int DKWebCCDV { get; set; } = 1;

        [DisplayName("Website thương mại điện tử bán hàng")]
        public static int TBWebBanHang { get; set; } = 0;
    }

    public class WebsiteManageDisplayConstant
    {
        [DisplayName("Website CCDV thương mai điện tử")]
        public static int DKWebCCDV { get; set; } = 1;

        [DisplayName("Website thương mại điện tử bán hàng")]
        public static int TBWebBanHang { get; set; } = 0;
    }
}