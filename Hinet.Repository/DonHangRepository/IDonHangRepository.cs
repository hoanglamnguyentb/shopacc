using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DonHangRepository
{
    public interface IDonHangRepository:IGenericRepository<DonHang>
    {
        DonHang GetById(long id);

    }
   
}
