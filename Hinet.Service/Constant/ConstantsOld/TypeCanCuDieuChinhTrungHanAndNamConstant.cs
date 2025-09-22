using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeCanCuDieuChinhTrungHanAndNamConstant
    {
        [DisplayName("Trung Hạn")]
        public static int TrungHan { get; set; } = 0;

        [DisplayName("Năm")]
        public static int Nam { get; set; } = 1;
    }
}