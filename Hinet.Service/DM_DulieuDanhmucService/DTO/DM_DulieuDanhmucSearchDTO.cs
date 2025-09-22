using Hinet.Service.Common;

namespace Hinet.Service.DM_DulieuDanhmucService.DTO
{
    public class DM_DulieuDanhmucSearchDTO : SearchBase
    {
        public string QueryName { get; set; }
        public string QueryCode { get; set; }
        public long? GroupId { get; set; }
    }
}