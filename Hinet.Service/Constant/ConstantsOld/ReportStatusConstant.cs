using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class ReportStatusConstant
    {
        /// <summary>
        /// Đối tượng phản ánh đã xác định được địa phương
        /// </summary>
        [DisplayName("Đã xác định được địa phương")]
        public static int DaXacDinh { get; set; } = 1;

        /// <summary>
        /// Đối tượng phản ánh chưa xác định được địa phương
        /// </summary>
        [DisplayName("Chưa xác định được địa phương")]
        public static int ChuaXacDinh { get; set; } = 0;
    }
}