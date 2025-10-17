using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.SiteConfigService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.SiteConfigService
{
    public interface ISiteConfigService:IEntityService<SiteConfig>
    {
        PageListResultBO<SiteConfigDto> GetDaTaByPage(SiteConfigSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        SiteConfig GetById(long id);
        SiteConfigDto GetActiveConfig();
        SiteConfig GetTelegramInfo();
    }
}
