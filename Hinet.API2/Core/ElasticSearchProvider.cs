using Nest;
using System;
using System.Configuration;

namespace Hinet.API2
{
    public class ElasticSearchProvider
    {
        public static ElasticClient client;

        static ElasticSearchProvider()
        {
            var HostES = ConfigurationManager.AppSettings["HostES"].ToString();
            var uriHost = new Uri(HostES);
            var connectionSettings = new ConnectionSettings(uriHost)
                .EnableDebugMode()
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            client = new ElasticClient(connectionSettings);
        }
    }
}