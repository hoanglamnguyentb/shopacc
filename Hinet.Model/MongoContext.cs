using MongoDB.Driver;
using System;
using System.Configuration;

namespace MongoDBCore
{
    public class MongoContext
    {
        private static MongoContext _instance;
        public static MongoContext Instance => _instance ?? (_instance = new MongoContext());
        private MongoClient Client;
        public IMongoDatabase Database;

        private MongoContext()        //constructor
        {
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
            Client = new MongoClient(settings);
            Database = Client.GetDatabase(MongoDatabaseName);
        }
    }
}