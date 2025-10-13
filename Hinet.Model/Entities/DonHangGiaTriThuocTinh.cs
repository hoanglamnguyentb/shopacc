using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("DonHangGiaTriThuocTinh")]
	public class DonHangGiaTriThuocTinh : AuditableEntity<long>
	{
		public int DonHangId { get; set; }
        public int? ThuocTinhId { get; set; }
        public string ThuocTinhTxt { get; set; }
		public string GiaTri { get; set; }
		public string GiaTriTxt { get; set; }
        public string KieuDuLieu { get; set; }
    }
}