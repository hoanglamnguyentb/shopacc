using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class GioiTinhConstant
    {
        [DisplayName("Nam")]
        public static int Nam { get; set; } = 1;

        [DisplayName("Nữ")]
        public static int Nu { get; set; } = 2;

        [DisplayName("Chưa xác định")]
        public static int Chuaxacdinh { get; set; } = 0;
    }
}