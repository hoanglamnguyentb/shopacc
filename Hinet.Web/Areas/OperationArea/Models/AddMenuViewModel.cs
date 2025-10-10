using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Web.Areas.OperationArea.Models
{
    public class AddMenuViewModel
    {
        public List<SelectListItem> ListPermissionCode { get; set; }
        public OperationViewModel.OperationEditViewModel EditViewModel { get; set; }
        public List<SelectListItem> ListModule { get; set; }
    }
}