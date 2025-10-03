using Hinet.Model.Entities;
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
		public string Code { get; set; }
		public int GameId { get; set; }
		public string TrangThai { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int GiaGoc { get; set; }
		public int GiaKhuyenMai { get; set; }
		public string Mota { get; set; }
		public int ViTri { get; set; }
		public List<TaiLieuDinhKem> TaiLieuDinhKemList { get; set; } = new List<TaiLieuDinhKem>();
        public int? DanhMucGameId { get; set; }
    }
}