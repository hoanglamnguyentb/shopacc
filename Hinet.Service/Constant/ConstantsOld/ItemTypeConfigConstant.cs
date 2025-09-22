using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ItemTypeConfigConstant
    {
        [DisplayName("Người dùng")]
        public static int User { get; set; } = 1;

        [DisplayName("Khoa phòng")]
        public static int Department { get; set; } = 2;
    }
}