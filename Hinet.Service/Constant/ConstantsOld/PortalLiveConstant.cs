using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class PortalLiveConstant
    {
        [DisplayName("Trực tiếp sự kiện")]
        public static int TrucTiepSuKien { get; set; } = 1;

        [DisplayName("Tọa đàm trực tuyến")]
        public static int ToaDamTrucTuyen { get; set; } = 2;

        [DisplayName("Giao lưu trực tuyến")]
        public static int GiaoLuuTrucTuyen { get; set; } = 3;
    }
}