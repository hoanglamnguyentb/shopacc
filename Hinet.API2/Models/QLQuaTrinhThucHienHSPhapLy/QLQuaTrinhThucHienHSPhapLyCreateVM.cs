using Hinet.API2.Core;
using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.QLQuaTrinhThucHienHSPhapLy
{
    ///<Summary>
    /// Gets the answer
    ///</Summary>
    public class QLQuaTrinhThucHienHSPhapLyCreateVM
    {
        ///<Summary>
        /// Id dự án
        ///</Summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long? IdDuAn { get; set; }

        ///<Summary>
        /// Id nhà đầu tư
        ///</Summary>
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long? IdNhaDauTu { get; set; }

        ///<Summary>
        /// Tên hồ sơ pháp lý
        ///</Summary>
        public string TenHoSoPhapLy { get; set; }

        ///<Summary>
        /// Thông tin chi tiết
        ///</Summary>
        public string ThongTinChiTiet { get; set; }

        ///<Summary>
        /// Tài liệu đính kèm
        ///</Summary>
        public FileDataFromClient TaiLieuDinhKemData { get; set; }
    }
}