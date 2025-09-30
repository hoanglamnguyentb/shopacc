using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("DichVu")]
	public class DichVu : AuditableEntity<int>
	{
		[Required]
		[StringLength(250)]
		public string Name { get; set; }
		public string DuongDanAnh { get; set; }
		public string Link { get; set; }
		public bool? KichHoat { get; set; }
        public int STT { get; set; }
    }
}