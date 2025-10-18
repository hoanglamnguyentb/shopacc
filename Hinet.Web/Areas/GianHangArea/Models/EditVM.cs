using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.GianHangArea.Models
{
    public class EditVM
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
		public int STT { get; set; }
		public string Name { get; set; }
        public string LuuY { get; set; }
        public string MoTa { get; set; }
		public string ViTriHienThi { get; set; }
		public string Slug { get; set; }
		public string AnhBia { get; set; }
        public HttpPostedFileBase FileAnh { get; set; }
        public List<ThuocTinhGianHang> ThuocTinhs { get; set; } = new List<ThuocTinhGianHang>();
        public bool? KichHoat { get; set; }


    }
}