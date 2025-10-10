using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DepositService.Dto
{
    public class DepositSearchDto : SearchBase
    {
		public long UserIdFilter { get; set; }
		public string CodeFilter { get; set; }
		public long AmountFilter { get; set; }
		public string StatusFilter { get; set; }
		public DateTime ExpiryFilter { get; set; }


    }
}