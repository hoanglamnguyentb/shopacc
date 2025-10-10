using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
	public class TrangThaiTaiKhoanConstant
	{
		[DisplayName("Chưa bán")]
		public static string CHUABAN { get; set; } = "CHUABAN";

		[DisplayName("Đã bán")]
		public static string DABAN { get; set; } = "DABAN";

		[DisplayName("Khác")]
		public static string KHAC { get; set; } = "KHAC";
	}
}