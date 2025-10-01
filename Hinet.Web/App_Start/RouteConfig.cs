using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Route rút gọn cho Game -> GameController.Index(slug)
            var gameShort = routes.MapRoute(
                name: "GameShort",
                url: "game/{slug}",
                defaults: new { controller = "Game", action = "Index", slug = UrlParameter.Optional },
                namespaces: new[] { "Hinet.Web.Controllers" } // chỉ tìm controller trong Web
            );
            gameShort.DataTokens = gameShort.DataTokens ?? new RouteValueDictionary();
            gameShort.DataTokens["UseNamespaceFallback"] = false; // không fallback qua namespace khác

            // Route mua-acc -> GameController.DanhMuc(slug)
            var muaAcc = routes.MapRoute(
                name: "MuaAcc",
                url: "mua-acc/{slug}",
                defaults: new { controller = "Game", action = "DanhMuc", slug = UrlParameter.Optional },
                namespaces: new[] { "Hinet.Web.Controllers" }
            );
            muaAcc.DataTokens = muaAcc.DataTokens ?? new RouteValueDictionary();
            muaAcc.DataTokens["UseNamespaceFallback"] = false;

            // Route mặc định
            var @default = routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Hinet.Web.Controllers" } // BẮT BUỘC: fix trùng controller
            );
            @default.DataTokens = @default.DataTokens ?? new RouteValueDictionary();
            @default.DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
