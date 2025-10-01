using Hinet.Model.Entities;
using Hinet.Service.DanhMucGameService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Models.GameVM
{
    public class IndexVM
    {
        public Game Game { get; set; }
        public List<DanhMucGameDto> ListDanhMucGame { get; set; }
    }
}