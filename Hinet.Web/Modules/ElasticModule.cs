using Autofac;
using Hinet.Model;

namespace Hinet.Web.Modules
{
    public class ElasticModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ElasticContext>()
              .AsSelf()
              .SingleInstance();
        }
    }
}