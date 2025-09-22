using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class TinhTrangHoatDongBuuCuc_DiemPhucVuBuuChinhConstant
    {
        [DisplayName("Tạm ngưng")]
        public static string TamNgung { get; set; } = "TamNgung";
        [DisplayName("Dừng hoạt động")]

        public static string DungHoatDong { get; set; } = "DungHoatDong";
        [DisplayName("Đang hoạt động")]
        public static string DangHoatDong { get; set; } = "DangHoatDong";

    }
}
