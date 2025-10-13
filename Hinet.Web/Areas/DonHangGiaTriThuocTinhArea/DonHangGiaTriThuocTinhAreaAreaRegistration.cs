using System.Web.Mvc;

namespace Hinet.Web.Areas.DonHangGiaTriThuocTinhArea
{
    public class DonHangGiaTriThuocTinhAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DonHangGiaTriThuocTinhArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DonHangGiaTriThuocTinhArea_default",
                "DonHangGiaTriThuocTinhArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}