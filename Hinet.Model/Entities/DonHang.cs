using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Model.Entities
{
    public class DonHang : AuditableEntity<int>
    {
        public int DonHangId { get; set; }
        public int VatPhamId { get; set; }
        public int MaGiamGia { get; set; }
        public int GiaGoc { get; set; }
        public int GiaKhuyenMai { get; set; }
        public string TrangThai { get; set; }
        public string QrUrl { get; set; }
    }
}
