using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.DanhMucGameArea.Models
{
    public class CreateVM
    {
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int? GameId { get; set; }
		public string Name { get; set; }
		public string DuongDanAnh { get; set; }
		public string MoTa { get; set; }
        public string ThongBao { get; set; }
        public HttpPostedFileBase FileAnh { get; set; }

    }
}