using System.ComponentModel;

namespace Hinet.Service.Constant.DoanhNghiep
{
    public class HoatDongDoanhNghiepConstant
    {
        //Hoạt động tập huấn đào tạo
        [DisplayName("Hoạt động tập huấn đào tạo")]
        public static string HoatDongTapHuan { get; set; } = "HoatDongTapHuanDaoTao";

        //Hoạt động chuyển giao công nghệ, chuyển giao kỹ thuật của Nhà đàu tư đến các cá nhân tổ chức
        [DisplayName("Hoạt động chuyển giao công nghệ, chuyển giao kỹ thuật của Nhà đàu tư đến các cá nhân tổ chức")]
        public static string HoatDongChuyenGiao { get; set; } = "HoatDongChuyenGiaoCongNghe";

        //Hoạt động hỗ trợ trực tiếp sản xuất đến các cá nhân, tổ chức
        [DisplayName("Hoạt động hỗ trợ trực tiếp sản xuất đến các cá nhân, tổ chức")]
        public static string HoatDongHoTroSanXuat { get; set; } = "HoatDongHoTroSanXuat";

        //Hoạt động Hỗ trợ trong công tác học tập, nghiên cứu
        [DisplayName("Hoạt động Hỗ trợ trong công tác học tập, nghiên cứu")]
        public static string HoatDongHoTroCongTacHocTap { get; set; } = "HoatDongHoTroCongTacHocTap";
    }

    public class HoatDongTrangThaiConstant
    {
        [DisplayName("Đã hoàn thành")]
        public static string DaHoanThanh { get; set; } = "DAHOANTHANH";

        [DisplayName("Chưa hoàn thành")]
        public static string ChuaHoanThanh { get; set; } = "ChuaHoanThanh";
    }
}