using Hinet.Model.Entities;
using Hinet.Service.RoleOperationService.DTO;

namespace Hinet.Service.RoleOperationService
{
    public interface IRoleOperationService : IEntityService<RoleOperation>
    {
        RoleOperationDTO GetConfigureOperation(int roleId);

        RoleOperationDTO GetConfigureProvince(int roleId);

        RoleOperationDTO GetConfigureProvinceJoinTinhHuyenXa(int roleId);
    }
}