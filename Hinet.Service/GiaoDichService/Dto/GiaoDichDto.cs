using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinet.Service.Common;
using Hinet.Service.Constant;

namespace Hinet.Service.GiaoDichService.Dto
{
    public class GiaoDichDto : GiaoDich
    {
        public string NguoiGiaoDichTxt { get; set; }
        public string TaiKhoanTxt { get; set; }
        public string LoaiDoiTuongTxt { get {
                return
                    ConstantExtension.GetName<LoaiDoiTuongConstant>(LoaiDoiTuong);
            } }
        public string LoaiGiaoDichTxt
		{
			get
			{
				return
					ConstantExtension.GetName<LoaiGiaoDichConstant>(LoaiGiaoDich);
			}
		}
        public string TrangThaiTxt
		{
			get
			{
                return
                    ConstantExtension.GetName<TrangThaiGiaoDichConstant>(TrangThai);
			}
		}

        public string TrangThaiHTML
        {
            get
            {
                var trangThaiTxt = ConstantExtension.GetName<TrangThaiGiaoDichConstant>(TrangThai);
                var bgColor = ConstantExtension.GetColor<TrangThaiGiaoDichConstant>(TrangThai);
                return $"<span class='badge' style='background-color: {bgColor};'>{trangThaiTxt}</span>";
            }
        }

        public string PhuongThucThanhToanTxt
		{
			get
			{
				return
					ConstantExtension.GetName<PhuongThucThanhToanConstant>(PhuongThucThanhToan);
			}
		}
        public TaiKhoan TaiKhoan { get; set; }
        public VatPham VatPham { get; set; }
        public GianHang GianHang { get; set; }
        public List<GiaTriThuocTinh> ListGTTT { get; set; }
        public List<DonHangGiaTriThuocTinh> ListDonHangGTTT { get; set; }
    }
}