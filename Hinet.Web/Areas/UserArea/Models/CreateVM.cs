using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.UserArea.Models
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string FullName { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [RegularExpression(@"^(\+84|0)\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { get; set; }

        public int Gender { get; set; }
        public string Address { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long? IdDepartment { get; set; }

        public int? ChucVuId { get; set; }
        public string IdChucVu { get; set; }
        public List<string> Province { get; set; }
        public int TypeDashboard { get; set; }
        public string Facebook { get; set; }
        public string Skype { get; set; }
        public string Yahoo { get; set; }

        //public bool IsAllProvine { get; set; }
        //public bool IsAllLinhVuc { get; set; }
        //public List<long> lstLinhVuc { get; set; }
        public bool IsCongTacVien { get; set; }

        public bool IsTruongPhongDepartment { get; set; }
        public long? RoleMobileId { get; set; }
        public long? PhongBanId { get; set; }
        public long? IdDoiTuong { get; set; }
        public string TypeAccount { get; set; }
        public List<string> lstHuyenQL { get; set; }
        public List<string> lstXaQL { get; set; }
    }
}