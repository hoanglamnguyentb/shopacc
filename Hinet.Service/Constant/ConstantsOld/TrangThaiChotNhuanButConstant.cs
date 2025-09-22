using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TrangThaiChotNhuanButConstant
    {
        [DisplayName("Bản thảo")]
        [Color(BgColor = "#95a5a6")]
        public static string BanThao { get; set; } = "BanThao";

        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#16a085")]
        public static string DaDuyet { get; set; } = "DaDuyet";
    }
}