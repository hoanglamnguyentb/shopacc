using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("Operation")]
    public class Operation : AuditableEntity<long>
    {
        [Required]
        public int ModuleId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string URL { get; set; }

        [Required]
        public string Code { get; set; }

        [StringLength(250)]
        public string Css { get; set; }

        [Required]
        public bool IsShow { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// Icon hiển thị trên Mobile
        /// </summary>
        public string Icon { get; set; }

        public string UrlFull { get; set; }
    }
}