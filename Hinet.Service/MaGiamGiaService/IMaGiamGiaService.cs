using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.MaGiamGiaService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.MaGiamGiaService
{
    public interface IMaGiamGiaService:IEntityService<MaGiamGia>
    {
        PageListResultBO<MaGiamGiaDto> GetDaTaByPage(MaGiamGiaSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        MaGiamGia GetById(long id);
    }
}
