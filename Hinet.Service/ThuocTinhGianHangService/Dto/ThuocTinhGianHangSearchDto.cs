using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ThuocTinhGianHangService.Dto
{
    public class ThuocTinhGianHangSearchDto : SearchBase
    {
		public int GianHangIdFilter { get; set; }
		public long? NhomDanhMucIdFilter { get; set; }
		public string TenThuocTinhFilter { get; set; }
		public string KieuDuLieuFilter { get; set; }
		public string NhomDanhmucCodeFilter { get; set; }


    }
}