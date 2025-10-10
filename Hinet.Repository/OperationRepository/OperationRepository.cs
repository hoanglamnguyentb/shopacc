using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.OperationRepository
{
    public class OperationRepository : GenericRepository<Operation>, IOperationRepository
    {
        public OperationRepository(DbContext context) : base(context)
        {
        }
    }
}