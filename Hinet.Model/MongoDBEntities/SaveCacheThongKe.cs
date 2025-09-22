using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Model.MongoDBEntities
{
    public class SaveTramBts : AuditableEntityMongo<string>
    {
        public long total { get; set; }
        public long totalInyear { get; set; }
        public long totalLastYear { get; set; }
        public long vungcam { get; set; }
        public long doDaiTuyenViBa { get; set; }
    }


    public class SaveCacheAntens : AuditableEntityMongo<string>
    {
        public long total { get; set; }
        public long totalInyear { get; set; }
        public long totalLastYear { get; set; }
        public long _2G { get; set; }
        public long _3G { get; set; }
        public long _4G { get; set; }
        public long _5G { get; set; }
    }



    public class SaveCacheLoaPhatThanh : AuditableEntityMongo<string>
    {
        public long total { get; set; }
        public long totalInyear { get; set; }
        public long totalLastYear { get; set; }

    }

    public class SaveCacheThongKe : AuditableEntityMongo<string>
    {
    }
}
