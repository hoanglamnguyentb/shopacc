using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class HeUngVienTuyenDungConstant
    {
        [DisplayName("Hệ bác sĩ")]
        public static string HEBACSI { get; set; } = "HEBACSI";

        [DisplayName("Hệ cử nhân")]
        public static string HECUNHAN { get; set; } = "HECUNHAN";

        [DisplayName("Bác sĩ nội trú")]
        public static string BSNOITRU { get; set; } = "BSNOITRU";

        [DisplayName("Vị trí khác")]
        public static string VITRIKHAC { get; set; } = "VITRIKHAC";
    }
}