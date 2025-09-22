using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.YeuCauGiaiNgan
{
    ///<Summary>
    /// Yêu cầu giải ngân Edit
    ///</Summary>
    public class YeuCauGiaiNganEditVM
    {
        ///<Summary>
        /// Id yêu cầu giải ngân
        ///</Summary>
        public long Id { get; set; }

        ///<Summary>
        /// Tiêu đề yêu cầu
        ///</Summary>
        public string TieuDeYeuCau { get; set; }

        ///<Summary>
        /// Nội dung yêu cầu
        ///</Summary>
        public string NoiDungYeuCau { get; set; }

        ///<Summary>
        /// Phản hồi
        ///</Summary>
        public string PhanHoi { get; set; }

        ///<Summary>
        /// Gets the answer
        ///</Summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdNhaDauTu { get; set; }

        ///<Summary>
        /// Id dự án
        ///</Summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdDuAn { get; set; }
    }
}