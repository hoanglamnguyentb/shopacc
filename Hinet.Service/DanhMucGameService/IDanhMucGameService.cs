using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DanhMucGameService
{
    public interface IDanhMucGameService:IEntityService<DanhMucGame>
    {
        PageListResultBO<DanhMucGameDto> GetDaTaByPage(DanhMucGameSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        DanhMucGame GetById(long id);
        List<DanhMucGame> GetDanhMucByGame(long gameId);
    }
}
