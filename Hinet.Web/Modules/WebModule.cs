using Autofac;
using System.Linq;
using System.Reflection;

namespace Hinet.Modules
{
    public class WebModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Hinet.Web"))

                      .Where(t => t.Name.EndsWith("Provider"))

                      .AsImplementedInterfaces()

                      .InstancePerLifetimeScope();
        }
    }
}