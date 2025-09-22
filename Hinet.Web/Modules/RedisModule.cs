using Autofac;

namespace Hinet.Web.Modules
{
    public class RedisModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder
            //.Register(cx => ConnectionMultiplexer.Connect(string.Format("{0}:{1}", WebConfigurationManager.AppSettings["RedisHost"], WebConfigurationManager.AppSettings["RedisPort"])))
            //.As<IConnectionMultiplexer>()
            //.SingleInstance();
            //builder.Register(c => c.Resolve<IConnectionMultiplexer>()
            //     .GetDatabase(WebConfigurationManager.AppSettings["RedisDatabase"].ToIntOrZero()))
            //     .As<IDatabase>()
            //     .SingleInstance();
        }
    }
}