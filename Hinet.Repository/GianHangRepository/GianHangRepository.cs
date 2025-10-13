using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.GianHangRepository
{
    public class GianHangRepository : GenericRepository<GianHang>, IGianHangRepository
    {
        public GianHangRepository(DbContext context)
            : base(context)
        {

        }
        public GianHang GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
