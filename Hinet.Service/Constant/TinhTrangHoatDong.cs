using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TinhTrangHoatDong
    {
        [DisplayName("Đang hoạt động")]
        public static string DangHoatDong { get; set; } = "DangHoatDong";

        [DisplayName("Tạm ngừng hoạt động ")]
        public static string TamDungHoatDong { get; set; } = "TamDungHoatDong";

        [DisplayName("Ngừng hoạt động, chờ giải thể ")]
        public static string NgungHoatDong { get; set; } = "NgungHoatDong";

        //[DisplayName("Giải thể, phá sản ")]
        //public static string GiaiThe { get; set; } = "GiaiThe";

        //[DisplayName("Không có doanh thu, không có chi phí SXKD ")]
        //public static string KhongDoanhThu { get; set; } = "KhongDoanhThu";
    }
}