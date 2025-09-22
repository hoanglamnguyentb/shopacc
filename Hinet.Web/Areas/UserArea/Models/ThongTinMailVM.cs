using Hinet.Model.IdentityEntities;

namespace Hinet.Web.Areas.UserArea.Models
{
    public class ThongTinMailVM
    {
        public string UserName { get; set; }
        public AppUser UserData { get; set; }
        public string LinkHeThong { get; set; }
    }
}