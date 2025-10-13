using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DonHangService.Dto
{
    public class DonHangSearchDto : SearchBase
    {
		public int DonHangIdFilter { get; set; }
		public int VatPhamIdFilter { get; set; }
		public int MaGiamGiaFilter { get; set; }
		public int GiaGocFilter { get; set; }
		public int GiaKhuyenMaiFilter { get; set; }
		public string TrangThaiFilter { get; set; }
		public string QrUrlFilter { get; set; }


    }
}