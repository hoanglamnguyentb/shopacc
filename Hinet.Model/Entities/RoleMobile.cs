using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("RoleMobile")]
    public class RoleMobile : AuditableEntity<long>
    {
        public string Ten { get; set; }
        public bool TrangThai { get; set; }
    }
}