using CommonHelper.Validation;
using Hinet.Service.Common;
using Hinet.Service.ModuleService.DTO;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.ModuleArea.Models
{
    public class ModuleViewModel
    {
        public class ModuleIndexViewModel
        {
            public PageListResultBO<ModuleDTO> GroupData { get; set; }
        }

        public class ModuleEditViewModel
        {
            public int Id { get; set; }

            [RequiredExtend]
            [StringLengthExtends(250)]
            [HTMLInjection]
            public string Name { set; get; }

            [RegularExpression("([0-9]+)", ErrorMessage = "Vui lòng nhập số")]
            public string Order { get; set; }

            [RequiredExtend]
            public bool IsShow { get; set; }

            public string Icon { get; set; }

            [StringLength(250)]
            public string ClassCss { get; set; }

            [StringLength(250)]
            public string StyleCss { get; set; }

            [RequiredExtend]
            [StringLength(250)]
            public string Code { get; set; }

            public bool? AllowFilterScope { get; set; }
            public bool? IsMobile { get; set; }
        }
    }
}