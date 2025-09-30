using Microsoft.Owin;
using Owin;
using System.Web.Http;
using static Hinet.API2.RouteConfig;

[assembly: OwinStartup(typeof(Hinet.API2.Startup))]

namespace Hinet.API2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

    public static class HttpConfigurationExtensions
    {
        public static void MapInheritedAttributeRoutes(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new InheritanceDirectRouteProvider());
        }
    }
}