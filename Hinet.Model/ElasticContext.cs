using Hinet.Model.ElasticEntities;
using Nest;
using System;
using System.Configuration;


namespace Hinet.Model
{

    public class ElasticContext
    {
        public ElasticClient Client;
        public ElasticContext()
        {
            //var elasticConnection = ConfigurationManager.ConnectionStrings["ElasticContext"].ConnectionString;
            //var settings = new ConnectionSettings(new Uri(elasticConnection));
            //settings.DefaultMappingFor<ObjMap>(x => x.IndexName("obj_item_map_dulichhaiphong").IdProperty(y => y.GuidId));
            //Client = new ElasticClient(settings);
            //var pingResponse = Client.Ping();
            //if (pingResponse.IsValid)
            //{
            //    CreateIndexIfNotExists("obj_item_map_dulichhaiphong", x => x.Map<ObjMap>(y => y.AutoMap()));
            //}

            var elasticConnection = ConfigurationManager.ConnectionStrings["ElasticContext"].ConnectionString;
            var settings = new ConnectionSettings(new Uri(elasticConnection));
            settings.DefaultMappingFor<ObjMap>(x => x.IndexName("obj_item_map_gisthanhoa_v2").IdProperty(y => y.GuidId));
            Client = new ElasticClient(settings);
            var pingResponse = Client.Ping();
            if (pingResponse.IsValid)
            {
                CreateIndexIfNotExists("obj_item_map_gisthanhoa_v2", x => x.Map<ObjMap>(y => y.AutoMap()));
            }

        }
        public void CreateIndexIfNotExists(string indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> mapping)
        {
            if (!Client.Indices.Exists(indexName).Exists)
            {
                var x = Client.Indices.Create(indexName, mapping);
            }
        }
    }

}