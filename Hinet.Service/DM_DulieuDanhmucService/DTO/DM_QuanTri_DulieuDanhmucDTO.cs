using Hinet.Model.Entities;

namespace Hinet.Service.DM_DulieuDanhmucService.DTO
{
    public class DM_QuanTri_DulieuDanhmucDTO : DM_DulieuDanhmuc
    {
        public string TenNhomDuLieu { get; set; }
        public string GroupCode { get; set; }
    }
}