using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ThuocTinhService.Dto
{
    public class ThuocTinhSearchDto : SearchBase
    {
		public int GameIdFilter { get; set; }
		public string TenThuocTinhFilter { get; set; }
		public string KieuDuLieuFilter { get; set; }
		public string DmNhomDanhmucFilter { get; set; }


    }
}