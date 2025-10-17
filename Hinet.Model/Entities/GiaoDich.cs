using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("GiaoDich")]
	public class GiaoDich : AuditableEntity<long>
	{
		public long NguoiGiaoDich { get; set; }
		public long DoiTuongId { get; set; } //Id của đối tượng (TaiKhoanId, GameId, v.v.)
        public string LoaiDoiTuong { get; set; } //TaiKhoan, GameId 
        public string LoaiGiaoDich { get; set; } //Nạp topup, Mua acc, Nạp thường
        public string TrangThai { get; set; }
		public string PhuongThucThanhToan { get; set; }
		public DateTime NgayGiaoDich { get; set; }
		public DateTime? NgayXuLy { get; set; }
		public int SoTien { get; set; }
		public string NoiDung { get; set; }
        public string TenTaiKhoanCanNap { get; set; }
        public string MatKhauTaiKhoanNap { get; set; }
        //Full giao dịch
        public string MaGiaoDich { get; set; }
        public string MaGiaoDichDoiTac { get; set; }
        public string NoiDungChuyenKhoan { get; set; }
        public string WebhookTrangThai { get; set; }
        public DateTime? ThoiGianWebhook { get; set; }
    }
}