using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class NghienCuuKhoaHocLoaiHinhTrienKhaiconstant
    {
        [DisplayName("Ngiên Cứu Cơ Bản")]
        public static string NghienCuuCoBan { get; set; } = "Nghiên Cứu Cơ Bản";

        [DisplayName("Nghiên Cứu Ứng Dụng")]
        public static string NghienCuuUngDung { get; set; } = "Nghiên Cứu Ứng dụng";

        [DisplayName("Nghiên Cứu Thực Nghiệm")]
        public static string TrienKhaiThucNghiem { get; set; } = "Nghiên Cứu Thực Nghiệm";
    }
}