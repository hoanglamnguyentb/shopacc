using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ThongBaoWebsiteStatusConstant
    {
        [DisplayName("Mới tạo")]
        public static int MoiTao { get; set; } = 0;

        [DisplayName("Chờ duyệt")]
        public static int ChoDuyet { get; set; } = 1;
    }
}