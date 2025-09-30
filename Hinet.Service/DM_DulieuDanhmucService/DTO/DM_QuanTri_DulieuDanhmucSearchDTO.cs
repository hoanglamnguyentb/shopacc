using Hinet.Service.Common;

namespace Hinet.Service.DM_DulieuDanhmucService.DTO
{
    public class DM_QuanTri_DulieuDanhmucSearchDTO : SearchBase
    {
        public long? IdNhomDuLieuFilter { get; set; }
        public string QueryName { get; set; }
        public string QueryCode { get; set; }
    }
}