using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("ThuocTinhGianHang")]
	public class ThuocTinhGianHang : AuditableEntity<int>
	{
        public int GianHangId { get; set; }
        public string TenThuocTinh { get; set; }
        public string KieuDuLieu { get; set; } //text, number, dropdown, boolean
        public string NhomDanhmucCode { get; set; } //Danh mục
        public long? NhomDanhMucId { get; set; } //Danh mục
    }
}