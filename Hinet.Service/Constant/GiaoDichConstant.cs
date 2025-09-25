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
		public static string CHOXULY { get; set; } = "NHAP";

		[DisplayName("Đã thanh toán")]
		public static string DATHANHTOAN { get; set; } = "DATHANHTOAN";

		[DisplayName("Thất bại")]
		public static string THATBAI { get; set; } = "THATBAI";
	}

	public static class LoaiDoiTuongConstant
	{
		[DisplayName("Dịch vụ")]
		public static string DICHVU { get; set; } = "DICHVU";

		[DisplayName("Tài khoản game")]
		public static string TAIKHOANGAME { get; set; } = "TAIKHOANGAME";

		[DisplayName("Nạp tiền")]
		public static string NAPTIEN { get; set; } = "NAPTIEN";
	}

	public static class LoaiGiaoDichConstant
	{
		[DisplayName("Mua")]
		public static string MUA { get; set; } = "MUA";

		[DisplayName("Nạp tiền")]
		public static string NAP { get; set; } = "NAP";

		[DisplayName("Rút tiền")]
		public static string RUT { get; set; } = "RUT";
	}

	public static class PhuongThucThanhToanConstant
	{
		[DisplayName("MoMo")]
		public static string MOMO { get; set; } = "MOMO";

		[DisplayName("Ngân hàng")]
		public static string NGANHANG { get; set; } = "NGANHANG";

		[DisplayName("Paypal")]
		public static string PAYPAL { get; set; } = "PAYPAL";
	}
}