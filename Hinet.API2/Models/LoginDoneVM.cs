using Hinet.Service.AppUserService.Dto;
using System;

namespace Hinet.API2.Models
{
    public class LoginDoneVM
    {
        public DateTime TimeoutToken { get; set; }
        public string Token { get; set; }
        public AppUserVM_Api AccountInfo { get; set; }

        public LoginDoneVM()
        {
            AccountInfo = new AppUserVM_Api();
        }
    }
}