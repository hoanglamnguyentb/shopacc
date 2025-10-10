using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hinet.Model.Entities
{
	[Table("Deposit")]
	public class Deposit : AuditableEntity<long>
	{
        public long UserId { get; set; }
        public string Code { get; set; }
        public long Amount { get; set; } 
        public string Status { get; set; } = "PENDING"; //PENDING, COMPLETED, CANCELLED, EXPIRED
        public DateTime Expiry { get; set; } 
    }
}