using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Common
{
    public class FilterBaoCao
    {
        public List<int> Nams { get; set; }
        public List<string> KyBaoCaos { get; set; }
        public List<DoanhNghiepFilter> DoanhNghieps { get; set; }
        public string MaBieu { get; set; }
    }

    public class DoanhNghiepFilter
    {
        public string TenDoanhNghiep { get; set; }
        public long IdDoanhNghiep { set; get; }
    } 
}
