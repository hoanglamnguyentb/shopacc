using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class CheckExistItemTypeChamCong
    {
        [DisplayName("OT")]
        [Color(BgColor = "#3498db")]
        public static string DiLamNgoaiGio { get; set; } = "OT";

        [DisplayName("TK")]
        [Color(BgColor = "#3498db")]
        public static string TrucKhoa { get; set; } = "TK";

        [DisplayName("THS")]
        [Color(BgColor = "#3498db")]
        public static string TrucHoiSuc { get; set; } = "THS";

        [DisplayName("TTT")]
        [Color(BgColor = "#3498db")]
        public static string TrucThuongTru { get; set; } = "TTT";

        [DisplayName("Làm đêm")]
        [Color(BgColor = "#3498db")]
        public static string LamDem { get; set; } = "Đ";
    }
}