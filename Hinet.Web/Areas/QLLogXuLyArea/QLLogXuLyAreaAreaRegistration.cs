using System.Web.Mvc;

namespace Hinet.Web.Areas.QLLogXuLyArea
{
    public class QLLogXuLyAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "QLLogXuLyArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "QLLogXuLyArea_default",
                "QLLogXuLyArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}