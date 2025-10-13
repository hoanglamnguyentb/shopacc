using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DonHangGiaTriThuocTinhService.Dto
{
    public class DonHangGiaTriThuocTinhSearchDto : SearchBase
    {
		public int DonHangIdFilter { get; set; }
		public int? ThuocTinhIdFilter { get; set; }
		public string ThuocTinhTxtFilter { get; set; }
		public string GiaTriFilter { get; set; }
		public string GiaTriTxtFilter { get; set; }
		public string KieuDuLieuFilter { get; set; }


    }
}