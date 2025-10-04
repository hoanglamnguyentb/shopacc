using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.ThuocTinhService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ThuocTinhService
{
    public interface IThuocTinhService:IEntityService<ThuocTinh>
    {
        PageListResultBO<ThuocTinhDto> GetDaTaByPage(ThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        ThuocTinh GetById(long id);
        void DeleteByGameId(long gameId);
    }
}
