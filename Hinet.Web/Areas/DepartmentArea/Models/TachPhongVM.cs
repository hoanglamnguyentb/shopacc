using System;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.DepartmentArea.Models
{
    public class PhongBanNewVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Code { get; set; }

        public string Loai { get; set; }

        public long? ParentId { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public int? DefaultRole { get; set; }
    }

    public class TachPhongVM
    {
        public long Id { get; set; }
        public PhongBanNewVM phongBanNewVM { get; set; }

        //public List<string> lstChucVu { get; set; }
        //public List<long> lstLyLich{ get; set; }
        public ThongTinQuyetDinhVM thongTinQuyetDinhVM { get; set; }
    }

    public class ThongTinQuyetDinhVM
    {
        public string SoQuyetDinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public DateTime? NgayQuyetDinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string NguoiKy { get; set; }

        public string GhiChu { get; set; }
    }
}