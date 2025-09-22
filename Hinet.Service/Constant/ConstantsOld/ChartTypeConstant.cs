using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ChartTypeConstant
    {
        [DisplayName("Hình tròn")]
        public static int Tron { get; set; } = 1;

        [DisplayName("Hình cột")]
        public static int Cot { get; set; } = 2;
    }
}