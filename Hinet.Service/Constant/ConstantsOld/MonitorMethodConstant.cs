using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class MonitorMethodConstant
    {
        [DisplayName("Hosting nước ngoài")]
        public static string HostinNuocNgoai { get; set; } = "HostingNuocNgoai";

        [DisplayName("Hosting trong nước")]
        public static string HostinTrongNuoc { get; set; } = "HostinTrongNuoc";

        [DisplayName("Tự cung cấp dịch vụ Hosting")]
        public static string TuCungCapDichVuHosting { get; set; } = "TuCungCapDichVuHosting";
    }
}