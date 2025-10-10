using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.TienDoDuAnDauTu
{
    public class TienDoDuAnDauTuCreateVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdDuAn { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string TrangThai { get; set; }

        public string MucDoTrienKhai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public float TienDo { get; set; }

        public string TienDoCuThe { get; set; }
        //public string PathUrl { get; set; }
    }
}