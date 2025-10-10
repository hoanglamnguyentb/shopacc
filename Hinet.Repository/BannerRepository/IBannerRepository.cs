using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Repository.BannerRepository
{
    public interface IBannerRepository:IGenericRepository<Banner>
    {
        Banner GetById(long id);

    }
   
}
