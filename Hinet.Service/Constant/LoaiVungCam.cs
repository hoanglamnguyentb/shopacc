using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class LoaiVungCam
    {
        [DisplayName("Vùng hạn chế độ cao")]
        public static string VungHanCheDoCao { get; set; } = "VungHanCheDoCao";

        [DisplayName("Vùng cấm triển khải")]
        public static string VungCamTrienKhai { get; set; } = "VungCamTrienKhai";
    }
}
