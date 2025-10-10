using System;
using System.ComponentModel;

namespace Hinet.Service.AppUserService.Dto
{
    public class AppUserExportDto
    {
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [DisplayName("Ngày sinh")]
        public string BirthDay { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}