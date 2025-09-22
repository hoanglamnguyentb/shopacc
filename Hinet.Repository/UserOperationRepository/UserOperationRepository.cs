using Hinet.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace Hinet.Repository.UserOperationRepository
{
    public class UserOperationRepository : GenericRepository<UserOperation>, IUserOperationRepository
    {
        public UserOperationRepository(DbContext context)
            : base(context)
        {
        }

        public UserOperation GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}