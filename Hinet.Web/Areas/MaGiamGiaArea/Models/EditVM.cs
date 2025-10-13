using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.MaGiamGiaArea.Models
{
    public class EditVM
    {
		public int Id { get; set; }
		public int? SoLuong { get; set; }
		public DateTime? TuNgay { get; set; }
		public DateTime? DenNgay { get; set; }
		public bool? ToanHeThong { get; set; }
		public bool? TrangThai { get; set; }
		public string ThongTin { get; set; }
		public List<string> GianHangApDung { get; set; }

        
    }
}