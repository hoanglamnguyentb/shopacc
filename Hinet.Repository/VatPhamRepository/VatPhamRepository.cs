using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.VatPhamRepository
{
    public class VatPhamRepository : GenericRepository<VatPham>, IVatPhamRepository
    {
        public VatPhamRepository(DbContext context)
            : base(context)
        {

        }
        public VatPham GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
