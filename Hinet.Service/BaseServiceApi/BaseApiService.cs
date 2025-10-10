using Hinet.Repository;

namespace Hinet.Service.BaseServiceApi
{
    public class BaseApiService<T> : EntityService<T>, IBaseApiService<T> where T : class
    {
        public BaseApiService(IUnitOfWork unitOfWork, IGenericRepository<T> repository) : base(unitOfWork, repository)
        {
        }
    }
}