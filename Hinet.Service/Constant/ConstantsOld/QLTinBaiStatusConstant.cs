using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLTinBaiStatusConstant
    {
        [DisplayName("Lưu tạm")]
        [Color(BgColor = "#bbdefb")]
        public static int LuuTam { get; set; } = 0;

        [DisplayName("Chờ duyệt")]
        [Color(BgColor = "#bbdefb")]
        public static int ChoDuyet { get; set; } = 1;

        [DisplayName("Yêu cầu bổ sung")]
        [Color(BgColor = "#bbdefb")]
        public static int YeuCauBoSung { get; set; } = 2;

        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#bbdefb")]
        public static int DaDuyet { get; set; } = 3;

        [DisplayName("Bị từ chối")]
        [Color(BgColor = "#bbdefb")]
        public static int BiTuChoi { get; set; } = 4;
    }
}