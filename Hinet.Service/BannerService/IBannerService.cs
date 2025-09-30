using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.BannerService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.BannerService
{
    public interface IBannerService:IEntityService<Banner>
    {
        PageListResultBO<BannerDto> GetDaTaByPage(BannerSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        Banner GetById(long id);
    }
}
