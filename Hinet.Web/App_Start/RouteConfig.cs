using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("signin-google");
            routes.MapMvcAttributeRoutes();

            // Route mặc định
            var @default = routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Hinet.Web.Controllers" }
            );
            @default.DataTokens = @default.DataTokens ?? new RouteValueDictionary();
            @default.DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
