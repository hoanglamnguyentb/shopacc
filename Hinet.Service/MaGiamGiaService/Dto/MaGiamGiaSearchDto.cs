using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.MaGiamGiaService.Dto
{
    public class MaGiamGiaSearchDto : SearchBase
    {
		public int SoLuongFilter { get; set; }
		public DateTime? TuNgayFilter { get; set; }
		public DateTime? DenNgayFilter { get; set; }
		public bool? ToanHeThongFilter { get; set; }
		public bool? TrangThaiFilter { get; set; }
		public string ThongTinFilter { get; set; }
		public string GianHangApDungFilter { get; set; }


    }
}