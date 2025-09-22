using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLLoaiCongViecConstant
    {
        [DisplayName("Ngày")]
        [Color(BgColor = "#9b59b6")]
        public static string Ngay { get; set; } = "Ngay";

        [DisplayName("Tuần")]
        [Color(BgColor = "#1abc9c")]
        public static string Tuan { get; set; } = "Tuan";

        [DisplayName("Tháng")]
        [Color(BgColor = "#2ecc71")]
        public static string Thang { get; set; } = "Thang";

        [DisplayName("Quý")]
        [Color(BgColor = "#9b59b6")]
        public static string Quy { get; set; } = "Quy";

        [DisplayName("Năm")]
        [Color(BgColor = "#3498db")]
        public static string Nam { get; set; } = "Nam";

        [DisplayName("Giai đoạn")]
        [Color(BgColor = "#3498db")]
        public static string GiaiDoan { get; set; } = "GiaiDoan";
    }

    public class DmThoiGianThang
    {
        [DisplayName("Tháng 1")]
        public static string Thang1 { get; set; } = "1";

        [DisplayName("Tháng 2")]
        public static string Thang2 { get; set; } = "2";

        [DisplayName("Tháng 3")]
        public static string Thang3 { get; set; } = "3";

        [DisplayName("Tháng 4")]
        public static string Thang4 { get; set; } = "4";

        [DisplayName("Tháng 5")]
        public static string Thang5 { get; set; } = "5";

        [DisplayName("Tháng 6")]
        public static string Thang6 { get; set; } = "6";

        [DisplayName("Tháng 7")]
        public static string Thang7 { get; set; } = "7";

        [DisplayName("Tháng 8")]
        public static string Thang8 { get; set; } = "8";

        [DisplayName("Tháng 9")]
        public static string Thang9 { get; set; } = "9";

        [DisplayName("Tháng 10")]
        public static string Thang10 { get; set; } = "10";

        [DisplayName("Tháng 11")]
        public static string Thang11 { get; set; } = "11";

        [DisplayName("Tháng 12")]
        public static string Thang12 { get; set; } = "12";
    }

    public class DmThoiGianQuy
    {
        [DisplayName("Quý I")]
        public static string QuyI { get; set; } = "I";

        [DisplayName("Quý II")]
        public static string QuyII { get; set; } = "II";

        [DisplayName("Quý III")]
        public static string QuyIII { get; set; } = "III";

        [DisplayName("Quý IV")]
        public static string QuyIV { get; set; } = "IV";
    }
}