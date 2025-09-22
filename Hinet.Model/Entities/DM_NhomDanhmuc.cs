using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("DM_NhomDanhmuc")]
    public class DM_NhomDanhmuc : AuditableEntity<long>
    {
        [Required]
        [StringLength(150)]
        public string GroupName { get; set; }

        [Required]
        [StringLength(150)]
        public string GroupCode { get; set; }
    }
}