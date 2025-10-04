using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.TaiKhoanArea.Models
{
    public class CreateVM
    {
		public string Code { get; set; }
		public int? GameId { get; set; }
		public string TrangThai { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int GiaGoc { get; set; }
		public int GiaKhuyenMai { get; set; }
		public string Mota { get; set; }
		public int ViTri { get; set; }
        public int? DanhMucGameId { get; set; }
        public List<ThuocTinh> ThuocTinhs { get; set; }
        public List<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; } = new List<GiaTriThuocTinh>();
    }
}