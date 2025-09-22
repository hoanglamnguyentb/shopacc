using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class CacLoaiDichVuConstant
    {
        [DisplayName("Dịch vụ bưu phẩm")]
        public static string DichVuBuuPham { get; set; } = "DichVuBuuPham";

        [DisplayName("Dịch vụ trong nước và ngoài nước")]
        public static string DichVuTrongNuocVaNgoaiNuoc { get; set; } = "DichVuTrongNuocVaNgoaiNuoc";

        [DisplayName("Dịch vụ tài chính bưu chính")]
        public static string DichVuTaiChinhBuuChinh { get; set; } = "DichVuTaiChinhBuuChinh";

        [DisplayName("Dịch vụ điện hoa")]
        public static string DichVuDienHoa { get; set; } = "DichVuDienHoa";

        [DisplayName("Dịch vụ phát hành báo chí")]
        public static string DichVuPhatHanhBaoChi { get; set; } = "DichVuPhatHanhBaoChi";

        [DisplayName("Dịch vụ chuyển phát nhanh EMS")]
        public static string DichVuChuyenPhatNhanhEms { get; set; } = "DichVuChuyenPhatNhanhEms";

        [DisplayName("Dịch vụ đặc biệt cho bưu phẩm, bưu kiện trong nước và quốc tế")]
        public static string DichVuDacBietChoBuuPhamBuuKienTrongNuocVaQuocTe { get; set; } = "DichVuDacBietChoBuuPhamBuuKienTrongNuocVaQuocTe";
    }
}