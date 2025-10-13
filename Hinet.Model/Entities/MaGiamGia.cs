using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("MaGiamGia")]
	public class MaGiamGia : AuditableEntity<int>
	{
        public string ThongTin { get; set; }
        public string GianHangApDung { get; set; }
        public bool ToanHeThong { get; set; }
        public int SoLuong { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public bool TrangThai { get; set; }
    }
}