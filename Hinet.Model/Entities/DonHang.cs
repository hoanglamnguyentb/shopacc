using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Model.Entities
{
    [Table("DonHang")]
    public class DonHang : AuditableEntity<int>
    {
        public int DonHangId { get; set; }
        public int VatPhamId { get; set; }
        public int GianHangId { get; set; }
        public int MaGiamGiaId { get; set; }
        public int GiaGoc { get; set; }
        public int GiaKhuyenMai { get; set; }
        public string TrangThai { get; set; }
        public string QrUrl { get; set; }
        public int SoLuong { get; set; }
        public int TongTien { get; set; }
        public string GhiChu { get; set; }
    }
}
