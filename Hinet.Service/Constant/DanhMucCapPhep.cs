using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DanhMucCapPhep
    {
        [DisplayName("Giấy phép kinh doanh")]
        public static string GIAYPHEPKINHDOANH { get; set; } = "GIAYPHEPKINHDOANH";

        [DisplayName("Đăng ký hoạt động bán hàng đa cấp")]
        public static string DANGKYHOATDONGBANHANGDACAP { get; set; } = "DANGKYHOATDONGBANHANGDACAP";
    }

    public class LoaiGiayPhep
    {
        [DisplayName("Còn hạn")]
        public static string CONHAN { get; set; } = "CONHAN";

        [DisplayName("Hết hạn")]
        public static string HETHAN { get; set; } = "HETHAN";
    }

    public class TienCapQuyenConstant
    {
        [DisplayName("Có tiền cấp quyền")]
        public static string COTIENCAPQUYEN { get; set; } = "COTIENCAPQUYEN";

        [DisplayName("Không có tiền cấp quyền")]
        public static string KOCOTIENCAPQUYEN { get; set; } = "KOCOTIENCAPQUYEN";
    }
}