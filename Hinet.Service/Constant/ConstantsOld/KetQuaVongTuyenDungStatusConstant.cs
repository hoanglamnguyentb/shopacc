using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class KetQuaVongTuyenDungStatusConstant
    {
        [DisplayName("Đạt")]
        [Color(BgColor = "#27ae60")]
        public static string Dat { get; set; } = "Dat";

        [DisplayName("Không đạt")]
        [Color(BgColor = "#e67e22")]
        public static string KhongDat { get; set; } = "KhongDat";
    }
}