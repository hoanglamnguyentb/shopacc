using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.VatPhamArea.Models
{
    public class EditVM
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GianHangId { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GiaGoc { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int STT { get; set; }
		public string Name { get; set; }
		public string DuongDanAnh { get; set; }
		public string MoTa { get; set; }
		public string Slug { get; set; }
        public HttpPostedFileBase FileAnh { get; set; }

    }
}