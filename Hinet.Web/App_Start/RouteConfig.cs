using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
              name: "CheckLogin",
              url: "CheckLogin",
              defaults: new { controller = "Dashboard", action = "GetInfoUserLogin", id = UrlParameter.Optional }
                                ).DataTokens = new RouteValueDictionary(new { area = "DashboardArea" });

            routes.MapRoute(
                name: "baocaopublic",
                url: "baocaopublic",
                defaults: new { controller = "baocaothongkeioc", action = "thongkepublic", id = UrlParameter.Optional }
                                ).DataTokens = new RouteValueDictionary(new { area = "baocaothongkearea" });

            routes.MapRoute(
               name: "AdminRoute",
               url: "admin",
               defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
                                ).DataTokens = new RouteValueDictionary(new { area = "DashboardArea" });

            routes.MapRoute(
                     name: "Default",
                     url: "{controller}/{action}/{id}",
                     defaults: new { controller = "AccountAdmin", action = "Login", id = UrlParameter.Optional }
                    );

            routes.MapRoute(
                name: "DefaultArea",
                url: "{area}/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}