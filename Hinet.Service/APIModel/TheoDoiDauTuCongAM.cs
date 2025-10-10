using System;
using System.Collections.Generic;

namespace Hinet.Service.APIModel
{
    public class TheoDoiDauTuCongAM
    {
        /// <summary>
        /// Tổng số dự án trong kế hoạch
        /// </summary>
        public int TongSoDuAnTrongHeHoach { get; set; }

        /// <summary>
        /// Tổng số dự án đã giải ngân
        /// </summary>
        public int TongSoDuAnDaGiaiNgan { get; set; }

        /// <summary>
        /// Tổng giá trị kế hoạch vốn
        /// </summary>
        public decimal TongGiaTriKeHoachVon { get; set; }

        /// <summary>
        /// Tổng giá trị giải ngân
        /// </summary>
        public decimal TongGiaTriGiaiNgan { get; set; }

        /// <summary>
        /// Tổng giá trị thực hiện
        /// </summary>
        public decimal TongGiaTriThucHien { get; set; }

        /// <summary>
        /// Danh sách chi tiết tình hình giải ngân
        /// </summary>
        public List<ThongTinGiaiNganItem> GiaiNganChiTiet { get; set; }

        public DateTime? ThoiGianXetSoLieu { get; set; }
    }

    public class ThongTinGiaiNganItem
    {
        /// <summary>
        /// ID định danh chủ đầu tư
        /// </summary>
        public long? IdChuDauTu { get; set; }

        /// <summary>
        /// Tên chủ đầu tư
        /// </summary>
        public string TenChuDauTu { get; set; }

        /// <summary>
        /// Tỷ lệ giải ngân
        /// </summary>
        public decimal TyLeGiaiNGan
        {
            get
            {
                if (TongGiaTriThucHien > 0)
                {
                    return TongGiaTriGiaiNgan / TongGiaTriThucHien;
                }
                return 100;
            }
        }

        /// <summary>
        /// Tổng giá trị thực hiện
        /// </summary>
        public decimal TongGiaTriThucHien { get; set; }

        /// <summary>
        /// Tổng giá trị giải ngân
        /// </summary>
        public decimal TongGiaTriGiaiNgan { get; set; }
    }

    public class ItemGroupDuAnChuDauTu
    {
        public long? IdChuDauTu { get; set; }
        public long IdDuAn { get; set; }
    }
}