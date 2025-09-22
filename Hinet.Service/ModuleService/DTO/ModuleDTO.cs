using Hinet.Model.Entities;
using Hinet.Service.OperationService.DTO;
using System.Collections.Generic;

namespace Hinet.Service.ModuleService.DTO
{
    public class ModuleDTO : Module
    {
        public int OperationQuantity { set; get; }
        public IEnumerable<OperationDTO> GroupOperations { get; set; }
    }
}