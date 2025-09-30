using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("TaiKhoan")]
	public class TaiKhoan : AuditableEntity<long>
	{
		public string Code { get; set; }
		public int GameId { get; set; }
		public string TrangThai { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int GiaGoc { get; set; }
		public int GiaKhuyenMai { get; set; }
		public string Mota { get; set; }
		public int ViTri { get; set; }
    }
}