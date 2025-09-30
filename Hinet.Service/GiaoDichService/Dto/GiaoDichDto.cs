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
        public string NguoiGiaoDich { get; set; }
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
        public string PhuongThucThanhToanTxt
		{
			get
			{
				return
					ConstantExtension.GetName<PhuongThucThanhToanConstant>(PhuongThucThanhToan);
			}
		}
    }
}