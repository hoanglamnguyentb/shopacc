using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DepartmentStatusConstant
    {
        [DisplayName("Hoạt động")]
        public static string HoatDong { get; set; } = "HoatDong";

        [DisplayName("Đã giải thể")]
        public static string DaGiaiThe { get; set; } = "DaGiaiThe";
    }
}