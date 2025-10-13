using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.MaGiamGiaArea.Models
{
    public class CreateVM
    {
		public int? SoLuong { get; set; }
		public DateTime? TuNgay { get; set; }
		public DateTime? DenNgay { get; set; }
		public bool? ToanHeThong { get; set; }
		public bool? TrangThai { get; set; }
		public string ThongTin { get; set; }
		//public string GianHangApDung { get; set; }
        public List<string> GianHangApDung { get; set; }

    }
}