using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(Hinet.Web.Startup))]

namespace Hinet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            var configDB = WebConfigurationManager.AppSettings["HangFireDB"];
            if (!string.IsNullOrEmpty(configDB))
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(configDB);
                app.UseHangfireDashboard();
                app.UseHangfireServer();
            }
        }
    }
}