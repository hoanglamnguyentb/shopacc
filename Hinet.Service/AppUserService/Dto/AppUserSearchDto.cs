using Hinet.Service.Common;

namespace Hinet.Service.AppUserService.Dto
{
	public class AppUserSearchDto : SearchBase
	{
		public string UserNameFilter { get; set; }
		public string FullNameFilter { get; set; }
		public string EmailFilter { get; set; }
		public string AddressFilter { get; set; }
	}
}