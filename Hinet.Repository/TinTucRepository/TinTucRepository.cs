using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.TinTucRepository
{
    public class TinTucRepository : GenericRepository<TinTuc>, ITinTucRepository
    {
        public TinTucRepository(DbContext context)
            : base(context)
        {

        }
        public TinTuc GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
