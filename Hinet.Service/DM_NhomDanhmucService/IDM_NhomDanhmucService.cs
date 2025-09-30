using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_NhomDanhmucService.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Service.DM_NhomDanhmucService
{
    public interface IDM_NhomDanhmucService : IEntityService<DM_NhomDanhmuc>
    {
        PageListResultBO<DM_NhomDanhmucDTO> GetDataByPage(DM_NhomDanhmucSearchDTO searchParams, int pageIndex = 1, int pageSize = 10);

        bool CheckGroupCodeExisted(string groupCode);

        IEnumerable<SelectListItem> GetDataByCode(string code);

        List<DM_DulieuDanhmuc> GetByCode(string code);

        long GetIdByGroupCode(string groupCode);

        DM_NhomDanhmuc GetNhomDanhMucByGroupCode(string groupCode);
    }
}