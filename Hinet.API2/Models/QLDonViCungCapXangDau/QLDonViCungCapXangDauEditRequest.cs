using System;

namespace Hinet.API2.Models.QLDonViCungCapXangDau
{
    public class QLDonViCungCapXangDauEditRequest
    {
        public long Id { get; set; }
        public string MaDonViCC { get; set; }
        public string TenDonViCC { get; set; }
        public string PhuongTienVC { get; set; }
        public decimal? PhiVanChuyenCC { get; set; }
        public DateTime? NgayDiVaoHD { get; set; }
        public string Huyen { get; set; }
        public string Xa { get; set; }
        public int? SoLuongTramXangDVCC { get; set; }
        public decimal? KinhDo { get; set; }
        public decimal? ViDo { get; set; }
        public string LoaiNhienLieuCC { get; set; }
        public string DaiDien { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }
    }
}