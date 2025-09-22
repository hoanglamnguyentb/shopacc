using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class HoSoBanCungStatusConstant
    {
        [DisplayName("Chưa lưu trữ")]
        public static string ChuaLuuTru { get; set; } = "ChuaLuuTru";

        [DisplayName("Đã lưu trữ")]
        public static string DaLuuTru { get; set; } = "DaLuuTru";

        [DisplayName("Thất lạc")]
        public static string ThatLac { get; set; } = "ThatLac";

        [DisplayName("Đã trả")]
        public static string DaTra { get; set; } = "DaTra";
    }
}