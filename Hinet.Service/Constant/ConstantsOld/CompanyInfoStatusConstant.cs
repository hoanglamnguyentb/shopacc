using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class CompanyInfoStatusConstant
    {
        [DisplayName("Tài khoản mới đăng ký")]
        [Color(BgColor = "#bbdefb")]
        public static int MoiTao { get; set; } = 0;

        [DisplayName("Tài khoản đã duyệt")]
        [Color(BgColor = "#00e676")]
        public static int DaDuyet { get; set; } = 1;

        [DisplayName("Tài khoản cần bổ sung thông tin")]
        [Color(BgColor = "#a5d6a7")]
        public static int BoSung { get; set; } = 2;

        [DisplayName("Tài khoản đề nghị chỉnh sửa")]
        [Color(BgColor = "#fff176")]
        public static int ChinhSua { get; set; } = 3;

        [DisplayName("Tài khoản bị từ chối")]
        [Color(BgColor = "#ff7043")]
        public static int TuChoi { get; set; } = 4;

        [DisplayName("Tài khoản bị khóa")]
        [Color(BgColor = "#ff7043")]
        public static int BijKhoa { get; set; } = 5;

        [DisplayName("Tài khoản đề nghị chấm dứt")]
        [Color(BgColor = "#ff7043")]
        public static int DeNghiChamDut { get; set; } = 6;

        [DisplayName("Tài khoản đã chấm dứt")]
        [Color(BgColor = "#ff7043")]
        public static int DaChamDut { get; set; } = 7;

        [DisplayName("Tài khoản đã xác nhận")]
        [Color(BgColor = "#00e676")]
        public static int DaXacNhan { get; set; } = 8;
    }
}