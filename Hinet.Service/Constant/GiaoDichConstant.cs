using Hinet.Service.Common;
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
        [Color(Color = "#FFC107", BgColor = "#FFF8E1", Icon = "bi bi-clock")]
        public static string CHOXULY => "CHOXULY";

        [DisplayName("Đã thanh toán")]
        [Color(Color = "#4CAF50", BgColor = "#E8F5E9", Icon = "bi bi-check-circle")]
        public static string DATHANHTOAN => "DATHANHTOAN";

        [DisplayName("Hoàn tiền")]
        [Color(Color = "#2196F3", BgColor = "#E3F2FD", Icon = "bi bi-arrow-repeat")]
        public static string HOANTIEN => "HOANTIEN";

        [DisplayName("Thất bại")]
        [Color(Color = "#F44336", BgColor = "#FFEBEE", Icon = "bi bi-x-circle")]
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

        [DisplayName("Nạp topup")]
        public static string NAPTOPUP => "NAPTOPUP";
    }

	public class LoaiGiaoDichConstant
	{
		[DisplayName("Mua")]
		public static string MUA => "MUA";

		[DisplayName("Nạp tiền")]
		public static string NAP => "NAP";

		[DisplayName("Rút tiền")]
		public static string RUT => "RUT";

        [DisplayName("Nạp topup")]
        public static string NAPTOPUP => "NAPTOPUP";
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