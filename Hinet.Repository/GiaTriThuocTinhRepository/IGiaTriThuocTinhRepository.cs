using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.GiaTriThuocTinhRepository
{
    public interface IGiaTriThuocTinhRepository:IGenericRepository<GiaTriThuocTinh>
    {
        GiaTriThuocTinh GetById(long id);

    }
   
}
