using Hinet.API2.Core;

namespace Hinet.API2.Models.PhanHoi
{
    public class PhanHoiCreateVM
    {
        public string NoiDungPhanHoi { get; set; }
        public long IdYc { get; set; }
        public FileDataFromClient TaiLieuDinhKemData { get; set; }
    }
}