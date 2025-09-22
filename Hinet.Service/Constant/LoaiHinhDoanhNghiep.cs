using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiHinhDoanhNghiep
    {
        [DisplayName("Nhà nước")]
        public static string Government { get; set; } = "Government";

        [DisplayName("Ngoài nhà nước (trừ FDI)")]
        public static string NonGovernment { get; set; } = "NonGovernment";

        [DisplayName("Có vốn FDI")]
        public static string NonGovernmentHaveFDI { get; set; } = "NonGovernmentHaveFDI";
    }
}