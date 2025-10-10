using System.ComponentModel.DataAnnotations;

namespace Hinet.API2.Models.TBCuaSoChoNhaDauTu
{
    public class TBCuaSoChoNhaDauTuEditVM
    {
        public long Id { get; set; }
        public string TieuDeThongBao { get; set; }
        public string NoiDungThongBao { get; set; }
        public string PhanHoi { get; set; }
        public bool IsNDTRead { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long IdNhaDauTu { get; set; }
    }
}