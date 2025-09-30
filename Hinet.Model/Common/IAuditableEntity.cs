using System;

namespace Hinet.Model
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }

        string CreatedBy { get; set; }
        long? CreatedID { get; set; }

        DateTime UpdatedDate { get; set; }
        long? UpdatedID { get; set; }
        string UpdatedBy { get; set; }

        bool? IsDelete { get; set; }
        DateTime? DeleteTime { get; set; }
        long? DeleteId { get; set; }

        //long? OldSysId { get; set; }
    }
}