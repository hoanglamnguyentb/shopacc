using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DanhMucVeHuuConstant
    {
        [DisplayName("Sắp về hưu")]
        public static string SapVeHuu { get; set; } = "SapVeHuu";

        [DisplayName("Đã về hưu")]
        public static string DaVeHuu { get; set; } = "DaVeHuu";
    }

    public class TuoiNghiHuuConstant
    {
        [DisplayName("Tuổi nghỉ hưu nam")]
        public static string TUOINGHIHUU_NAM { get; set; } = "TUOINGHIHUU_NAM";

        [DisplayName("Tuổi nghỉ hưu nữ")]
        public static string TUOINGHIHUU_NU { get; set; } = "TUOINGHIHUU_NU";
    }

    public class IsActiveLamViecOrNghiViecConstant
    {
        [DisplayName("Đang làm việc")]
        public static string DangLamViec { get; set; } = "DangLamViec";

        [DisplayName("Đã Nghỉ việc")]
        public static string DaNghiViec { get; set; } = "DaNghiViec";
    }
}