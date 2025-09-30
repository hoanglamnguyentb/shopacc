using System.Web.Mvc;

namespace Hinet.Web.Areas.DanhMucGameArea
{
    public class DanhMucGameAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DanhMucGameArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DanhMucGameArea_default",
                "DanhMucGameArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}