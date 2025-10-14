using Hinet.Model.Entities;
using Hinet.Service.ThuocTinhGianHangService.Dto;
using System.Collections.Generic;

namespace Hinet.Web.Models.GianHangVM
{
    public class IndexVM
    {
        public GianHang GianHang { get; set; }
        public List<VatPham> ListVatPhat { get; set; }
        public List<ThuocTinhGianHangDto> ThuocTinhs { get; set; }
    }
}