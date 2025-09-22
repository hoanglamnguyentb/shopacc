using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ChamCongStatusConstant
    {
        [DisplayName("Nghỉ làm")]
        [Color(BgColor = "#e74c3c")]
        public static string NghiLam { get; set; } = "NghiLam";

        [DisplayName("Đã chấm công")]
        [Color(BgColor = "#3498db")]
        public static string DaChamCong { get; set; } = "DaChamCong";
    }
}