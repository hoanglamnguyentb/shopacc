using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.TaiLieuDinhKemRepository
{
    public class TaiLieuDinhKemRepository : GenericRepository<TaiLieuDinhKem>, ITaiLieuDinhKemRepository
    {
        public TaiLieuDinhKemRepository(DbContext context) : base(context)
        {
        }
    }
}