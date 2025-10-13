using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.DonHangGiaTriThuocTinhArea.Models
{
    public class EditVM
    {
		public long Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int DonHangId { get; set; }
		public int? ThuocTinhId { get; set; }
		public string ThuocTinhTxt { get; set; }
		public string GiaTri { get; set; }
		public string GiaTriTxt { get; set; }
		public string KieuDuLieu { get; set; }

        
    }
}