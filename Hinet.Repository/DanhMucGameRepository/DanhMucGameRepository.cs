using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DanhMucGameRepository
{
    public class DanhMucGameRepository : GenericRepository<DanhMucGame>, IDanhMucGameRepository
    {
        public DanhMucGameRepository(DbContext context)
            : base(context)
        {

        }
        public DanhMucGame GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
