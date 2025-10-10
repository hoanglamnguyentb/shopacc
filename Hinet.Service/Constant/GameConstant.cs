using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
	public class ViTriHienThiGameConstant
	{
		[DisplayName("Trang chủ")]
		public static string TRANGCHU { get; set; } = "TRANGCHU";

		[DisplayName("Nổi bật")]
		public static string NOIBAT { get; set; } = "NOIBAT";

		[DisplayName("Khác")]
		public static string KHAC { get; set; } = "KHAC";
	}

    public class KieuDuLieuThuocTinhGameConstant
    {
        [DisplayName("Text")]
        public static string TEXT { get; set; } = "TEXT";

        [DisplayName("Number")]
        public static string NUMBER { get; set; } = "NUMBER";

        [DisplayName("Dropdown")]
        public static string DROPDOWN { get; set; } = "DROPDOWN";

        [DisplayName("Boolean")]
        public static string BOOLEAN { get; set; } = "BOOLEAN";
    }
}