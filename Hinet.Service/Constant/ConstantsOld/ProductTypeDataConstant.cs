using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ProductTypeDataConstant
    {
        [DisplayName("Sản phẩm")]
        public static string SanPham { get; set; } = "SanPham";

        [DisplayName("Dịch vụ")]
        public static string DichVu { get; set; } = "DichVu";
    }
}