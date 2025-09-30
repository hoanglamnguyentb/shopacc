using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DanhMucGameTaiKhoanRepository
{
    public interface IDanhMucGameTaiKhoanRepository:IGenericRepository<DanhMucGameTaiKhoan>
    {
        DanhMucGameTaiKhoan GetById(long id);

    }
   
}
