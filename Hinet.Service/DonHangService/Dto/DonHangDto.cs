using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinet.Service.Common;
using Hinet.Service.Constant;

namespace Hinet.Service.DonHangService.Dto
{
    public class DonHangDto : DonHang
    {
        public GianHang GianHang { get; set; }
        public VatPham VatPham { get; set; }
        public MaGiamGia MaGiamGia { get; set; }
        public List<DonHangGiaTriThuocTinh> ListGTTT { get; set; }

        public string TrangThaiHTML
        {
            get
            {
                var trangThaiTxt = ConstantExtension.GetName<TrangThaiGiaoDichConstant>(TrangThai);
                var bgColor = ConstantExtension.GetColor<TrangThaiGiaoDichConstant>(TrangThai);
                return $"<span class='badge' style='background-color: {bgColor};'>{trangThaiTxt}</span>";
            }
        }
    }
}