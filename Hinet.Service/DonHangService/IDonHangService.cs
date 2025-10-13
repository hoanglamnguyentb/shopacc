using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.DonHangService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DonHangService
{
    public interface IDonHangService:IEntityService<DonHang>
    {
        PageListResultBO<DonHangDto> GetDaTaByPage(DonHangSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        DonHang GetById(long id);
    }
}
