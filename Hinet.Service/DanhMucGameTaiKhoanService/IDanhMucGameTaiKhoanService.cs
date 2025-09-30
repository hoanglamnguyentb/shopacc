using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.DanhMucGameTaiKhoanService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DanhMucGameTaiKhoanService
{
    public interface IDanhMucGameTaiKhoanService:IEntityService<DanhMucGameTaiKhoan>
    {
        PageListResultBO<DanhMucGameTaiKhoanDto> GetDaTaByPage(DanhMucGameTaiKhoanSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        DanhMucGameTaiKhoan GetById(long id);
        void DeleteByTaiKhoanId(long taiKhoanId);
        List<DanhMucGameTaiKhoan> GetByTaiKhoanId(long taiKhoanId);
    }
}
