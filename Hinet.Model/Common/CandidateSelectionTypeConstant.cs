using System.ComponentModel;

namespace Hinet.Model.Common
{
    public class CandidateSelectionTypeConstant
    {
        [DisplayName("Phỏng vấn")]
        public static string Interview { get; set; } = "Interview";

        [DisplayName("Bài test")]
        public static string Test { get; set; } = "Test";
    }
}