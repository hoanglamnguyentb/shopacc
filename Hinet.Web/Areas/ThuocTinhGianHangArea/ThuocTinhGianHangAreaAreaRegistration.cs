using System.Web.Mvc;

namespace Hinet.Web.Areas.ThuocTinhGianHangArea
{
    public class ThuocTinhGianHangAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ThuocTinhGianHangArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ThuocTinhGianHangArea_default",
                "ThuocTinhGianHangArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}