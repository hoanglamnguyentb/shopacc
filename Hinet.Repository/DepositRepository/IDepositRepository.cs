using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DepositRepository
{
    public interface IDepositRepository:IGenericRepository<Deposit>
    {
        Deposit GetById(long id);

    }
   
}
