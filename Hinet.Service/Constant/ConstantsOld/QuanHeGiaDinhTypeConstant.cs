using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class QuanHeGiaDinhTypeConstant
    {
        [DisplayName("Bản thân")]
        public static string VeBanThan { get; set; } = "VeBanThan";

        [DisplayName("Về bên vợ (hoặc chồng)")]
        public static string VeBenVoOrChong { get; set; } = "VeBenVoOrChong";
    }
}