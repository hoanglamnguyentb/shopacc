using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DonHangGiaTriThuocTinhRepository
{
    public interface IDonHangGiaTriThuocTinhRepository:IGenericRepository<DonHangGiaTriThuocTinh>
    {
        DonHangGiaTriThuocTinh GetById(long id);

    }
   
}
