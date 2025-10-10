using System;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.DepartmentArea.Models
{
    public class GiaiTheVM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
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