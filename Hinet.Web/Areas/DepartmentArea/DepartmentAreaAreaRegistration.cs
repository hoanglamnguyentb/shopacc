using System.Web.Mvc;

namespace Hinet.Web.Areas.DepartmentArea
{
    public class DepartmentAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DepartmentArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DepartmentArea_default",
                "DepartmentArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}