using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.DichVuService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DichVuService
{
    public interface IDichVuService:IEntityService<DichVu>
    {
        PageListResultBO<DichVuDto> GetDaTaByPage(DichVuSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        DichVu GetById(long id);
    }
}
