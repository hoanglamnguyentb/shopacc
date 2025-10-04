using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.GiaTriThuocTinhService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GiaTriThuocTinhService
{
    public interface IGiaTriThuocTinhService:IEntityService<GiaTriThuocTinh>
    {
        PageListResultBO<GiaTriThuocTinhDto> GetDaTaByPage(GiaTriThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        GiaTriThuocTinh GetById(long id);
    }
}
