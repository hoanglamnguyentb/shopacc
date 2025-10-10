using Hinet.Model.IdentityEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Web.Areas.UserArea.Models
{
    public class NguoiDungChuyenMucEditViewModel
    {
        public AppUser EntityUser { get; set; }
        public List<SelectListItem> GroupChuyenMuc { get; set; }
    }
}