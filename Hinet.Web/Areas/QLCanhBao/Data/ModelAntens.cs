using DocumentFormat.OpenXml.Spreadsheet;
using Hinet.Service.Common;
using Hinet.Service.QlAntensService.Dto;
using ServiceStack.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.QLCanhBao.Data
{
    public class ModelAntens
    {
        public PageListResultBO<QlAntensDto> CONHANLST { get; set; }
        public PageListResultBO<QlAntensDto> HETHANLST { get; set; }
        public PageListResultBO<QlAntensDto> SAPHETHANLST { get; set; }

        public Dictionary<string,int> keyCanhBao { get; set; }


    }
}