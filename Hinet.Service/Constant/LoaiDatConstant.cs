using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiDatConstant
    {
        [DisplayName("Đất của doanh nghiệp")]
        public static string DOANHNGHIEP { set; get; } = "DOANHNGHIEP";

        [DisplayName("CQNN")]
        public static string CQNN { set; get; } = "CQNN";

        [DisplayName("Đi thuê")]
        public static string DITHUE { set; get; } = "DITHUE";
    }
}