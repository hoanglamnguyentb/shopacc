using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LichCongTacStatusConstant
    {
        [DisplayName("Tạo mới")]
        [Color(BgColor = "#95a5a6")]
        public static int MoiTao { get; set; } = 1;

        [DisplayName("Đăng ký")]
        [Color(BgColor = "#3498db")]
        public static int GuiDuyet { get; set; } = 2;

        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#27ae60")]
        public static int DaDuyet { get; set; } = 3;

        [DisplayName("Từ chối")]
        [Color(BgColor = "#e67e22")]
        public static int TuChoi { get; set; } = 4;

        [DisplayName("Hủy bỏ")]
        [Color(BgColor = "#2c3e50")]
        public static int Huy { get; set; } = 5;

        [DisplayName("Trình lãnh đạo")]
        [Color(BgColor = "#2c3e50")]
        public static int TrinhLanhDao { get; set; } = 6;
    }
}