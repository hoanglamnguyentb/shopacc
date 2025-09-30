using System.ComponentModel;

namespace Hinet.Web.Areas.DashboardArea.Models
{
    public class WebInfoVM
    {
        [DisplayName("Website chờ duyệt")]
        public int ChoDuyet { get; set; }

        [DisplayName("Website cần bổ sung thông tin")]
        public int CanBoSungThongTin { get; set; }

        [DisplayName("Website đã duyệt điện tử")]
        public int DaDuyetDienTu { get; set; }

        [DisplayName("Website đã xác nhận")]
        public int DaXacNhan { get; set; }

        [DisplayName("Website bị từ chối")]
        public int BiTuChoi { get; set; }

        [DisplayName("Website đề nghị chỉnh sửa")]
        public int DeNghiChinhSua { get; set; }

        [DisplayName("Website đề nghị chấm dứt đăng ký")]
        public int ChamDutDangKy { get; set; }

        [DisplayName("Website đã chấm dứt đăng ký")]
        public int DaChamDutDangKy { get; set; }

        [DisplayName("Website đã hủy đăng ký")]
        public int DaHuyDangKy { get; set; } = 8;

        [DisplayName("Website đề nghị chấm dứt thông báo")]
        public int DeNghiChamDutThongBao { get; set; }

        [DisplayName("Website đã chấm dứt thông báo")]
        public int DaChamDutThongBao { get; set; }

        [DisplayName("Website đã hủy thông báo")]
        public int DaHuyThongBao { get; set; }

        [DisplayName("Website cần gia hạn")]
        public int CanGiaHan { get; set; }

        [DisplayName("Website đã yêu cầu gia hạn")]
        public int DaYeuCauGiaHan { get; set; }

        [DisplayName("Tổng số website")]
        public int TongSo { get; set; }
    }
}