using System.ComponentModel;

namespace Hinet.Service.Constant
{
    internal class NewCategoryConstant
    {
        [DisplayName("Tao moi")]
        public static string TaoMoi { get; set; } = "NEW";

        [DisplayName("Da duyet")]
        public static string Duyet { get; set; } = "APPROVED";
    }
}