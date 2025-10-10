using System.ComponentModel;

namespace Hinet.Service.Constant
{
	public class DepositConstant
	{
		[DisplayName("Đang xử lý")]
		public static string PENDING { get; set; } = "PENDING";

		[DisplayName("Thành công")]
		public static string SUCCESS { get; set; } = "SUCCESS";

		[DisplayName("Đã huỷ")]
		public static string CANCELED { get; set; } = "CANCELED";

		[DisplayName("Quá hạn")]
		public static string EXPIRED { get; set; } = "EXPIRED";
	}
}