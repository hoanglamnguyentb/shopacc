using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiTramConstant
    {
        [DisplayName("Đo lưu lượng")]
        public static string Doluuluong { get; set; } = "Doluuluong";

        [DisplayName("Mực nước")]
        public static string Mucnuoc { get; set; } = "Mucnuoc";

        [DisplayName("Chất lượng nước")]
        public static string Chatluongnuoc { get; set; } = "Chatluongnuoc";
    }
}