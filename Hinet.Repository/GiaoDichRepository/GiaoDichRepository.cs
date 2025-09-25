using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.GiaoDichRepository
{
    public class GiaoDichRepository : GenericRepository<GiaoDich>, IGiaoDichRepository
    {
        public GiaoDichRepository(DbContext context)
            : base(context)
        {

        }
        public GiaoDich GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
