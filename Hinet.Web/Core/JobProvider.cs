using Autofac;
using Hinet.Modules;
using Hinet.Web.Modules;

//using AutoMapperModule = Hinet.Web.Modules.AutoMapperModule;

namespace Hinet.Web.Core
{
    public class JobProvider : IJobProvider
    {
        private static IContainer Container { get; set; }

        public JobProvider()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            //builder.RegisterModule(new RedisModule());
            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule(new WebModule());

            Container = builder.Build();
        }
    }
}