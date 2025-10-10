using Hinet.Service.ModuleService.DTO;
using System.Collections.Generic;

namespace Hinet.Service.RoleOperationService.DTO
{
    public class RoleOperationDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<ModuleDTO> GroupModules { get; set; }
    }
}