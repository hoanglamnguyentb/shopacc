using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using System.Data.Entity;
using System.Linq;

namespace Hinet.Repository.AppUserRepository
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(DbContext context)
            : base(context)
        {
        }

        public AppUser GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public bool HasRole(long userId, string role)
        {
            var roleObj = _entities.Set<Role>().Where(x => x.Code == role).FirstOrDefault();
            if (roleObj == null)
            {
                return false;
            }
            return _entities.Set<UserRole>().Any(x => x.RoleId == roleObj.Id && x.UserId == userId);
        }
    }
}