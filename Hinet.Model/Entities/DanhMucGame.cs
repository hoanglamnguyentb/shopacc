
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("DanhMucGame")]
	public class DanhMucGame : AuditableEntity<int>
	{
        public int GameId { get; set; }
        public string Name { get; set; }
        public string DuongDanAnh { get; set; }
        public string MoTa { get; set; }
    }
}