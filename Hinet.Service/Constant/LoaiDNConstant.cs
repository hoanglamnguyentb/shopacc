using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LoaiDNConstant
    {
        [DisplayName("Doanh nghiệp bưu chính")]
        public static string DOANHNGHIEPBUUCHINH { set; get; } = "DOANHNGHIEPBUUCHINH";

        [DisplayName("Doanh nghiệp viễn thông")]
        public static string DOANHNGHIEPVIENTHONG { set; get; } = "DOANHNGHIEPVIENTHONG";

        [DisplayName("Doanh nghiệp CNTT, ĐTVT")]
        public static string DoanhNghiepCNTTDTVT { set; get; } = "DOANHNGHIEPCNTTDTVT";

        [DisplayName("Doanh nghiệp in")]
        public static string DOANHNGHIEPIN { set; get; } = "DOANHNGHIEPIN";

        [DisplayName("Doanh nghiệp xuất bản phẩm")]
        public static string DOANHNGHIEPXBP { set; get; } = "DOANHNGHIEPXBP";

        [DisplayName("Doanh nghiệp THTT")]
        public static string DOANHNGHIEPTHTT { set; get; } = "DOANHNGHIEPTHTT";

        [DisplayName("Đối tượng thanh tra")]
        public static string DOITUONGTHANHTRA { set; get; } = "DOITUONGTHANHTRA";

        [DisplayName("Cơ quan báo chí")]
        public static string COQUANBAOCHI { set; get; } = "COQUANBAOCHI";

        [DisplayName("Sở TTTT")]
        public static string SOTTTT { set; get; } = "SOTTTT";

        [DisplayName("Huyện")]
        public static string HUYEN { set; get; } = "HUYEN";

        [DisplayName("Xã")]
        public static string XA { set; get; } = "XA";
    }

    public class NhomLinhVucHoatDongChinh_CNTT
    {
        [DisplayName("Doanh nghiệp phần cứng, điện tử")]
        public static string PhanCungDienTu { set; get; } = "PhanCungDienTu";

        [DisplayName("Doanh nghiệp phần mềm")]
        public static string PhanMem { set; get; } = "PhanMem";

        [DisplayName("Doanh nghiệp nội dung số")]
        public static string NoiDungSo { set; get; } = "NoiDungSo";

        [DisplayName("DN cung cấp dịch vụ CNTT (trừ buôn bán, phân phối)")]
        public static string CungCapDichVu { set; get; } = "CapDichVu";
    }
}