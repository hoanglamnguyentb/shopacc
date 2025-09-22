using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class TrangThaiBaoCaoConstant
    {
        [Display(Name = "Chưa gửi")]
        public static string ChuaGui { get; set; } = "ChuaGui";

        [Display(Name = "Đã gửi")]
        public static string DaGui { get; set; } = "DaGui";
    }
}
