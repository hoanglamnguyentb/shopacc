using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.BinhLuanArea.Models
{
    public class CreateVM
    {
        public long NguoiBinhLuanId { get; set; }
        public long DoiTuongId { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string NoiDung { get; set; }
        public int? Diem { get; set; }
        public long ParentId { get; set; }
        public string TrangThai { get; set; }
    }
}