using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.GiaTriThuocTinhArea.Models
{
    public class CreateVM
    {
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int TaiKhoanId { get; set; }
		public string ThuocTinhId { get; set; }
		public string ThuocTinhTxt { get; set; }
		public string GiaTri { get; set; }
		public string GiaTriText { get; set; }

        
    }
}