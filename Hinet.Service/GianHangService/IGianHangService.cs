using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.GianHangService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GianHangService
{
    public interface IGianHangService:IEntityService<GianHang>
    {
        PageListResultBO<GianHangDto> GetDaTaByPage(GianHangSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        GianHang GetById(long id);
    }
}
