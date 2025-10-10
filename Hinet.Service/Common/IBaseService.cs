using Hinet.Model.Entities;

namespace Hinet.Service.Common
{
    public interface IBaseService<T> : IService where T : class
    {
        ServiceResult<T> GetAllEntity();

        ServiceResult<T> CreateEntity(T entity);
    }
}