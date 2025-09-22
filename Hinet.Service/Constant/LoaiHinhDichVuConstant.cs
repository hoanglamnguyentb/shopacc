using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiHinhDichVuConstant
    {
        [DisplayName("Bưu cục")]
        public static string BuuCuc { get; set; } = "BuuCuc";

        [DisplayName("Điểm BDVHX")]
        public static string DiemBDVHX { get; set; } = "DiemBDVHX";

        [DisplayName("Địa điểm kinh doanh")]
        public static string DiaDiemKinhDoanh { get; set; } = "DiaDiemKinhDoanh";

        [DisplayName("Chi nhánh")]
        public static string ChiNhanh { get; set; } = "ChiNhanh";

        [DisplayName("Văn phòng đại diện")]
        public static string VanPhongDaiDien { get; set; } = "VanPhongDaiDien";
    }
}