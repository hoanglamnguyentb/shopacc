using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLLoaiBaoConstant
    {
        [DisplayName("Báo in")]
        [Color(BgColor = "#81c784")]
        public static string BaoIn { get; set; } = "BaoIn";

        [DisplayName("Báo điện tử")]
        [Color(BgColor = "#00e676")]
        public static string BaoDienTu { get; set; } = "BaoDienTu";
    }
}