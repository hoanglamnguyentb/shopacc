using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class NguonHinhThanhTaiSanStatusConstant
    {
        [DisplayName("Nguồn ngân sách nhà nước")]
        [Color(BgColor = "#bbdefb")]
        public static string NguonNganSachNhaNuoc { get; set; } = "NguonNganSachNhaNuoc";

        [DisplayName("Nguồn thu đơn vị")]
        [Color(BgColor = "#81c784")]
        public static string NguonThuDonVi { get; set; } = "NguonThuDonVi";
    }
}