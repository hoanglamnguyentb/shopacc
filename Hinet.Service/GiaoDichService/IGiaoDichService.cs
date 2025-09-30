using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.GiaoDichService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GiaoDichService
{
    public interface IGiaoDichService:IEntityService<GiaoDich>
    {
        PageListResultBO<GiaoDichDto> GetDaTaByPage(GiaoDichSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        GiaoDich GetById(long id);
    }
}
