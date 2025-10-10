using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.DanhmucRepository
{
    public class NhomDanhmucRepository : GenericRepository<DM_NhomDanhmuc>, IDM_NhomDanhmucRepository
    {
        public NhomDanhmucRepository(DbContext context) : base(context)
        {
        }
    }
}