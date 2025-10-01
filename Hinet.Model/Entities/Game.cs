using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("Game")]
	public class Game : AuditableEntity<int>
	{
		[Required]
		[StringLength(250)]
		public string Name { get; set; }
		public string MoTa { get; set; }
		public string TrangThai { get; set; }
        public int STT { get; set; }
        public string ViTriHienThi { get; set; }
        public string Slug { get; set; }
    }
}