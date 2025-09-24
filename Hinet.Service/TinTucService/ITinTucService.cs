using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.TinTucService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.TinTucService
{
    public interface ITinTucService:IEntityService<TinTuc>
    {
        PageListResultBO<TinTucDto> GetDaTaByPage(TinTucSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        TinTuc GetById(long id);
    }
}
