using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("SiteConfig")]
	public class SiteConfig : AuditableEntity<int>
	{
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string OgImage { get; set; }
        public string SiteTitle { get; set; }
        public string Favicon { get; set; }
        public string Logo { get; set; }
    }
}