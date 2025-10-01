using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("BinhLuan")]
	public class BinhLuan : AuditableEntity<long>
	{
        public long NguoiBinhLuanId { get; set; }
        public long DoiTuongId { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string NoiDung { get; set; }
        public int Diem { get; set; }
        public long ParentId { get; set; }
        public string TrangThai { get; set; }
    }
}