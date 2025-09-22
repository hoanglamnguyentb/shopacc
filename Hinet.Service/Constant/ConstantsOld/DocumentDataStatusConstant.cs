using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class DocumentDataStatusConstant
    {
        [DisplayName("Mới tạo")]
        public static string MoiTao { get; set; } = "NEW";

        [DisplayName("Đã duyệt")]
        public static string DaDuyet { get; set; } = "APPROVED";
    }
}