using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.GiaTriThuocTinhRepository
{
    public class GiaTriThuocTinhRepository : GenericRepository<GiaTriThuocTinh>, IGiaTriThuocTinhRepository
    {
        public GiaTriThuocTinhRepository(DbContext context)
            : base(context)
        {

        }
        public GiaTriThuocTinh GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
