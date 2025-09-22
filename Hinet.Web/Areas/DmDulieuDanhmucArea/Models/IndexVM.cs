using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService.DTO;

namespace Hinet.Web.Areas.DmDulieuDanhmucArea.Models
{
    public class IndexVM
    {
        public PageListResultBO<DM_DulieuDanhmucDTO> Data { get; set; }
        public long? GroupId { get; set; }
        public string Code { get; set; }
    }
}