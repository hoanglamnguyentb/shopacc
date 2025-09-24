using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.TinTucService.Dto
{
    public class TinTucSearchDto : SearchBase
    {
		public string SlugFilter { get; set; }
		public string TieuDeFilter { get; set; }
		public string NoiDungFilter { get; set; }
		public string AnhBiaFilter { get; set; }
		public string TacGiaFilter { get; set; }
		public string TrangThaiFilter { get; set; }
		public DateTime ThoiGianXuatBanFilter { get; set; }


    }
}