using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DichVuService.Dto
{
    public class DichVuSearchDto : SearchBase
    {
		public string NameFilter { get; set; }
		public string DuongDanAnhFilter { get; set; }
		public string LinkFilter { get; set; }
		public bool? KichHoatFilter { get; set; }


    }
}