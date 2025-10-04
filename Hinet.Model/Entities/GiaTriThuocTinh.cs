using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("GiaTriThuocTinh")]
	public class GiaTriThuocTinh : AuditableEntity<long>
	{
		public int TaiKhoanId { get; set; }
        public string ThuocTinhId { get; set; }
        public string ThuocTinhTxt { get; set; }
		public string GiaTri { get; set; }
		public string GiaTriText { get; set; }
    }
}