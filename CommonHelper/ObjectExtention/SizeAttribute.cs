using System;

namespace CommonHelper.ObjectExtention
{
    public class SizeAttribute : Attribute
    {
        public int Width { get; set; }

        public SizeAttribute(int width)
        {
            Width = width;
        }
    }
}