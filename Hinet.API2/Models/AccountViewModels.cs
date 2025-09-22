using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class LoginViewModel
    {
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [Display(Name = "Tên đăng nhập")]
        [MinLength(3, ErrorMessage = "Độ dài tối thiểu là 3 ký tự")]
        [MaxLength(50, ErrorMessage = "Độ dài tối đa là 50 ký tự")]
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập ?")]
        public bool RememberMe { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}