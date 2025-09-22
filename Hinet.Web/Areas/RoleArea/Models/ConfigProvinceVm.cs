using System.Collections.Generic;

namespace Hinet.Web.Areas.EndUserDocumentArea.Models
{
    public class ConfigProvinceVm
    {
        public long? VaiTroId { get; set; }
        public long? ChucNangId { get; set; }
        public List<string> MaTinh { get; set; }
        public List<string> MaHuyen { get; set; }
        public List<string> MaXa { get; set; }
    }
}