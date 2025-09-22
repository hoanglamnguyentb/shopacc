using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class WebsiteGanBieuTuongTinNhiemConstant
    {
        [DisplayName("Đã thêm vào danh sách")]
        [Color(BgColor = "#2980b9")]
        public static int DaThemVaoDanhSach { get; set; } = 1;

        [DisplayName("Đã đưa ra khỏi danh sách")]
        [Color(BgColor = "#e74c3c")]
        public static int DaDuaRaKhoiDanhSach { get; set; } = 2;

        [DisplayName("Lưu tạm")]
        public static int LuuTam { get; set; } = -1;

        [DisplayName("Tất cả")]
        public static int All { get; set; } = -1;
    }
}