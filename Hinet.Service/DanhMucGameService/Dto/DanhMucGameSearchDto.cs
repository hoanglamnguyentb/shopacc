using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DanhMucGameService.Dto
{
    public class DanhMucGameSearchDto : SearchBase
    {
		public int? GameIdFilter { get; set; }
		public string NameFilter { get; set; }
		public string DuongDanAnhFilter { get; set; }
		public string MoTaFilter { get; set; }


    }
}