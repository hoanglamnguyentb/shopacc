using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ArticleStepConstant
    {
        [DisplayName("Bản thảo")]
        [Color(BgColor = "#e21717")]
        public static int Nhap { get; set; } = 1;

        [DisplayName("Gửi thư ký")]
        [Color(BgColor = "#1798e2")]
        public static int GuiThuKy { get; set; } = 2;

        [DisplayName("Trả về phóng viên")]
        [Color(BgColor = "#dcc100")]
        public static int TraVePhongVien { get; set; } = 3;

        [DisplayName("Trình lãnh đạo")]
        [Color(BgColor = "#00b52f")]
        public static int TrinhLanhDao { get; set; } = 4;

        [DisplayName("Trả về phóng viên")]
        [Color(BgColor = "#FF9800")]
        public static int TraVeThuKy { get; set; } = 5;

        [DisplayName("Lãnh đạo đã duyệt")]
        [Color(BgColor = "#00a707")]
        public static int LanhDaoDuyet { get; set; } = 6;

        [DisplayName("Trả về phóng viên")]
        [Color(BgColor = "#d4ba00")]
        public static int LanhDao_KhongDuyet { get; set; } = 7;
    }
}