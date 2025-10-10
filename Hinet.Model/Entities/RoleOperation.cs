using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("RoleOperation")]
    public class RoleOperation : AuditableEntity<long>
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public long OperationId { get; set; }

        [Required]
        public int IsAccess { get; set; }
    }
}