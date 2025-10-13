using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class TrangThaiGianHangConstant
    {
        [DisplayName("Bật")]
        [Color(Color = "#FFC107", BgColor = "#FFF8E1", Icon = "bi bi-clock")]
        public static string BAT => "BAT";

        [DisplayName("Tắt")]
        [Color(Color = "#FFC107", BgColor = "#FFF8E1", Icon = "bi bi-clock")]
        public static string TAT => "TAT";
    }
}