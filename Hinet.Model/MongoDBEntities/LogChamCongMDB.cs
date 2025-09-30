using Hinet.Model.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hinet.Model.MongoDBEntities
{
    public class LogChamCongMDB : AuditableEntityMongo<string>
    {
        public long ItemId { get; set; }
        public string CaLamViecCode { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? NgayLamViec { get; set; }

        public string GhiChu { get; set; }

        public long? NhanVienId { get; set; }

        public long? DepartmentId { get; set; }
        public Department DepartmentInfo { get; set; }
        public long? LyLichDepartmentId { get; set; }
        public Department LyLichDepartmentInfo { get; set; }

        public bool Status { get; set; }

        public string ItemType { get; set; }
        public string ItemTypeName { get; set; }

        public double? KhoiLuongCongViec { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
        public long? IdDonNghiPhep { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string ActionType { get; set; }
        public string LogName { get; set; }
    }
}