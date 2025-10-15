using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
	public class KieuGiamGiaConstant
    {
		[DisplayName("Phần trăm")]
		public static string PERCENT { get; set; } = "PERCENT";

		[DisplayName("Trừ tiền")]
		public static string AMOUNT { get; set; } = "AMOUNT";
	}
}