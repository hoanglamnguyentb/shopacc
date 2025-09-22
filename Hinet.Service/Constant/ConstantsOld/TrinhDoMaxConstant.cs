using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TrinhDoMaxConstant
    {
        [DisplayName("Sơ cấp")]
        public static string SoCap { get; set; } = "SC";

        [DisplayName("Trung cấp")]
        public static string TrungCap { get; set; } = "TC";

        [DisplayName("Cao đẳng")]
        public static string CaoDang { get; set; } = "CĐ";

        [DisplayName("Cử nhân")]
        public static string CuNhan { get; set; } = "CN";

        [DisplayName("Kỹ sư")]
        public static string KySu { get; set; } = "KS";

        [DisplayName("Thạc sĩ")]
        public static string ThacSi { get; set; } = "THS";

        [DisplayName("Tiến sĩ")]
        public static string TienSi { get; set; } = "TS";

        [DisplayName("Phó giáo sư")]
        public static string PhoGiaoSu { get; set; } = "PGS";

        [DisplayName("Giáo sư")]
        public static string GiaoSu { get; set; } = "GS";

        [DisplayName("Chuyên ngành")]
        public static string ChuyenNganh { get; set; } = "CNG";

        [DisplayName("Chuyên khoa 1")]
        public static string ChuyenKhoa1 { get; set; } = "CK1";

        [DisplayName("Chuyên khoa 2")]
        public static string ChuyenKhoa2 { get; set; } = "CK2";
    }
}