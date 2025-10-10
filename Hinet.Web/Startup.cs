using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Web.Configuration;

//[assembly: OwinStartupAttribute(typeof(Hinet.Web.Startup))]
[assembly: OwinStartup(typeof(Hinet.Web.Startup))]
namespace Hinet.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
            app.Use((context, next) =>
            {
                context.Request.Host = new HostString("localhost");
                return next();
            });
            ConfigureAuth(app);
		}
	}
}