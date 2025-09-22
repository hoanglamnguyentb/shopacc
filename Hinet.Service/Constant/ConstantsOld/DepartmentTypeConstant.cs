using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DepartmentTypeConstant
    {
        [DisplayName("Khối")]
        public static string Khoi { get; set; } = "Khoi";

        [DisplayName("Ban")]
        public static string Ban { get; set; } = "Ban";

        [DisplayName("Khoa")]
        public static string Khoa { get; set; } = "Khoa";

        [DisplayName("Phòng")]
        public static string Phong { get; set; } = "Phong";

        [DisplayName("Đơn nguyên")]
        public static string DonNguyen { get; set; } = "DonNguyen";

        [DisplayName("Đơn vị")]
        public static string DonVi { get; set; } = "DonVi";

        [DisplayName("Sở ban ngành")]
        public static string SoBanNganh { get; set; } = "SoBanNganh";

        [DisplayName("Quận/Huyện")]
        public static string QuanHuyen { get; set; } = "QuanHuyen";

        [DisplayName("Phường/Xã")]
        public static string PhuongXa { get; set; } = "PhuongXa";
    }
}