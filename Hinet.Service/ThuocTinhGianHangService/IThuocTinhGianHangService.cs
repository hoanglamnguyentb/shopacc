using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.ThuocTinhGianHangService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ThuocTinhGianHangService
{
    public interface IThuocTinhGianHangService:IEntityService<ThuocTinhGianHang>
    {
        PageListResultBO<ThuocTinhGianHangDto> GetDaTaByPage(ThuocTinhGianHangSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        ThuocTinhGianHang GetById(long id);
        void DeleteByGianHangId(long gianHangId);
        List<ThuocTinhGianHangDto> GetDaTaByGianHangId(int gianHangId);
    }
}
