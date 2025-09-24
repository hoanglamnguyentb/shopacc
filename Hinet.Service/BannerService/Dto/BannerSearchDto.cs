using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.BannerService.Dto
{
    public class BannerSearchDto : SearchBase
    {
		public string NameFilter { get; set; }
		public string DuongDanAnhFilter { get; set; }
		public string LinkFilter { get; set; }
		public bool? KichHoatFilter { get; set; }


    }
}