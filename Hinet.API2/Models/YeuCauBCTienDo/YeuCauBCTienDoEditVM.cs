using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.YeuCauBCTienDo
{
    public class YeuCauBCTienDoEditVM
    {
        public long Id { get; set; }
        public string TieuDeYeuCau { get; set; }
        public string NoiDungYeuCau { get; set; }
        public string PhanHoi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdNhaDauTu { get; set; }
    }
}