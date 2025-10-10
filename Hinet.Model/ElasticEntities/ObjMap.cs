using System;

namespace Hinet.Model.ElasticEntities
{
    public class ObjMap : ElasticEntity
    {
        public Guid GuidId { get; set; }
        public long ItemId { get; set; }
        public string Type { get; set; }
        public string TenDoiTuong { get; set; }
        public string DiaChi { get; set; }
        public string TrangThai { get; set; }
        public string AnhDaiDien { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public long ParentId { get; set; }
        public string LngStart { get; set; }
        public string LatStart { get; set; }
        public string LngEnd { get; set; }
        public string LatEnd { get; set; }
        public string HuyenId{ get; set; }
        public string XaId{ get; set; }
        public string PathAvata { get; set; }
        public string NoiDung { get; set; }
        public float DiemTrungBinh { get; set; }
        public int SoLuotDanhGia { get; set; }
        public string PathMarker { get; set; }
        public string detailInfor { get; set; }
        public string objectChildren { get; set; }
    }
}
