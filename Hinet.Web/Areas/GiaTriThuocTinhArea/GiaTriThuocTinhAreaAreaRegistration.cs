using System.Web.Mvc;

namespace Hinet.Web.Areas.GiaTriThuocTinhArea
{
    public class GiaTriThuocTinhAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GiaTriThuocTinhArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GiaTriThuocTinhArea_default",
                "GiaTriThuocTinhArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}