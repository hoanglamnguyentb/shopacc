using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.VatPhamService.Dto
{
    public class VatPhamSearchDto : SearchBase
    {
		public int GianHangIdFilter { get; set; }
		public int? GiaGocFilter { get; set; }
		public int? STTFilter { get; set; }
		public string NameFilter { get; set; }
		public string DuongDanAnhFilter { get; set; }
		public string MoTaFilter { get; set; }
		public string SlugFilter { get; set; }


    }
}