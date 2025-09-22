using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TrangThaiHoatDongDoanhNghiepConstant
    {
        [DisplayName("Đang hoạt động")]
        [Color(Color = "#198754", Icon = "fa fa-check-circle")]
        public static string DangHoatDong { get; set; } = "60558";

        [DisplayName("Tạm ngưng")]
        [Color(Color = "#ff5a83", Icon = "fa fa-warning")]
        public static string TamNgung { get; set; } = "60559";

        [DisplayName("Dừng hoạt động")]
        [Color(Color = "#FD8B51", Icon = "fa fa-close")]
        public static string DungHoatDong { get; set; } = "60560";
    }
}