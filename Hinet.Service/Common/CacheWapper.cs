using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hinet.Service.Common
{
    public class CacheWapper<T> where T : class
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TimeOutCache { get; set; }

        public T DataCache { get; set; }
    }
}