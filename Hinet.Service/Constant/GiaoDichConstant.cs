using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
	public class TrangThaiGiaoDichConstant
	{
		[DisplayName("Chờ xử lý")]
		public static string CHOXULY => "CHOXULY";

		[DisplayName("Đã thanh toán")]
		public static string DATHANHTOAN => "DATHANHTOAN";

		[DisplayName("Thất bại")]
		public static string THATBAI => "THATBAI";
	}

	public class LoaiDoiTuongConstant
	{
		[DisplayName("Dịch vụ")]
		public static string DICHVU => "DICHVU";

		[DisplayName("Tài khoản game")]
		public static string TAIKHOANGAME => "TAIKHOANGAME";

		[DisplayName("Nạp tiền")]
		public static string NAPTIEN => "NAPTIEN";
	}

	public class LoaiGiaoDichConstant
	{
		[DisplayName("Mua")]
		public static string MUA => "MUA";

		[DisplayName("Nạp tiền")]
		public static string NAP => "NAP";

		[DisplayName("Rút tiền")]
		public static string RUT => "RUT";
	}

	public class PhuongThucThanhToanConstant
	{
		[DisplayName("MoMo")]
		public static string MOMO => "MOMO";

		[DisplayName("Ngân hàng")]
		public static string NGANHANG => "NGANHANG";

		[DisplayName("Paypal")]
		public static string PAYPAL => "PAYPAL";
	}
}