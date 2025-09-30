using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.BannerArea.Models
{
    public class CreateVM
    {
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public string Name { get; set; }
		public string DuongDanAnh { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public string Link { get; set; }
		public bool? KichHoat { get; set; }
        public int STT { get; set; }
        public HttpPostedFileBase FileAnh { get; set; }
    }
}