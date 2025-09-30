using Autofac;
using Autofac.Integration.Mvc;
using Hinet.Modules;
using Hinet.Web.Core;
using Hinet.Web.HubControl;
using Hinet.Web.Modules;
using log4net;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hinet.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(long?), new LongModelBinder());
            ModelBinders.Binders.Add(typeof(long), new LongModelBinder());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Autofac Configuration
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule(new WebModule());

            builder.RegisterType<XinChaoHub>().ExternallyOwned();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            var pathConfig = Server.MapPath("~/App_Data/ConfigStatus.json");
            StatusProvider.loadData(pathConfig);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            ILog log = LogManager.GetLogger("RollingLogFileAppender");

            var exception = Server.GetLastError();
            log.Error("Lỗi hệ thống", exception);
            if (exception is HttpUnhandledException)
            {
                log.Error("Lỗi hệ thống", exception);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            //if (cookie != null && cookie.Value != null)
            //{
            //    System.Threading.Thread.CurrentThread.CurrentCulture =
            //        new System.Globalization.CultureInfo(cookie.Value);
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo(cookie.Value);
            //}
            //else
            //{
            //    System.Threading.Thread.CurrentThread.CurrentCulture =
            //        new System.Globalization.CultureInfo("vi");
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo("vi");
            //}
        }
    }
}