using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TaiSanHuuHinhVoHinhStatusConstant
    {
        [DisplayName("Tài sản hữu hình")]
        [Color(BgColor = "#3498db")]
        public static string TaiSanHuuHinh { get; set; } = "TaiSanHuuHinh";

        [DisplayName("Tài sản vô hình")]
        [Color(BgColor = "#2ecc71")]
        public static string TaiSanVoHinh { get; set; } = "TaiSanVoHinh";
    }
}