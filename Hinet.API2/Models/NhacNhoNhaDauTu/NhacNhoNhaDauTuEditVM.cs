using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.NhacNhoNhaDauTu
{
    public class NhacNhoNhaDauTuEditVM
    {
        public long Id { get; set; }
        public string TieuDeNhacNho { get; set; }
        public string NoiDungNhacNho { get; set; }
        public string PhanHoi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdNhaDauTu { get; set; }
    }
}