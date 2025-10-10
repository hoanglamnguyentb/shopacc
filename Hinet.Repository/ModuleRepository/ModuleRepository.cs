using Hinet.Model.Entities;
using System.Data.Entity;

namespace Hinet.Repository.ModuleRepository
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(DbContext context) : base(context)
        {
        }
    }
}