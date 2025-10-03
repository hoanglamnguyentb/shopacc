using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.ThuocTinhArea.Models
{
    public class CreateVM
    {
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GameId { get; set; }
		public string TenThuocTinh { get; set; }
		public string KieuDuLieu { get; set; }
		public string DmNhomDanhmuc { get; set; }

        
    }
}