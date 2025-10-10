using System.Web.Mvc;

namespace Hinet.Web.Areas.SiteConfigArea
{
    public class SiteConfigAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SiteConfigArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SiteConfigArea_default",
                "SiteConfigArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}