using Hinet.Service.Common;
using Hinet.Service.QlAntensService.Dto;
using Hinet.Service.TramBTSService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.QLCanhBao.Data
{
    public class ModelTramBts
    {
        public PageListResultBO<TramBTSDto> CONHANLST { get; set; }
        public PageListResultBO<TramBTSDto> HETHANLST { get; set; }
        public PageListResultBO<TramBTSDto> SAPHETHANLST { get; set; }

        public Dictionary<string, int> keyCanhBao { get; set; }

    }
}