using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GiaoDichService.Dto
{
    public class GiaoDichSearchDto : SearchBase
    {
		public long UserIdFilter { get; set; }
		public long DoiTuongIdFilter { get; set; }
		public string LoaiDoiTuongFilter { get; set; }
		public string LoaiGiaoDichFilter { get; set; }
		public string TrangThaiFilter { get; set; }
		public string PhuongThucThanhToanFilter { get; set; }
		public DateTime NgayGiaoDichFilter { get; set; }
		public DateTime? NgayThanhToanFilter { get; set; }


    }
}