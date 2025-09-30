using System.Web.Mvc;

namespace Hinet.Web.Areas.GiaoDichArea
{
    public class GiaoDichAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GiaoDichArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GiaoDichArea_default",
                "GiaoDichArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}