using Hinet.Model.Entities;

namespace Hinet.Repository.UserOperationRepository
{
    public interface IUserOperationRepository : IGenericRepository<UserOperation>
    {
        UserOperation GetById(long id);
    }
}