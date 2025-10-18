using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GianHangService.Dto
{
    public class GianHangSearchDto : SearchBase
    {
		public string NameFilter { get; set; }
		public string MoTaFilter { get; set; }
		public bool? KichHoatFilter { get; set; }
		public string SlugFilter { get; set; }
		public string AnhBiaFilter { get; set; }
    }
}