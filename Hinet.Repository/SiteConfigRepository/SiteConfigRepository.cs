using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.SiteConfigRepository
{
    public class SiteConfigRepository : GenericRepository<SiteConfig>, ISiteConfigRepository
    {
        public SiteConfigRepository(DbContext context)
            : base(context)
        {

        }
        public SiteConfig GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
