using Hinet.Model.IdentityEntities;

namespace Hinet.Repository.AppUserRepository
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        AppUser GetById(long id);

        bool HasRole(long userId, string role);
    }
}