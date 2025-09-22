using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypePhanHoiConstant
    {
        [DisplayName("Nhắc nhở")]
        public static string NhacNho { get; set; } = "NhacNho";

        [DisplayName("Thông báo")]
        public static string ThongBao { get; set; } = "ThongBao";

        [DisplayName("Báo cáo tiến độ")]
        public static string BaoCaoTienDo { get; set; } = "BaoCaoTienDo";

        [DisplayName("Yêu cầu giải ngân")]
        public static string YeuCauGiaiNgan { get; set; } = "BaoCaoTienDo";

        [DisplayName("Phản hồi nhà đầu tư")]
        public static string PhanHoiNhaDauTu { get; set; } = "PhanHoiNhaDauTu";
    }
}