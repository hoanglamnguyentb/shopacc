using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class AccountTypeConstant
    {
        [DisplayName("Tài khoản cá nhân/ thương nhân")]
        public static string EndUser { get; set; } = "EndUser";

        [DisplayName("Tài khoản xử lý nghiệp vụ")]
        public static string BussinessUser { get; set; } = "BussinessUser";

        [DisplayName("Tài khoản nhập dữ liệu của cơ sở xăng dầu")]
        public static string NvXang { get; set; } = "NvXang";
    }

    public class OrganizationTypeConstant
    {
        [DisplayName("Cá nhân")]
        public static string Personal { get; set; } = "Personal";

        [DisplayName("Thương nhân")]
        public static string Company { get; set; } = "Company";

        [DisplayName("Tổ chức")]
        public static string Organization { get; set; } = "Organization";
    }
}