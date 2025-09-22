using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class HanHopDongConstant
    {
        [DisplayName("3 Tháng")]
        public static int Han3Thang { get; set; } = 3;

        [DisplayName("6 Tháng")]
        public static int Han6Thang { get; set; } = 6;

        [DisplayName("1 năm")]
        public static int Han1Nam { get; set; } = 12;

        [DisplayName("2 năm")]
        public static int Han2Nam { get; set; } = 24;

        [DisplayName("3 năm")]
        public static int Han3Nam { get; set; } = 36;
    }
}