using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DanhMucGameTaiKhoanService.Dto
{
    public class DanhMucGameTaiKhoanSearchDto : SearchBase
    {
		public int DanhMucGameIdFilter { get; set; }
		public long TaiKhoanIdFilter { get; set; }


    }
}