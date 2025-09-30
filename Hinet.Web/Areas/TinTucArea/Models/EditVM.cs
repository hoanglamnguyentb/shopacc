using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.TinTucArea.Models
{
	public class EditVM
	{
		public long Id { get; set; }

		public string Slug { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public string TieuDe { get; set; }

		public string NoiDung { get; set; }
		public string AnhBia { get; set; }
		public string TacGia { get; set; }
		public string TrangThai { get; set; }
		public DateTime ThoiGianXuatBan { get; set; }
		public HttpPostedFileBase FileAnh { get; set; }
	}
}