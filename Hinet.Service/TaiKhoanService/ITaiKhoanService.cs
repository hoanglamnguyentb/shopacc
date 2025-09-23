using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.TaiKhoanService
{
    public interface ITaiKhoanService:IEntityService<TaiKhoan>
    {
        PageListResultBO<TaiKhoanDto> GetDaTaByPage(TaiKhoanSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        TaiKhoan GetById(long id);
    }
}
