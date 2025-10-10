using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DanhMucGameTaiKhoanRepository
{
    public class DanhMucGameTaiKhoanRepository : GenericRepository<DanhMucGameTaiKhoan>, IDanhMucGameTaiKhoanRepository
    {
        public DanhMucGameTaiKhoanRepository(DbContext context)
            : base(context)
        {

        }
        public DanhMucGameTaiKhoan GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
