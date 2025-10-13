using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.VatPhamService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.VatPhamService
{
    public interface IVatPhamService:IEntityService<VatPham>
    {
        PageListResultBO<VatPhamDto> GetDaTaByPage(VatPhamSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        VatPham GetById(long id);
    }
}
