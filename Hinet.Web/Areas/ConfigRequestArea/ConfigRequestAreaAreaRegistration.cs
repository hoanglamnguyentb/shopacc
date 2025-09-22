using System.Web.Mvc;

namespace Hinet.Web.Areas.ConfigRequestArea
{
    public class ConfigRequestAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ConfigRequestArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ConfigRequestArea_default",
                "ConfigRequestArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}