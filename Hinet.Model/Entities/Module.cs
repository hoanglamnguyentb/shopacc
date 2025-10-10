using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("Module")]
    public class Module : AuditableEntity<int>
    {
        [Required]
        [StringLength(250)]
        public string Code { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { set; get; }

        public int Order { get; set; }

        [Required]
        public bool IsShow { get; set; }

        public string Icon { get; set; }

        [StringLength(250)]
        public string ClassCss { get; set; }

        [StringLength(250)]
        public string StyleCss { get; set; }

        public string Link { get; set; }
        public bool? AllowFilterScope { get; set; }

        /// <summary>
        /// Có hiển thị trên Mobile hay không
        /// </summary>
        public bool? IsMobile { get; set; }
    }
}