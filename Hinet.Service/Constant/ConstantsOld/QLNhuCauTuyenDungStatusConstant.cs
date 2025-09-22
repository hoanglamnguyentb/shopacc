using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLNhuCauTuyenDungStatusConstant
    {
        [Color(BgColor = "#34495e")]
        [DisplayName("Mới tạo")]
        public static string MoiTao { get; set; } = "MoiTao";

        [Color(BgColor = "#1abc9c")]
        [DisplayName("Đang diễn ra")]
        public static string HoatDong { get; set; } = "HoatDong";

        [DisplayName("Đã kết thúc")]
        [Color(BgColor = "#d15b47")]
        public static string KhongHoatDong { get; set; } = "KhongHoatDong";
    }
}