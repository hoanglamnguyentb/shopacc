using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
    [Table("Notification")]
    public class Notification : AuditableEntity<long>
    {
        [Required]
        public string Message { get; set; }
        public string Link { get; set; }
        public long? FromUser { get; set; }
        public long? ToUser { get; set; }
        public bool IsRead { get; set; }
        [MaxLength(250)]
        public string Type { get; set; }
        public string Param { get; set; }
    }
}