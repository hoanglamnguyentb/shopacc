using System;

namespace Hinet.Service.Common
{
    public class ColorAttribute : Attribute
    {
        public string Color { get; set; }
        public string BgColor { get; set; }
        public string Icon { get; set; }
    }
}