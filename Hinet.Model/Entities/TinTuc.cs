using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("TinTuc")]
	public class TinTuc : AuditableEntity<long>
	{
		public string Slug { get; set; }
		public string TieuDe { get; set; }
		public string NoiDung { get; set; }
		public string AnhBia { get; set; }
		public string TacGia { get; set; }
		public string TrangThai { get; set; }// Nháp, Xuất bản, Lưu trữ
		public DateTime ThoiGianXuatBan { get; set; }
	}
}