using System.ComponentModel;

namespace Hinet.Service.Constant.TramQuanTrac
{
    public class TrangThaiPheDuyetTramQuanTrac
    {
        [DisplayName("Yêu cầu kết nối")]
        public static string GuiPheDuyet { get; set; } = "GuiPheDuyet";

        [DisplayName("Thẩm định kết nối")]
        public static string ThamDinh { get; set; } = "ThamDinh";

        [DisplayName("Phê duyệt")]
        public static string PheDuyet { get; set; } = "PheDuyet";

        [DisplayName("Từ chối kết nối")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName("Yêu cầu bổ sung thông tin kết nối")]
        public static string YeuCauBoSung { get; set; } = "YeuCauBoSung";
    }
}