using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.TaiKhoanArea.Models
{
    public class EditVM
    {
		public long Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public string Code { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GameId { get; set; }
		public string TrangThai { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GiaGoc { get; set; }
		public int GiaKhuyenMai { get; set; }
		public string Mota { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int ViTri { get; set; }

        
    }
}