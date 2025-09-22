using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class FilterTypeConstant
    {
        [DisplayName("Text")]
        public static int Text { get; private set; } = 0;

        [DisplayName("Date")]
        public static int Date { get; private set; } = 1;

        [DisplayName("Dropdown")]
        public static int Dropdown { get; private set; } = 2;
    }
}