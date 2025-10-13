using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.DonHangGiaTriThuocTinhRepository
{
    public class DonHangGiaTriThuocTinhRepository : GenericRepository<DonHangGiaTriThuocTinh>, IDonHangGiaTriThuocTinhRepository
    {
        public DonHangGiaTriThuocTinhRepository(DbContext context)
            : base(context)
        {

        }
        public DonHangGiaTriThuocTinh GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
