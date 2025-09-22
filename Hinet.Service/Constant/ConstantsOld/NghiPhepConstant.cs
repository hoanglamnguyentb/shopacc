using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class NghiPhepConstant
    {
        [Color(Color = "#7f8c8d")]
        [DisplayName("Bản thảo")]
        public static string BanThao { get; set; } = "BANTHAO";

        [Color(Color = "#34495e")]
        [DisplayName("Đã trình")]
        public static string DaTrinh { get; set; } = "DATRINH";

        [DisplayName("Đã duyệt")]
        [Color(Color = "#2980b9")]
        public static string DaDuyet { get; set; } = "YES";

        [DisplayName("Đã từ chối")]
        [Color(Color = "#c0392b")]
        public static string DaTuChoi { get; set; } = "NO";
    }
}