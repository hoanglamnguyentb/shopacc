using System.Collections.Generic;

namespace Hinet.Service.Common
{
    public class ChartModuleWraper
    {
        public List<ChartDto> chartDtos { get; set; }
        public string title { get; set; }
        public string TextOfLabel { get; set; }
        public string TextOfData { get; set; }

        /// <summary>
        /// Id của chart
        /// </summary>
        public string KeyModuleChartJs { get; set; }
    }
}