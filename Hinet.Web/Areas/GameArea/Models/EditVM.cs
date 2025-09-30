using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.GameArea.Models
{
    public class EditVM
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public string Name { get; set; }
		public string MoTa { get; set; }
		public string TrangThai { get; set; }
        public int STT { get; set; }
        public string ViTriHienThi { get; set; }
    }
}