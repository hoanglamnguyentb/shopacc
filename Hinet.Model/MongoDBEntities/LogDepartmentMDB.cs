using Hinet.Model.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hinet.Model.MongoDBEntities
{
    public class LogDepartmentMDB : AuditableEntityMongo<string>
    {
        public long IdNhanSu { get; set; }
        public long DepartmentId { get; set; }
        public Department departmentInfo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? TimeStart { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? TimeEnd { get; set; }

        public string Name { get; set; }
    }
}