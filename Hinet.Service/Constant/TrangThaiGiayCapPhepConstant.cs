using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class TrangThaiGiayCapPhepConstant 
    {
        [DisplayName("Thu hồi")]
        [Color(Color = "#FD8B51", Icon = "fa fa-close")]
        public static string THUHOI => "60583";
        [DisplayName("Hết hiệu lực")]
        [Color(Color = "#ff5a83", Icon = "fa fa-warning")]
        public static string HETHIEULUC => "60582";
        [DisplayName("Còn hiệu lực")]
        [Color(Color = "#198754", Icon = "fa fa-check-circle")]
        public static string CONHIEULUC => "60581";
    }
}
