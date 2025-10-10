using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("UserRole")]
    public class UserRole : AuditableEntity<long>
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
    }
}