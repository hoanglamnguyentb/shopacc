using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("DanhMucGameTaiKhoan")]
	public class DanhMucGameTaiKhoan : AuditableEntity<long>
	{
        public int DanhMucGameId { get; set; }
        public long TaiKhoanId { get; set; }
    }
}