using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("DM_DulieuDanhmuc")]
    public class DM_DulieuDanhmuc : AuditableEntity<long>
    {
        public long? GroupId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Code { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public int? Priority { get; set; }
        public string Icon { get; set; }
    }
}