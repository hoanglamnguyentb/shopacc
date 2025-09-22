using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hinet.Web.MongoDBEntities
{
    public class MessageUser
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
    }
}