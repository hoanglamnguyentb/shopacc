using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.UserOperationService.Dto;
using System.Collections.Generic;

namespace Hinet.Service.UserOperationService
{
    public interface IUserOperationService : IEntityService<UserOperation>
    {
        PageListResultBO<UserOperationDto> GetDaTaByPage(UserOperationSearchDto searchModel, int pageIndex = 1, int pageSize = 10);

        UserOperation GetById(long id);

        List<ModuleService.DTO.ModuleDTO> GetConfigureOperation(long userID);
    }
}