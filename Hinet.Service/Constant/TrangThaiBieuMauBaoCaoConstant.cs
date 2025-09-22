using CommonHelper.ObjectExtention;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class TrangThaiBieuMauBaoCaoConstant
    {
        [DisplayName("Chưa gửi")]
        [Color(Color = "#FD8B51", Icon = "fa fa-warning")]
        public static string CHUAGUI { get; set; } = "CHUAGUI";

        [DisplayName("Đã gửi")]
        [Color(Color = "#9ecefd", Icon = "fa fa-share")]
        public static string DAGUI { get; set; } = "DAGUI";

        [DisplayName("Từ chối")]
        [Color(Color = "#ff5a83", Icon = "fa fa-close")]
        public static string TUCHOI { get; set; } = "TUCHOI";

        [DisplayName("Chấp nhận")]
        [Color(Color = "#198754", Icon = "fa fa-check-circle")]
        public static string CHAPNHAN { get; set; } = "CHAPNHAN";
    }
}