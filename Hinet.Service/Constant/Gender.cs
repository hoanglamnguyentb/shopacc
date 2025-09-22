using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class Gender
    {
        [DisplayName("Nam")]
        public static string Nam { get; set; } = "Nam";

        [DisplayName("Nữ")]
        public static string Nu { get; set; } = "Nu";
    }
}