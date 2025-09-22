using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class HistoryConstant
    {
        [DisplayName("Doanh nghiệp")]
        public static string ThuongNhan { get; set; } = "ThuongNhan";

        [DisplayName("Cá nhân")]
        public static string CaNhan { get; set; } = "CaNhan";

        [DisplayName("Website")]
        public static string Website { get; set; } = "Website";

        [DisplayName("Ứng dụng")]
        public static string UngDung { get; set; } = "UngDung";

        [DisplayName("Báo cáo")]
        public static string BaoCao { get; set; } = "BaoCao";
    }
}