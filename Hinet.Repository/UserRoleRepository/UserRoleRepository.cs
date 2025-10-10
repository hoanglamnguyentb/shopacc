using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.UserRoleRepository
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }
    }
}