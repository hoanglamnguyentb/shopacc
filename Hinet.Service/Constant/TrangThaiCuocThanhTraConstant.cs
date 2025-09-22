using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class TrangThaiCuocThanhTraConstant
    {
        [DisplayName("Đã kết thúc")]
        [Color(Color = "#FD8B51")]
        public static string DAKETTHUC => "DAKETTHUC";
        [DisplayName("Chưa bắt đầu")]
        [Color(Color = "#B7B7B7")]
        public static string CHUABATDAU => "CHUABATDAU";
        [DisplayName("Đang diễn ra")]
        [Color(Color = "#72BF78")]
        public static string DANGDIENRA => "DANGDIENRA";
    }
}
