using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DichVuRepository
{
    public class DichVuRepository : GenericRepository<DichVu>, IDichVuRepository
    {
        public DichVuRepository(DbContext context)
            : base(context)
        {

        }
        public DichVu GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
