using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.ThuocTinhRepository
{
    public class ThuocTinhRepository : GenericRepository<ThuocTinh>, IThuocTinhRepository
    {
        public ThuocTinhRepository(DbContext context)
            : base(context)
        {

        }
        public ThuocTinh GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
