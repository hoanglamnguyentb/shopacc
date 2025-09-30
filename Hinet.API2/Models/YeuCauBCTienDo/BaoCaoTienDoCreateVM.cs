using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.YeuCauBCTienDo
{
    ///<Summary>
    /// Báo cáo tiến độ
    ///</Summary>
    public class BaoCaoTienDoCreateVM
    {
        ///<Summary>
        /// Id yêu cầu
        ///</Summary>
        public long IdYeuCau { get; set; }

        ///<Summary>
        /// Báo cáo tiến độ
        ///</Summary>
        public decimal KeHoachVon { get; set; }

        ///<Summary>
        /// Báo cáo tiến độ
        ///</Summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public decimal GiaiNganKeHoachVon { get; set; }

        ///<Summary>
        /// Báo cáo tiến độ
        ///</Summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public double PhanTramKeHoach { get; set; }
    }
}