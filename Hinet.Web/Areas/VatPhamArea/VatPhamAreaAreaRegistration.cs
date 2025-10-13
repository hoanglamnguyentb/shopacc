using System.Web.Mvc;

namespace Hinet.Web.Areas.VatPhamArea
{
    public class VatPhamAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "VatPhamArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "VatPhamArea_default",
                "VatPhamArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}