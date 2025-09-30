using Hinet.Service.Common;

namespace Hinet.Service.OperationService.DTO
{
    public class OperationSearchDTO : SearchBase
    {
        public int QueryModuleId { get; set; }
        public string QueryName { get; set; }
        public bool? QueryIsShow { get; set; }
    }
}