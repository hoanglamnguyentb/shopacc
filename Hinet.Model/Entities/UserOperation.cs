using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("UserOperation")]
    public class UserOperation : AuditableEntity<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long OperationId { get; set; }

        [Required]
        public int IsAccess { get; set; }
    }
}