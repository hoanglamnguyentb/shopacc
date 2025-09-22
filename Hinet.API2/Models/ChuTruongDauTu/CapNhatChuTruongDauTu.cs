namespace Hinet.API2.Models.ChuTruongDauTu
{
    public class CapNhatChuTruongDauTu
    {
        public long Id { get; set; }
        public long IdDuAn { get; set; }
        public long IdBuoc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }

        public string HinhThucThucHien { get; set; }
    }
}