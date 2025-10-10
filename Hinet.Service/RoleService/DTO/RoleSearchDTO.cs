using Hinet.Service.Common;

namespace Hinet.Service.RoleService.DTO
{
    public class RoleSearchDTO : SearchBase
    {
        public string QueryName { get; set; }
        public string QueryCode { get; set; }
    }
}