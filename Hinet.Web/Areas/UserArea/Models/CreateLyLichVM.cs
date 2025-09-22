using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.UserArea.Models
{
    public class CreateLyLichVM
    {
        public long IdLylich { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [MinLength(6, ErrorMessage = "Tối thiểu 6 ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}