
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("VatPham")]
	public class VatPham : AuditableEntity<int>
	{
        public int GianHangId { get; set; }
        public string Name { get; set; }
        public string DuongDanAnh { get; set; }
        public string MoTa { get; set; }
        public string Slug { get; set; }
        public int GiaGoc { get; set; }
        public int STT { get; set; }
    }
}