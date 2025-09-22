using System.Web.Mvc;

namespace Hinet.Web.Areas.QLCanhBao
{
    public class QLCanhBaoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QLCanhBao";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QLCanhBao_default",
                "QLCanhBao/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}