using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class AppStatusConstant
    {
        [DisplayName("Ứng dụng chờ duyệt")]
        [Color(BgColor = "#bbdefb")]
        public static int ChoDuyet { get; set; } = 1;

        [DisplayName("Ứng dụng cần bổ sung thông tin")]
        [Color(BgColor = "#a5d6a7")]
        public static int CanBoSungThongTin { get; set; } = 6;

        [DisplayName("Ứng dụng đã duyệt điện tử")]
        [Color(BgColor = "#81c784")]
        public static int DaDuyetDienTu { get; set; } = 4;

        [DisplayName("Ứng dụng đã xác nhận")]
        [Color(BgColor = "#00e676")]
        public static int DaXacNhan { get; set; } = 5;

        [DisplayName("Ứng dụng bị từ chối")]
        [Color(BgColor = "#ff7043")]
        public static int BiTuChoi { get; set; } = 3;

        [DisplayName("Ứng dụng đề nghị chỉnh sửa")]
        [Color(BgColor = "#fff176")]
        public static int DeNghiChinhSua { get; set; } = 2;

        [DisplayName("Ứng dụng đề nghị chấm dứt đăng ký")]
        [Color(BgColor = "#ffff00")]
        public static int DeNghiChamDutDangKy { get; set; } = 9;

        [DisplayName("Ứng dụng đã chấm dứt đăng ký")]
        [Color(BgColor = "#e0e0e0")]
        public static int DaChamDutDangKy { get; set; } = 7;

        [DisplayName("Ứng dụng đã hủy đăng ký")]
        [Color(BgColor = "#ff7043")]
        public static int DaHuyDangKy { get; set; } = 8;

        //[DisplayName("Ứng dụng đề nghị chấm dứt thông báo")]
        //[Color(BgColor = "#ffff00")]
        //public static int DeNghiChamDutThongBao { get; set; } = 9;
        //[DisplayName("Ứng dụng đã chấm dứt thông báo")]
        //[Color(BgColor = "#e0e0e0")]
        //public static int DaChamDutThongBao { get; set; } = 10;
        //[DisplayName("Ứng dụng đã hủy thông báo")]
        //[Color(BgColor = "#ff7043")]
        //public static int DaHuyThongBao { get; set; } = 11;
        [DisplayName("Ứng dụng đã khóa")]
        [Color(BgColor = "#00897b")]
        public static int DaKhoa { get; set; } = 10;

        [DisplayName("Ứng dụng chờ gia hạn")]
        [Color(BgColor = "#00897b")]
        public static int ChoGiaHan { get; set; } = 12;

        [DisplayName("Ứng dụng đã yêu cầu gia hạn")]
        [Color(BgColor = "#00897b")]
        public static int DaYeuCauGiaHan { get; set; } = 11;

        [DisplayName("Ứng dụng lưu tạm")]
        public static int TamLuu { get; set; } = 0;

        [DisplayName("Ứng dụng start")]
        public static int BatDau { get; set; } = -3;
    }
}