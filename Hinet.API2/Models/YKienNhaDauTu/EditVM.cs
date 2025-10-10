using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.YKienNhaDauTu
{
    public class EditVM
    {
        public long Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdNhaDauTu { get; set; }
    }
}