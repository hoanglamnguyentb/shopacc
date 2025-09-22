using Hinet.Service.Common;
using System.ComponentModel;
using System.Drawing;

namespace Hinet.Service.Constant
{
    public class LichTrucConstant
    {
        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#bdc3c7")]
        public static int DaDuyet { get; set; } = 1;

        [DisplayName("Chưa duyệt")]
        [Color(BgColor = "#f1c40f")]
        public static int ChuaDuyet { get; set; } = 2;
    }
}