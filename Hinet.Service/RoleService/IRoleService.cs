using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.RoleService.DTO;

namespace Hinet.Service.RoleService
{
    public interface IRoleService : IEntityService<Role>
    {
        PageListResultBO<RoleDTO> GetDataByPage(RoleSearchDTO searchParams, int pageIndex = 1, int pageSize = 10);

        int? GetIdByCode(string code);
    }
}