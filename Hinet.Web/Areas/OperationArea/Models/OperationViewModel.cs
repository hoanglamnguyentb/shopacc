using CommonHelper.Validation;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.OperationService.DTO;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.OperationArea.Models
{
    public class OperationViewModel
    {
        public class OperationIndexViewModel
        {
            public int ModuleId { get; set; }
            public PageListResultBO<OperationDTO> GroupData { set; get; }
            public Module module { get; set; }
        }

        public class OperationEditViewModel
        {
            public long Id { get; set; }
            public int ModuleId { get; set; }

            [RequiredExtend]
            [StringLengthExtends(250)]
            public string Name { get; set; }

            [RequiredExtend]
            [StringLengthExtends(250)]
            public string URL { get; set; }

            [RequiredExtend]
            [StringLengthExtends(250)]
            public string Code { get; set; }

            [StringLengthExtends(250)]
            [HTMLInjection]
            public string Css { get; set; }

            [Required]
            public bool IsShow { get; set; }

            [Numeric]
            public string Order { get; set; }

            public string Icon { get; set; }
        }
    }
}