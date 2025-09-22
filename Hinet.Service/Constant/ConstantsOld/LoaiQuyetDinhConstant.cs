using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiQuyetDinhConstant
    {
        [DisplayName("Quyết định chủ trương đầu tư")]
        [Color(BgColor = "#d15b47")]
        public static string QDChuTruongDauTu { get; set; } = "QDChuTruongDauTu";

        [DisplayName("Quyết định đầu tư")]
        [Color(BgColor = "#d15b47")]
        public static string QDDauTu { get; set; } = "QDDauTu";
    }
}