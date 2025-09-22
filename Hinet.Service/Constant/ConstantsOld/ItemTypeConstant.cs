using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ItemTypeConstant
    {
        [DisplayName("Hồ sơ khiếu nại tố cáo")]
        public static string TTHoSoGiaiQuyetKhieuNaiToCao { get; set; } = "TTHoSoGiaiQuyetKhieuNaiToCao";

        [DisplayName("Góp ý và thẩm tra dự án đầu tư")]
        public static string GopYThamTraDuAN { get; set; } = "GopYThamTraDuAN";

        [DisplayName("Hồ sơ thanh tra")]
        public static string TTHoSoThanhTra { get; set; } = "TTHoSoThanhTra";

        [DisplayName("Hồ sơ báo cáo")]
        public static string TTHoSoBaoCaoThanhTra { get; set; } = "TTHoSoBaoCaoThanhTra";

        [DisplayName("Đối tác quốc tế")]
        public static string DoiTacQuocTe { get; set; } = "DoiTacQuocTe";

        [DisplayName("Khoa phòng")]
        public static string Department { get; set; } = "Department";

        [DisplayName("Thông tin hợp tác quốc tế")]
        public static string TTHTQuocTe { get; set; } = "TTHTQuocTe";

        [DisplayName("Doanh nghiệp KHCN")]
        public static string DoanhNghiepKhcn { get; set; } = "DoanhNghiepKhcn";

        [DisplayName("Hợp đồng chuyển giao công nghệ")]
        public static string HopDongChuyenGiaoCongNghe { get; set; } = "HopDongChuyenGiaoCongNghe";

        [DisplayName("Sản phẩm khoa học công nghệ")]
        public static string DoanhNghiepKHCNSanPham { get; set; } = "DoanhNghiepKHCNSanPham";

        [DisplayName("Doanh thu hàng năm doanh nghiệp")]
        public static string DoanhThuHangNamDoanhNghiep { get; set; } = "DoanhThuHangNamDoanhNghiep";
    }
}