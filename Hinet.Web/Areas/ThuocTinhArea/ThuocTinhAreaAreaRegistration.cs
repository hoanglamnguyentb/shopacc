using System.Web.Mvc;

namespace Hinet.Web.Areas.ThuocTinhArea
{
    public class ThuocTinhAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ThuocTinhArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ThuocTinhArea_default",
                "ThuocTinhArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}