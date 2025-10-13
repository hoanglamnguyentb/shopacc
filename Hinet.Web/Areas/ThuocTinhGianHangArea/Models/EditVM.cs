using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.ThuocTinhGianHangArea.Models
{
    public class EditVM
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GianHangId { get; set; }
		public long? NhomDanhMucId { get; set; }
		public string TenThuocTinh { get; set; }
		public string KieuDuLieu { get; set; }
		public string NhomDanhmucCode { get; set; }

        
    }
}