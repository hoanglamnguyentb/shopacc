using Hinet.Service.Common;

namespace Hinet.Service.DM_NhomDanhmucService.DTO
{
    public class DM_NhomDanhmucSearchDTO : SearchBase
    {
        public string QueryName { get; set; }
        public string QueryCode { get; set; }
    }
}