using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.TaiKhoanService.Dto
{
    public class TaiKhoanDto : TaiKhoan
    {
        public Game Game { get; set; }
        public DanhMucGame DanhMucGame { get; set; }
        public string DanhMucGameTxt { get; set; }
        public List<TaiLieuDinhKem> TaiLieuDinhKemList { get; set; } = new List<TaiLieuDinhKem>();
        public string AnhBia { get; set; }
    }
}