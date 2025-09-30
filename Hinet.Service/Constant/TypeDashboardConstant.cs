using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeDashboardConstant
    {
        [DisplayName("Người dùng nội bộ")]
        public static int NoiBo { get; set; } = 0;

        [DisplayName("Lãnh đạo nội bộ")]
        public static int LanhDaoNoiBo { get; set; } = 1;

        [DisplayName("Ngoài hệ thống")]
        public static int NgoaiHeThong { get; set; } = 2;

        [DisplayName("Tổng cục quản lý thị trường")]
        public static int TongCucQLTT { get; set; } = 3;
    }
}