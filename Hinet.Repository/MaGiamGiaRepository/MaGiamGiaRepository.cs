using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.MaGiamGiaRepository
{
    public class MaGiamGiaRepository : GenericRepository<MaGiamGia>, IMaGiamGiaRepository
    {
        public MaGiamGiaRepository(DbContext context)
            : base(context)
        {

        }
        public MaGiamGia GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        
    }
}
