using System.Web.Mvc;

namespace Hinet.Web.Areas.TaiKhoanArea
{
    public class TaiKhoanAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TaiKhoanArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TaiKhoanArea_default",
                "TaiKhoanArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}