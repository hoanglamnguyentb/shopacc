using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.BinhLuanRepository
{
    public class BinhLuanRepository : GenericRepository<BinhLuan>, IBinhLuanRepository
    {
        public BinhLuanRepository(DbContext context)
            : base(context)
        {

        }
        public BinhLuan GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
