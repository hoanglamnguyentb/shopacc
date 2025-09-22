using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class WebsiteStatusConstant
    {
        [DisplayName("Website chờ duyệt")]
        [Color(BgColor = "#1F4788")]
        public static int ChoDuyet { get; set; } = 1;

        [DisplayName("Website cần bổ sung thông tin")]
        [Color(BgColor = "#837d9a")]
        public static int CanBoSungThongTin { get; set; } = 6;

        [DisplayName("Website đã duyệt điện tử")]
        [Color(BgColor = "#00b9b4")]
        public static int DaDuyetDienTu { get; set; } = 4;

        [DisplayName("Website đã xác nhận")]
        [Color(BgColor = "#b4b76f")]
        public static int DaXacNhan { get; set; } = 5;

        [DisplayName("Website bị từ chối")]
        [Color(BgColor = "#774a71")]
        public static int BiTuChoi { get; set; } = 3;

        [DisplayName("Website đề nghị chỉnh sửa")]
        [Color(BgColor = "#752929")]
        public static int DeNghiChinhSua { get; set; } = 2;

        [DisplayName("Website đề nghị chấm dứt đăng ký")]
        [Color(BgColor = "#f30991")]
        public static int DeNghiChamDutDangKy { get; set; } = 9;

        [DisplayName("Website đã chấm dứt đăng ký")]
        [Color(BgColor = "#313131")]
        public static int DaChamDutDangKy { get; set; } = 7;

        [DisplayName("Website đã hủy đăng ký")]
        [Color(BgColor = "#7A942E")]
        public static int DaHuyDangKy { get; set; } = 8;

        //[DisplayName("Website đề nghị chấm dứt thông báo")]
        //[Color(BgColor = "#ffff00")]
        //public static int DeNghiChamDutThongBao { get; set; } = 9;
        //[DisplayName("Website đã chấm dứt thông báo")]
        //[Color(BgColor = "#e0e0e0")]
        //public static int DaChamDutThongBao { get; set; } = 10;
        //[DisplayName("Website đã hủy thông báo")]
        //[Color(BgColor = "#ff7043")]
        //public static int DaHuyThongBao { get; set; } = 11;
        [DisplayName("Website đã khóa")]
        [Color(BgColor = "#00897b")]
        public static int DaKhoa { get; set; } = 10;

        [DisplayName("Website chờ gia hạn")]
        [Color(BgColor = "#00897b")]
        public static int ChoGiaHan { get; set; } = 12;

        [DisplayName("Website đã yêu cầu gia hạn")]
        [Color(BgColor = "#00897b")]
        public static int DaYeuCauGiaHan { get; set; } = 11;

        [DisplayName("Website lưu tạm")]
        public static int TamLuu { get; set; } = 0;

        [DisplayName("Website start")]
        public static int BatDau { get; set; } = -3;
    }
}