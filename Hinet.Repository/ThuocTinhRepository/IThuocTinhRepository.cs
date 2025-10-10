using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.ThuocTinhRepository
{
    public interface IThuocTinhRepository:IGenericRepository<ThuocTinh>
    {
        ThuocTinh GetById(long id);

    }
   
}
