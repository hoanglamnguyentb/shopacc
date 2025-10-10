using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.GiaoDichArea.Models
{
    public class CreateVM
    {
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public long UserId { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public long DoiTuongId { get; set; }
		public string LoaiDoiTuong { get; set; }
		public string LoaiGiaoDich { get; set; }
		public string TrangThai { get; set; }
		public string PhuongThucThanhToan { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public DateTime NgayGiaoDich { get; set; }
		public DateTime? NgayThanhToan { get; set; }

        
    }
}