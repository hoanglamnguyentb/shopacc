using System.Web.Mvc;

namespace Hinet.Web.Areas.BinhLuanArea
{
    public class BinhLuanAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BinhLuanArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BinhLuanArea_default",
                "BinhLuanArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}