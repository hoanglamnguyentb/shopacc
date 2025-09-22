using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class GuildStepConstant
    {
        [DisplayName("Điền thông tin vào mẫu")]
        public static string DienThongTin { get; set; } = "DienThongTin";

        [DisplayName("Cần bổ sung thông tin")]
        public static string CanBoSungThongTin { get; set; } = "CanBoSungThongTin"; //1

        [DisplayName("Chờ duyệt")]
        public static string ChoDuyet { get; set; } = "ChoDuyet"; //1

        [DisplayName("Đã duyệt điện tử")]
        public static string DaDuyetDienTu { get; set; } = "DaDuyetDienTu"; // 1

        [DisplayName("Đã xác nhận")]
        public static string DaXacNhan { get; set; } = "DaXacNhan"; //

        [DisplayName("Bị từ chối")]
        public static string BiTuChoi { get; set; } = "BiTuChoi"; // 1

        [DisplayName("Đề nghị chỉnh sửa")]
        public static string DeNghiChinhSua { get; set; } = "DeNghiChinhSua"; //1

        [DisplayName("Đề nghị chấm dứt")]
        public static string DeNghiChamDutDangKy { get; set; } = "DeNghiChamDutDangKy"; //1

        [DisplayName("Đã chấm dứt")]
        public static string DaChamDutDangKy { get; set; } = "DaChamDutDangKy"; //1

        [DisplayName("Đã hủy đăng ký")]
        public static string DaHuyDangKy { get; set; } = "DaHuyDangKy"; //1

        [DisplayName("Đã khóa")]
        public static string DaKhoa { get; set; } = "DaKhoa"; //1

        [DisplayName("Chờ gia hạn")]
        public static string ChoGiaHan { get; set; } = "ChoGiaHan"; //

        [DisplayName("Đã yêu cầu gia hạn")]
        public static string DaYeuCauGiaHan { get; set; } = "DaYeuCauGiaHan"; //

        [DisplayName("------THƯƠNG NHÂN------")]
        public static string TaiKhoanThuongNhan { get; set; } = "";

        [DisplayName("Tài khoản mới đăng ký")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName("Tài khoản đã duyệt")]
        public static string DaDuyet { get; set; } = "DaDuyet";

        [DisplayName("Tài khoản cần bổ sung thông tin")]
        public static string BoSung { get; set; } = "BoSung";

        [DisplayName("Tài khoản đề nghị chỉnh sửa")]
        public static string ChinhSua { get; set; } = "ChinhSua";

        [DisplayName("Tài khoản bị từ chối")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName("Tài khoản bị khóa")]
        public static string BijKhoa { get; set; } = "BijKhoa";
    }
}