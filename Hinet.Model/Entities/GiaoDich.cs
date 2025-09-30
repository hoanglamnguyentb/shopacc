using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("GiaoDich")]
	public class GiaoDich : AuditableEntity<long>
	{
		public long UserId { get; set; }
		public long DoiTuongId { get; set; }
		public string LoaiDoiTuong { get; set; }
		public string LoaiGiaoDich { get; set; }
		public string TrangThai { get; set; }
		public string PhuongThucThanhToan { get; set; }
		public DateTime NgayGiaoDich { get; set; }
		public DateTime? NgayThanhToan { get; set; }
		public int SoTien { get; set; }
	}
}