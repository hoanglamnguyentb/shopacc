using Hinet.Service.QlAntensService.Dto;
using Hinet.Service.TramBTSService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.QLCanhBao.Data
{
    public class saTisFyModel
    {
        public Dictionary<string, int> canhbaoTramBts { get; set; }
        public Dictionary<string, int> canhbaoAnten { get; set; }
    }
}