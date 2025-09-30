using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.GameService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GameService
{
    public interface IGameService:IEntityService<Game>
    {
        PageListResultBO<GameDto> GetDaTaByPage(GameSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        Game GetById(long id);
        List<GameDto> GetListGame();
    }
}
