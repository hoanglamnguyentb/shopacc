using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class HoSoUngVienStatusConstant
    {
        [DisplayName("Mới tạo")]
        [Color(BgColor = "#bdc3c7")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName("Hồ sơ đã tiếp nhận")]
        [Color(BgColor = "#2980b9")]
        public static string HoSoTiepNhan { get; set; } = "HoSoTiepNhan";

        [DisplayName("Hồ sơ không được nhận")]
        [Color(BgColor = "#d15b47")]
        public static string HoSoKhongTiepNhan { get; set; } = "HoSoKhongTiepNhan";

        //[DisplayName("Nhân viên thử việc")]
        //[Color(BgColor = "#16a085")]
        //public static string NhanVienThuViec { get; set; } = "NhanVienThuViec";

        //[DisplayName("Nhân viên chính thức")]
        //[Color(BgColor = "#2ecc71")]
        //public static string NhanVienChinhThuc { get; set; } = "NhanVienChinhThuc";
        //[DisplayName("Hồ sơ chờ phỏng vấn")]
        //[Color(BgColor = "#6fb3e0")]
        //public static string HoSoChoPhongVan { get; set; } = "HoSoChoPhongVan";
        //[DisplayName("Hồ sơ không đạt phỏng vấn")]
        //[Color(BgColor = "#d15b47")]
        //public static string HoSoKhongDatPhongVan { get; set; } = "HoSoKhongDatPhongVan";
        //[DisplayName("Hồ sơ thi tuyển")]
        //[Color(BgColor = "#2ecc71")]
        //public static string HoSoThiTuyen { get; set; } = "HoSoThiTuyen";
        //[DisplayName("Hồ sơ không đạt thi tuyển")]
        //[Color(BgColor = "#d15b47")]
        //public static string HoSoKhongDatThiTuyen { get; set; } = "HoSoKhongDatThiTuyen";

        [DisplayName("Hồ sơ đã trúng tuyển")]
        [Color(BgColor = "#2ecc71")]
        public static string HoSoDat { get; set; } = "HoSoDat";

        [DisplayName("Hồ sơ không đạt")]
        [Color(BgColor = "#d15b47")]
        public static string HoSoKhongDat { get; set; } = "HoSoKhongDat";
    }
}