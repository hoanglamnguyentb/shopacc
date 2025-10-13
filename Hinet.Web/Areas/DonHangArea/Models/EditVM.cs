using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.DonHangArea.Models
{
    public class EditVM
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int DonHangId { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int VatPhamId { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int MaGiamGia { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GiaGoc { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int GiaKhuyenMai { get; set; }
		public string TrangThai { get; set; }
		public string QrUrl { get; set; }

        
    }
}