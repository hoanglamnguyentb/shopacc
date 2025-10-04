using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GameLoadTaiKhoan",
                url: "Game/LoadTaiKhoan",
                defaults: new { controller = "Game", action = "LoadTaiKhoan" },
                namespaces: new[] { "Hinet.Web.Controllers" }
            );

            // Route rút gọn cho Game -> GameController.Index(slug)
            var gameShort = routes.MapRoute(
                name: "GameShort",
                url: "game/{slug}",
                defaults: new { controller = "Game", action = "Index" },
                constraints: new { slug = @"^(?!LoadTaiKhoan|DanhMuc).*" },
                namespaces: new[] { "Hinet.Web.Controllers" }
            );
            gameShort.DataTokens = gameShort.DataTokens ?? new RouteValueDictionary();
            gameShort.DataTokens["UseNamespaceFallback"] = false;

            // Route mua-acc -> GameController.DanhMuc(slug)
            var muaAcc = routes.MapRoute(
                name: "MuaAcc",
                url: "mua-acc/{slug}",
                defaults: new { controller = "Game", action = "DanhMuc", slug = UrlParameter.Optional },
                namespaces: new[] { "Hinet.Web.Controllers" }
            );
            muaAcc.DataTokens = muaAcc.DataTokens ?? new RouteValueDictionary();
            muaAcc.DataTokens["UseNamespaceFallback"] = false;

            // Route acc/{code} -> GameController.ChiTietTaiKhoan(code)
            var accRoute = routes.MapRoute(
                name: "AccChiTiet",
                url: "acc/{code}",
                defaults: new { controller = "Game", action = "ChiTietTaiKhoan" },
                namespaces: new[] { "Hinet.Web.Controllers" }
            );
            accRoute.DataTokens = accRoute.DataTokens ?? new RouteValueDictionary();
            accRoute.DataTokens["UseNamespaceFallback"] = false;

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
