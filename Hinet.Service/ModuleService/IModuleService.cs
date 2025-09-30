using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.ModuleService.DTO;
using System.Collections.Generic;

namespace Hinet.Service.ModuleService
{
    public interface IModuleService : IEntityService<Module>
    {
        PageListResultBO<ModuleDTO> GetDataByPage(ModuleSearchDTO searchParams, int pageIndex = 1, int pageSize = 20);

        bool CheckExistCode(string code, long? id = null);

        List<Module> GetListModuleLimitData();
    }
}