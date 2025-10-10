using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("GiaTriThuocTinh")]
	public class GiaTriThuocTinh : AuditableEntity<long>
	{
		public int TaiKhoanId { get; set; }
        public int? ThuocTinhId { get; set; }
        public string ThuocTinhTxt { get; set; }
		public string GiaTri { get; set; }
		public string GiaTriTxt { get; set; }
        public string KieuDuLieu { get; set; }
    }
}