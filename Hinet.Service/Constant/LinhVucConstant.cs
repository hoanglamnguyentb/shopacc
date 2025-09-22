using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.Constant
{
    public class LinhVucConstant
    {
        [DisplayName("Bưu chính")]
        public static string BUUCHINH { set; get; } = "BUUCHINH";
        [DisplayName("Viễn thông")]
        public static string VIENTHONG { set; get; } = "VIENTHONG";
        [DisplayName("Công nghệ thông tin, điện tử viễn thông")]
        public static string CNTT_DTVT { set; get; } = "CNTT_DTVT";
        [DisplayName("An toàn thông tin mạng")]
        public static string ANTOANTHONGTIN { set; get; } = "ANTOANTHONGTIN";
        [DisplayName("Ứng dụng công nghệ thông tin")]
        public static string UNGDUNGCNTT { set; get; } = "UNGDUNGCNTT";
        [DisplayName("Báo chí, truyền thông")]
        public static string BAOCHI_TRUYENTHONG { set; get; } = "BAOCHI_TRUYENTHONG";

    }
}
