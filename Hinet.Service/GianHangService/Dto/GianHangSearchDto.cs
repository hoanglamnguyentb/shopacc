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
		public int STTFilter { get; set; }
		public string NameFilter { get; set; }
		public string MoTaFilter { get; set; }
		public string TrangThaiFilter { get; set; }
		public string ViTriHienThiFilter { get; set; }
		public string SlugFilter { get; set; }
		public string AnhBiaFilter { get; set; }


    }
}