using MongoDB.Driver;
using System;
using System.Configuration;

namespace Hinet.Web.App_Start
{
    public class MongoDBContext
    {
        private MongoClient _client;
        private MongoServer _server;
        public MongoDatabase _database;

        public MongoDBContext()        //constructor
        {
            // Reading credentials from Web.config file
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //CarDatabase
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"]; //demouser
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"]; //Pass@123
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];  //27017
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];  //localhost

            // Creating credentials
            //var credential = MongoCredential.CreateMongoCRCredential
            //                (MongoDatabaseName,
            //                 MongoUsername,
            //                 MongoPassword);

            // Creating MongoClientSettings
            var settings = new MongoClientSettings
            {
                //Credentials = new[] { credential },
                Server = new MongoServerAddress(MongoHost, Convert.ToInt32(MongoPort))
            };
            _client = new MongoClient(settings);
            _server = _client.GetServer();
            _database = _server.GetDatabase(MongoDatabaseName);
        }
    }
}