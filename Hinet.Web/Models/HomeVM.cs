using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Hinet.Model.Entities;
using Hinet.Service.GameService.Dto;
using System.Collections.Generic;

namespace Hinet.Web.Models
{
    public class HomeVM
    {
        public List<DichVu> ListDichVu { set; get; }
        public List<Banner> ListBanner { set; get; }
        public List<GameDto> ListGame { set; get; }
        public List<TinTuc> ListTinTuc { get; set; }
    }
}