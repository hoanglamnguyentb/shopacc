using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class FormRegisterConstant
    {
        [DisplayName("Tài khoản cá nhân")]
        public static int Personal { get; set; } = 1;

        [DisplayName("Tài khoản doanh nghiệp")]
        public static int Company { get; set; } = 2;

        [DisplayName("Tài khoản tổ chức")]
        public static int Organization { get; set; } = 3;
    }
}