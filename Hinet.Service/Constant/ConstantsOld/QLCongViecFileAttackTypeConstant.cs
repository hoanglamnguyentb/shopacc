using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLCongViecFileAttackTypeConstant
    {
        [DisplayName("Danh mục")]
        [Color(BgColor = "#bdc3c7")]
        public static string DanhMuc { get; set; } = "DanhMuc";

        [DisplayName("Kế hoạch")]
        [Color(BgColor = "#f1c40f")]
        public static string Kehoach { get; set; } = "Kehoach";
    }
}