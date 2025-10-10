namespace Hinet.Service.APIModel
{
    public class ThongKeGiaiNganCungKyTheoThangAM
    {
        public ThongKeGiaiNganCungKyTheoThangAMItem GiaTri { get; set; }
        public ThongKeGiaiNganCungKyTheoThangAMItem GiaTriCungKyNamTruoc { get; set; }
    }

    public class ThongKeGiaiNganCungKyTheoNamAM
    {
        public ThongKeGiaiNganCungKyTheoThangAMItem GiaTri { get; set; }
        public ThongKeGiaiNganCungKyTheoThangAMItem GiaTriCungKyGiaiDoanTruoc { get; set; }
    }

    public class ThongKeGiaiNganCungKyTheoThangAMItem
    {
        /// <summary>
        /// Tổng giá trị giải ngân
        /// </summary>
        public decimal TongGiaTriGiaiNgan { get; set; }

        /// <summary>
        /// Tổng giá trị theo kế hoạch
        /// </summary>
        public decimal TongGiaTriKeHoach { get; set; }

        /// <summary>
        /// Tỷ lệ giải ngân
        /// </summary>
        public decimal TyLeGiaiNgan
        {
            get
            {
                if (TongGiaTriKeHoach > 0)
                {
                    return TongGiaTriGiaiNgan / TongGiaTriKeHoach;
                }
                return 0;
            }
        }

        /// <summary>
        /// Cùng kỳ năm trước
        /// </summary>
    }
}