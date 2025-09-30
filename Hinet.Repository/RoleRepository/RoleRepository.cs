using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.RoleRepository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}