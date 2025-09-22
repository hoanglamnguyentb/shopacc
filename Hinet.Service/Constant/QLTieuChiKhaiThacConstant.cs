using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiCauHinh
    {
        [DisplayName("Dạng text")]
        public static string TEXT { get; set; } = "TEXT";

        [DisplayName("Dạng số")]
        public static string NUMBER { get; set; } = "NUMBER";

        [DisplayName("Lấy từ tiêu chí báo cáo")]
        public static string TIEUCHIBAOCAO { get; set; } = "TIEUCHIBAOCAO";

        [DisplayName("Lấy từ API")]
        public static string API { get; set; } = "API";
    }

    public class Style
    {
        [DisplayName("Chữ đậm")]
        public static string BOLD { get; set; } = "bolder";

        [DisplayName("Chữ nghiêng")]
        public static string ITALIC { get; set; } = "italic";

        [DisplayName("Chữ đậm, nghiêng")]
        public static string BOLDITALIC { get; set; } = "bold italic";
    }
}