using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.BinhLuanService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.BinhLuanService
{
    public interface IBinhLuanService:IEntityService<BinhLuan>
    {
        PageListResultBO<BinhLuanDto> GetDaTaByPage(BinhLuanSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        BinhLuan GetById(long id);
    }
}
