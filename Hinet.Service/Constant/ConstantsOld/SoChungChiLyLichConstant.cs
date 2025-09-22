using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class SoChungChiLyLichConstant
    {
        [DisplayName("Đã có chứng chỉ")]
        public static bool DaCoChungChi { get; set; } = true;

        [DisplayName("Chưa có chứng chi")]
        public static bool ChuaCoChungChi { get; set; } = false;
    }
}