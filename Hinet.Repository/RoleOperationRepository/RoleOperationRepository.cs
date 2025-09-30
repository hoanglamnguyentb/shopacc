using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.RoleOperationRepository
{
    public class RoleOperationRepository : GenericRepository<RoleOperation>, IRoleOperationRepository
    {
        public RoleOperationRepository(DbContext context) : base(context)
        {
        }
    }
}