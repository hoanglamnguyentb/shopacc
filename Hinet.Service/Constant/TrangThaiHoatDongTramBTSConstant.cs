using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TrangThaiHoatDongTramBTSConstant
    {


        [DisplayName("Hoạt động tốt")]
        public static string HOATDONGTOT { get; set; } = "HOATDONGTOT";

        [DisplayName("Không hoạt động")]
        public static string KHONGHOATDONG { get; set; } = "KHONGHOATDONG";

        [DisplayName("Sửa chữa")]
        public static string SUACHUA { get; set; } = "SUACHUA";

        [DisplayName("Đang bảo trì")]
        public static string DANGBAOTRI { get; set; } = "DANGBAOTRI";
    }

    public class DonViBanKinhConstant
    {
        [DisplayName("m")]
        public static string m { get; set; } = "m";

        [DisplayName("Km")]
        public static string Km { get; set; } = "Km";
    }

    public class CongNghePhatSongConstant
    {
        [DisplayName("2G")]
        public static string G2 { get; set; } = "2G";

        [DisplayName("3G")]
        public static string G3 { get; set; } = "3G";

        [DisplayName("4G")]
        public static string G4 { get; set; } = "4G";

        [DisplayName("5G")]
        public static string G5 { get; set; } = "5G";
    }
}