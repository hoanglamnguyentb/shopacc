using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class StatusDaoTaoConstant
    {
        [DisplayName("Đã hoàn thành")]
        public static int DaHoanAnh { get; set; } = 1;

        [DisplayName("Chưa hoàn thành")]
        public static int ChuaHoanThanh { get; set; } = 2;
    }
}