using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.BinhLuanService.Dto
{
    public class BinhLuanSearchDto : SearchBase
    {
		public long? NguoiBinhLuanIdFilter { get; set; }
		public long DoiTuongIdFilter { get; set; }
		public string LoaiDoiTuongFilter { get; set; }
		public string NoiDungFilter { get; set; }
		public int? DiemFilter { get; set; }
		public long? ParentIdFilter { get; set; }
		public string TrangThaiFilter { get; set; }


    }
}