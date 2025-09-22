using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class TypeNotiToaSoanConstant
    {
        [DisplayName("Tất cả")]
        public static string All { get; set; } = "All";

        [DisplayName("Khoa phòng")]
        public static string Group { get; set; } = "Group";

        [DisplayName("Nhóm người dùng")]
        public static string CustomGroup { get; set; } = "CustomGroup";

        /// <summary>
        /// Người dùng
        /// </summary>
        [DisplayName("Người dùng")]
        public static string Users { get; set; } = "Users";
    }
}