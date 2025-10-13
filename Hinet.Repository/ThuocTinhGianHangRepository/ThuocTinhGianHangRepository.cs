using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.ThuocTinhGianHangRepository
{
    public class ThuocTinhGianHangRepository : GenericRepository<ThuocTinhGianHang>, IThuocTinhGianHangRepository
    {
        public ThuocTinhGianHangRepository(DbContext context)
            : base(context)
        {

        }
        public ThuocTinhGianHang GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
