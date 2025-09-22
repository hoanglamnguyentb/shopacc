using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TrangThaiPheDuyet
    {
        [DisplayName("Phê duyệt")]
        public static string PheDuyet { get; set; } = "PheDuyet";

        [DisplayName("Chờ phê duyệt")]
        public static string ChoPheDuyet { get; set; } = "ChoPheDuyet";
    }

    public class TrangThaiPheDuyetThietBi
    {
        [DisplayName("Đang chờ duyệt")]
        public static string DaPheDuyet { get; set; } = "DAPHEDUYET";

        [DisplayName("Chờ phê duyệt")]
        public static string ChoPheDuyet { get; set; } = "CHOPHEDUYET";

        [DisplayName("Từ chối phê duyệt")]
        public static string TuChoiPheDuyet { get; set; } = "TUCHOIPHEDUYET";
    }

    public class TrangThaiPheDuyetKetNoi
    {
        [DisplayName("Đã phê duyệt")]
        [Color(BgColor = "#2ecc71")]
        public static string DaPheDuyet { get; set; } = "DAPHEDUYET";

        [DisplayName("Chờ phê duyệt")]
        [Color(BgColor = "#f1c40f")]
        public static string ChoPheDuyet { get; set; } = "CHOPHEDUYET";

        [DisplayName("Từ chối phê duyệt")]
        [Color(BgColor = "#e74c3c")]
        public static string TuChoiPheDuyet { get; set; } = "TUCHOIPHEDUYET";

        [DisplayName("Bổ sung thông tin")]
        [Color(BgColor = "#8e44ad")]
        public static string BoSung { get; set; } = "BOSUNG";
    }

    public class TrangThaiPheDuyetYC
    {
        [DisplayName("Phê duyệt")]
        [Color(BgColor = "#2ecc71")]
        public static string DaPheDuyet { get; set; } = "DAPHEDUYET";

        [DisplayName("Từ chối")]
        [Color(BgColor = "#e74c3c")]
        public static string TuChoiPheDuyet { get; set; } = "TUCHOIPHEDUYET";
    }

    public class TrangThaiThamDinh
    {
        [DisplayName("Chờ thẩm định")]
        [Color(BgColor = "#f1c40f")]
        public static string ChoThamDinh { get; set; } = "CHOTHAMDINH";

        [DisplayName("Từ chối thẩm định")]
        [Color(BgColor = "#e74c3c")]
        public static string TuChoiThamDinh { get; set; } = "TUCHOITHAMDINH";

        [DisplayName("Đã thẩm định")]
        [Color(BgColor = "#2ecc71")]
        public static string DaThamDinh { get; set; } = "DATHAMDINH";
    }

    public class TrangThaiThamDinhYeuCau
    {
        [DisplayName("Từ chối")]
        [Color(BgColor = "#e74c3c")]
        public static string TuChoiThamDinh { get; set; } = "TUCHOITHAMDINH";

        [DisplayName("Thẩm định")]
        [Color(BgColor = "#2ecc71")]
        public static string DaThamDinh { get; set; } = "DATHAMDINH";
    }

    public class TrangThaiTinBai
    {
        [DisplayName("Đã xuất bản")]
        public static string DaXuatBan { get; set; } = "DAXUATBAN";

        [DisplayName("Gỡ bỏ")]
        public static string GoBo { get; set; } = "GOBO";

        [DisplayName("Bản nháp")]
        public static string BanNhap { get; set; } = "BANNHAP";
    }

    public class TrangThaiThietBiQuanTrac
    {
        [DisplayName("Đang đo")]
        public static string DangDo { get; set; } = "00";

        [DisplayName("Hiệu chuẩn")]
        public static string HieuChuan { get; set; } = "01";

        [DisplayName("Báo lỗi thiết bị")]
        public static string BaoLoi { get; set; } = "02";
    }

    public class TrangThaiYeuCauHieuChinh
    {
    }

    public class LoaiDoanhNghiepConstant
    {
        [DisplayName("Khu công nghiệp")]
        public static string KhuCN { get; set; } = "KhuCN";

        [DisplayName("Cụm công nghiệp")]
        public static string CumCN { get; set; } = "CumCN";
    }

    public class ThucTrangDoanhNghiepTrongKCN
    {
        //[DisplayName("Đang hoạt động ổn định")]
        //public static string DangHoatDongOnDinh { get; set; } = "DangHoatDongOnDinh";
        //[DisplayName("Dự án mới đang triển khai")]
        //public static string DuAnMoiDangTrienKhai { get; set; } = "DuAnMoiDangTrienKhai";
        //[DisplayName("Đang xây dựng cơ bản")]
        //public static string DangXayDungCoBan { get; set; } = "DangXayDungCoBan";
        //[DisplayName("Đã dừng hoạt động ")]
        //public static string DaDungHoatDong { get; set; } = "DaDungHoatDong";
        //[DisplayName("Chưa được giao đất")]
        //public static string ChuaDuocGiaoDat { get; set; } = "ChuaDuocGiaoDat";

        [DisplayName("Đã hoạt động")]
        public static string DAHOATDONG { get; set; } = "DAHOATDONG";

        [DisplayName("Đang xây dựng")]
        public static string DANGXAYDUNG { get; set; } = "DANGXAYDUNG";

        [DisplayName("Chưa triển khai")]
        public static string CHUATRIENKHAI { get; set; } = "CHUATRIENKHAI";
    }
}