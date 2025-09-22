using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeContentConstant
    {
        [DisplayName("Bài viết")]
        public static string BaiViet { get; set; } = "1";

        [DisplayName("Ảnh")]
        public static string Anh { get; set; } = "2";

        [DisplayName("Video")]
        public static string Video { get; set; } = "3";

        [DisplayName("Tin")]
        public static string Tin { get; set; } = "4";

        [DisplayName("Tin ảnh")]
        public static string TinAnh { get; set; } = "5";

        [DisplayName("Bài ảnh")]
        public static string BaiAnh { get; set; } = "6";
    }

    public class TypeContentBaoInConstant
    {
        [DisplayName("Bài")]
        public static string BaiViet { get; set; } = "1";

        [DisplayName("Tin")]
        public static string Tin { get; set; } = "4";
    }

    public class TypeContentTableConstant
    {
        [DisplayName("Bài viết")]
        public static string BaiViet { get; set; } = "BaiViet";

        [DisplayName("Ảnh")]
        public static string Anh { get; set; } = "Anh";

        [DisplayName("Video")]
        public static string Video { get; set; } = "Video";
    }

    public class TypeSystemConstant
    {
        [DisplayName("Báo Giấy")]
        public static int BaoGiay { get; set; } = 2;

        [DisplayName("Báo điện tử")]
        public static int BaoDienTu { get; set; } = 1;
    }
}