using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("TaiLieuDinhKem")]
	public class TaiLieuDinhKem : AuditableEntity<long>
	{
		[Required]
		[StringLength(500)]
		public string TenTaiLieu { get; set; }

		[StringLength(250)]
		public string LoaiTaiLieu { get; set; }

		public long? Item_ID { get; set; }
		public string MoTa { get; set; }
		public string DuongDanFile { get; set; }

		[StringLength(250)]
		public string DinhDangFile { get; set; }
	}
}