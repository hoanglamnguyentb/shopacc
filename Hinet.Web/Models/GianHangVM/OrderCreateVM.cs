using Hinet.Model.Entities;
using Hinet.Service.ThuocTinhGianHangService.Dto;
using System.Collections.Generic;

namespace Hinet.Web.Models.GianHangVM
{
    public class OrderCreateVM
    {
        public List<ThuocTinhGianHangDto> ThuocTinhs { get; set; }
        public List<DonHangGiaTriThuocTinh> GiaTriThuocTinhs { get; set; } = new List<DonHangGiaTriThuocTinh>();
        public int GianHangId { get; set; }
        public int VatPhamId { get; set; }
        public int? MaGiamGiaId { get; set; }
        public int SoLuong { get; set; } = 1;
        public string PhuongThucThanhToan { get; set; }
    }
}