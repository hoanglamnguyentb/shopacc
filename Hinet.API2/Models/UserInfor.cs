using System;
using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models
{
    public class UserInfor
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }

    public class UserRegister
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }
    }
}