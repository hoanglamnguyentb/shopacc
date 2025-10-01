using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.GameService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.DanhMucGameService.Dto;

namespace Hinet.Service.GameService
{
    public interface IGameService:IEntityService<Game>
    {
        PageListResultBO<GameDto> GetDaTaByPage(GameSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        Game GetById(long id);
        List<GameDto> GetListGame();
        GameDto GetListGameById(int id);    
        PageListResultBO<TaiKhoanDto> GetTaiKhoanPagedByDanhMucSlug(string slug, TaiKhoanSearchDto search);
        List<DanhMucGameDto> GetListDanhMucGameBySlug(string gameSlug);
        Game GetBySlug(string slug);
        List<DanhMucGameDto> GetListDanhMucGameKhac(int id, int? take);
    }
}
