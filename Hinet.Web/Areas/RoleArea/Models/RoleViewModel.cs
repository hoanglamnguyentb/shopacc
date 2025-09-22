using CommonHelper.Validation;
using Hinet.Service.Common;
using Hinet.Service.RoleOperationService.DTO;
using Hinet.Service.RoleService.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Web.Areas.RoleArea.Models
{
    public class RoleViewModel
    {
        public class RoleIndexViewModel
        {
            public PageListResultBO<RoleDTO> GroupData { get; set; }
        }

        public class RoleOperationConfigViewModel
        {
            public RoleOperationDTO ConfigureData { get; set; }
            public List<SelectListItem> Tinh { get; set; }
            public List<SelectListItem> Huyen { get; set; }
            public List<SelectListItem> Xa { get; set; }
        }

        public class RoleEditViewModel
        {
            public int Id { get; set; }

            [RequiredExtend]
            [StringLengthExtends(250)]
            [HTMLInjection]
            [SpecialCharacter]
            public string Name { get; set; }

            [RequiredExtend]
            [StringLengthExtends(250)]
            [HTMLInjection]
            [SpecialCharacter]
            public string Code { get; set; }
        }
    }
}