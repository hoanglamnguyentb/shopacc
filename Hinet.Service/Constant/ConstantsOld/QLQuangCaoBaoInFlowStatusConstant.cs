using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QLQuangCaoBaoInFlowStatusConstant
    {
        [DisplayName("Mới tạo")]
        [Color(BgColor = "#777")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName("Gửi kỹ thuật")]
        [Color(BgColor = "#d15b47")]
        public static string DaGuiKyThuat { get; set; } = "DaGuiKyThuat";

        [DisplayName("Kỹ thuật trả kết quả")]
        [Color(BgColor = "#bbdefb")]
        public static string KyThuatTraKetQua { get; set; } = "KyThuatTraKetQua";

        //[DisplayName("Kỹ thuật từ chối")]
        //public static string KyThuatTuChoi { get; set; } = "KyThuatTuChoi";

        [DisplayName("Gửi thư ký")]
        [Color(BgColor = "#337ab7")]
        public static string DaGuiThuKy { get; set; } = "DaGuiThuKy";

        [DisplayName("Thư ký trả về")]
        [Color(BgColor = "#999")]
        public static string ThuKyTraVe { get; set; } = "ThuKyTraVe";

        [Color(BgColor = "#56b23f")]
        [DisplayName("Thư Ký duyệt")]
        public static string ThuKyDaDuyet { get; set; } = "ThuKyDaDuyet";
    }
}