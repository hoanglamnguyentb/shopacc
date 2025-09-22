using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class BaoCaoStatusConstant
    {
        [DisplayName("Mới tạo báo cáo")]
        [Color(BgColor = "#dfe6e9")]
        public static string MoiTaoBaoCao { get; set; } = "MoiTaoBaoCao";

        [DisplayName("Chờ báo cáo")]
        [Color(BgColor = "#00b894")]
        public static string GuiBaoCaoDenDoiTuong { get; set; } = "GuiBaoCaoDenDoiTuong";

        [DisplayName("Đã báo cáo")]
        [Color(BgColor = "#74b9ff")]
        public static string DaNopKeKhaiBaoCao { get; set; } = "DaNopKeKhaiBaoCao";

        [DisplayName("Cần chỉnh sửa, bổ sung")]
        [Color(BgColor = "#81ecec")]
        public static string YeuCauChinhSuaKeKhaiBaoCao { get; set; } = "YeuCauChinhSuaKeKhaiBaoCao";

        [DisplayName("Từ chối duyệt")]
        [Color(BgColor = "#d63031")]
        public static string TuChoiBaoCaoDaKeKhai { get; set; } = "TuChoiBaoCaoDaKeKhai";

        [DisplayName("Đã duyệt")]
        [Color(BgColor = "#55efc4")]
        public static string PheDuyetKeKhaiBaoCao { get; set; } = "PheDuyetKeKhaiBaoCao";
    }
}