using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.TaiKhoanService.Dto
{
    public class TaiKhoanSearchDto : SearchBase
    {
		public string CodeFilter { get; set; }
		public int? GameIdFilter { get; set; }
		public string TrangThaiFilter { get; set; }
		public string UserNameFilter { get; set; }
		public string PasswordFilter { get; set; }
		public int? GiaGocFilter { get; set; }
		public int? GiaKhuyenMaiFilter { get; set; }
		public string MotaFilter { get; set; }
		public int? ViTriFilter { get; set; }
        //
        public decimal? GiaMin { get; set; }
        public decimal? GiaMax { get; set; }
    }
}