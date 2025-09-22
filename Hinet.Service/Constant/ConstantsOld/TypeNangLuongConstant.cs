using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeNangLuongConstant
    {
        [DisplayName("Trước thời hạn")]
        public static string TruocThoiHan { get; set; } = "Trước thời hạn";

        [DisplayName("Thường xuyên")]
        public static string ThuongXuyen { get; set; } = "Thường xuyên";

        [DisplayName("Vượt khung")]
        public static string VuotKhung { get; set; } = "Vượt khung";
    }
}