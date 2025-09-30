using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.DanhmucRepository
{
    public class DM_DulieuDanhmucRepository : GenericRepository<DM_DulieuDanhmuc>, IDM_DulieuDanhmucRepository
    {
        public DM_DulieuDanhmucRepository(DbContext context) : base(context)
        {
        }
    }
}