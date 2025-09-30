using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.GameService.Dto
{
    public class GameSearchDto : SearchBase
    {
		public string NameFilter { get; set; }
		public string MoTaFilter { get; set; }
		public string TrangThaiFilter { get; set; }


    }
}