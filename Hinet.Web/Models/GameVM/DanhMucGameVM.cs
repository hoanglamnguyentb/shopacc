using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.GameService.Dto;
using Hinet.Service.TaiKhoanService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Models.GameVM
{
    public class DanhMucGameVM
    {
        public Game Game { get; set; }
        public DanhMucGame DanhMucGame { get; set; }
        public PageListResultBO<TaiKhoanDto> TaiKhoanPagedResult { get; set; }
    }
}