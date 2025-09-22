using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ProductStatusConstant
    {
        [DisplayName("Hồ sơ lưu tạm")]
        [Color(BgColor = "#bdc3c7")]//sunflower
        public static int LuuTam { get; set; } = 0;

        [DisplayName("Hồ sơ chờ duyệt")]
        [Color(BgColor = "#f39c12")]//sunflower
        public static int ChoDuyet { get; set; } = 1;

        [DisplayName("Hồ sơ cần bổ sung thông tin")]
        [Color(BgColor = "#1abc9c")]//turquoise
        public static int CanBoSungThongTin { get; set; } = 6;

        [DisplayName("Hồ sơ đã duyệt điện tử")]
        [Color(BgColor = "#2ecc71")]//emerald
        public static int DaDuyetDienTu { get; set; } = 4;

        [DisplayName("Hồ sơ bị từ chối")]
        [Color(BgColor = "#c0392b")]//pomegranate
        public static int BiTuChoi { get; set; } = 3;

        [DisplayName("Hồ sơ đề nghị chỉnh sửa")]
        [Color(BgColor = "#0984e3")]//electron blue
        public static int DeNghiChinhSua { get; set; } = 2;

        [DisplayName("Hồ sơ đề nghị gỡ bỏ")]
        [Color(BgColor = "#d35400")]//pumpkin
        public static int ChamDutDangKy { get; set; } = 9;

        [DisplayName("Hồ sơ đã gỡ bỏ")]
        [Color(BgColor = "#2c3e50")]//midnight blue
        public static int DaChamDutDangKy { get; set; } = 7;

        [DisplayName("Hồ sơ đã hủy đăng ký")]
        [Color(BgColor = "#2d3436")]//dracula orchid
        public static int DaHuyDangKy { get; set; } = 8;
    }
}