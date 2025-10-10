namespace Hinet.Model.MongoDBEntities
{
    public class LogBuTrucMDB : AuditableEntityMongo<string>
    {
        public long? IdNhanSu { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public double? SoNgayBu { get; set; }
        public double? TrucCuoiThang { get; set; }
    }
}