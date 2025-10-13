using System.Web.Mvc;

namespace Hinet.Web.Areas.MaGiamGiaArea
{
    public class MaGiamGiaAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MaGiamGiaArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MaGiamGiaArea_default",
                "MaGiamGiaArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}