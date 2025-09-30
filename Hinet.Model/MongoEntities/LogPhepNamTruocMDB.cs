namespace Hinet.Model.MongoDBEntities
{
    public class LogPhepNamTruocMDB : AuditableEntityMongo<string>
    {
        public long ItemId { get; set; }
        public long? LyLichId { get; set; }
        public double? SoPhepNamTruoc { get; set; }
        public int Nam { get; set; }
    }
}