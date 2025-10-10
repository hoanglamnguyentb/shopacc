using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.SiteConfigService.Dto
{
    public class SiteConfigSearchDto : SearchBase
    {
		public string DescriptionFilter { get; set; }
		public string KeywordsFilter { get; set; }
		public string OgTitleFilter { get; set; }
		public string OgDescriptionFilter { get; set; }
		public string OgImageFilter { get; set; }
		public string SiteTitleFilter { get; set; }
		public string FaviconFilter { get; set; }
		public string LogoFilter { get; set; }


    }
}