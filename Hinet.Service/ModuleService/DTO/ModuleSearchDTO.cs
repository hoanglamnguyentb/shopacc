using Hinet.Service.Common;

namespace Hinet.Service.ModuleService.DTO
{
    public class ModuleSearchDTO : SearchBase
    {
        public string QueryName { get; set; }
        public bool? QueryIsShow { get; set; }
        public string QueryIcon { get; set; }
        public string QueryClassCss { get; set; }
        public string QueryStyleCss { get; set; }
        public string QueryCode { get; set; }
    }
}