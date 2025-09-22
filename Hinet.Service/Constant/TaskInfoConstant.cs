using Hinet.Service.Common;
using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TaskInfoConstant
    {
        public static bool DaXong { get; set; } = true;
        public static bool ChuaXong { get; set; } = false;
    }

    public class TaskTypeItemConstant
    {
        [DisplayName("Cá nhân")]
        [Color(BgColor = "#e58e26")]
        public static string Personal { get; set; } = "Personal";

        [DisplayName("Cá nhân")]
        [Color(BgColor = "#e58e26")]
        public static string Company { get; set; } = "Company";

        [DisplayName("Cá nhân")]
        [Color(BgColor = "#e58e26")]
        public static string Organization { get; set; } = "Organization";

        [DisplayName("Cá nhân")]
        [Color(BgColor = "#fff200")]
        public static string Website { get; set; } = "Website";

        [DisplayName("Cá nhân")]
        [Color(BgColor = "#7efff5")]
        public static string WebsiteNoti { get; set; } = "WebsiteNoti";

        [DisplayName("Cá nhân")]
        [Color(BgColor = "#fff200")]
        public static string App { get; set; } = "App";

        [DisplayName("Cá nhân")]
        [Color(BgColor = "#7efff5")]
        public static string AppNoti { get; set; } = "AppNoti";
    }

    public class ImportantAlertTypeConstant
    {
        [DisplayName("Tin tốt")]
        [Color(BgColor = "#7bed9f")]
        public static string Good { get; set; } = "Good";

        [DisplayName("Tin xấu")]
        [Color(BgColor = "#fed330")]
        public static string Bad { get; set; } = "Bad";
    }
}