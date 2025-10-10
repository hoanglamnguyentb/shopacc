using System.Web.Mvc;

namespace Hinet.Web.Areas.DepositArea
{
    public class DepositAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DepositArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DepositArea_default",
                "DepositArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}