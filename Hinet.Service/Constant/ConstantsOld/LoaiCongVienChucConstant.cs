using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiCongVienChucConstant
    {
        [DisplayName("Công Chức")]
        public static string CongChuc { get; set; } = "CongChuc";

        [DisplayName("Viên Chức")]
        public static string VienChuc { get; set; } = "VienChuc";

        [DisplayName("Hợp đồng lao động")]
        public static string HopDong { get; set; } = "HopDongLaoDong";

        [DisplayName("Hợp đồng 68")]
        public static string HopDong68 { get; set; } = "HopDong68";

        [DisplayName("Học tập")]
        public static string HocTap { get; set; } = "HocTap";
    }
}