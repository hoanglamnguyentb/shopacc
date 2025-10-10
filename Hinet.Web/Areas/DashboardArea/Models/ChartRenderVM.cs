using Hinet.Service.Common;
using System.Collections.Generic;

namespace Hinet.Web.Areas.DashboardArea.Models
{
    public class ChartRenderVM
    {
        public long TypeChart { get; set; }
        public int Year { get; set; }
        public List<ChartDto> ChartDtos { get; set; }
    }
}