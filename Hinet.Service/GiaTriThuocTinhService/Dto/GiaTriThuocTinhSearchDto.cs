using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GiaTriThuocTinhService.Dto
{
    public class GiaTriThuocTinhSearchDto : SearchBase
    {
		public int TaiKhoanIdFilter { get; set; }
		public string ThuocTinhIdFilter { get; set; }
		public string ThuocTinhTxtFilter { get; set; }
		public string GiaTriFilter { get; set; }
		public string GiaTriTextFilter { get; set; }


    }
}