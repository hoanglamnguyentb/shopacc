using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DGTNStatusConstant
    {
        [DisplayName("Hồ sơ tạo mới")]
        [Color(BgColor = "#bbdefb")]
        public static int HoSoTaoMoi { get; set; } = 0;

        [DisplayName("Hồ sơ chờ duyệt")]
        [Color(BgColor = "#bbdefb")]
        public static int HoSoChoDuyet { get; set; } = 1;

        [DisplayName("Hồ sơ cần bổ sung thông tin")]
        [Color(BgColor = "#a5d6a7")]
        public static int HoSoCanBoSungThongTin { get; set; } = 6;

        [DisplayName("Hồ sơ đã duyệt điện tử")]
        [Color(BgColor = "#81c784")]
        public static int HoSoDaDuyetDienTu { get; set; } = 4;

        [DisplayName("Hồ sơ đã xác nhận")]
        [Color(BgColor = "#00e676")]
        public static int HoSoDaXacNhan { get; set; } = 5;

        [DisplayName("Hồ sơ bị từ chối")]
        [Color(BgColor = "#ff7043")]
        public static int HoSoBiTuChoi { get; set; } = 3;

        [DisplayName("Hồ sơ đề nghị chỉnh sửa")]
        [Color(BgColor = "#fff176")]
        public static int HoSoDeNghiChinhSua { get; set; } = 2;

        [DisplayName("Hồ sơ đề nghị chấm dứt đăng ký")]
        [Color(BgColor = "#ffff00")]
        public static int HoSoDeNghiChamDutDangKy { get; set; } = 9;

        [DisplayName("Hồ sơ đã chấm dứt đăng ký")]
        [Color(BgColor = "#e0e0e0")]
        public static int HoSoDaChamDutDangKy { get; set; } = 7;

        [DisplayName("Hồ sơ đã hủy đăng ký")]
        [Color(BgColor = "#ff7043")]
        public static int HoSoDaHuyDangKy { get; set; } = 8;

        [DisplayName("Hồ sơ lưu tạm")]
        public static int HoSoLuuTam { get; set; } = -1;
    }
}