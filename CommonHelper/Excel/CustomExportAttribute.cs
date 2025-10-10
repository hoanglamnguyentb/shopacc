using System;

namespace CommonHelper.Excel
{
    public class CustomExportAttribute : Attribute
    {
        public int Width { get; set; }
    }
}