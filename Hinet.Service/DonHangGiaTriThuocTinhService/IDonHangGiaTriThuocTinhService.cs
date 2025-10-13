using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.DonHangGiaTriThuocTinhService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DonHangGiaTriThuocTinhService
{
    public interface IDonHangGiaTriThuocTinhService:IEntityService<DonHangGiaTriThuocTinh>
    {
        PageListResultBO<DonHangGiaTriThuocTinhDto> GetDaTaByPage(DonHangGiaTriThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        DonHangGiaTriThuocTinh GetById(long id);
    }
}
