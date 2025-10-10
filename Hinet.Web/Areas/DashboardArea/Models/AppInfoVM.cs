using System.ComponentModel;

namespace Hinet.Web.Areas.DashboardArea.Models
{
    public class AppInfoVM
    {
        [DisplayName("Ứng dụng chờ duyệt")]
        public int ChoDuyet { get; set; }

        [DisplayName("Ứng dụng cần bổ sung thông tin")]
        public int CanBoSungThongTin { get; set; }

        [DisplayName("Ứng dụng đã duyệt điện tử")]
        public int DaDuyetDienTu { get; set; }

        [DisplayName("Ứng dụng đã xác nhận")]
        public int DaXacNhan { get; set; }

        [DisplayName("Ứng dụng bị từ chối")]
        public int BiTuChoi { get; set; }

        [DisplayName("Ứng dụng đề nghị chỉnh sửa")]
        public int DeNghiChinhSua { get; set; }

        [DisplayName("Ứng dụng đề nghị chấm dứt đăng ký")]
        public int ChamDutDangKy { get; set; }

        [DisplayName("Ứng dụng đã chấm dứt đăng ký")]
        public int DaChamDutDangKy { get; set; }

        [DisplayName("Ứng dụng đã hủy đăng ký")]
        public int DaHuyDangKy { get; set; }

        [DisplayName("Ứng dụng đề nghị chấm dứt thông báo")]
        public int DeNghiChamDutThongBao { get; set; }

        [DisplayName("Ứng dụng đã chấm dứt thông báo")]
        public int DaChamDutThongBao { get; set; }

        [DisplayName("Ứng dụng đã hủy thông báo")]
        public int DaHuyThongBao { get; set; }

        [DisplayName("Ứng dụng cần gia hạn")]
        public int CanGiaHan { get; set; }

        [DisplayName("Ứng dụng đã yêu cầu gia hạn")]
        public int DaYeuCauGiaHan { get; set; }

        [DisplayName("Tổng số ứng dụng")]
        public int TongSo { get; set; }
    }
}