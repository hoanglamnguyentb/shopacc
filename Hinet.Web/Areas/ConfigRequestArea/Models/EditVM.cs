using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.ConfigRequestArea.Models
{
    public class EditVM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Hãy chọn bảng")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hãy chọn trường thông tin cần hiển thị")]
        public string AccessInfor { get; set; }

        public string Add { get; set; }
        public string Remove { get; set; }

        [Required(ErrorMessage = "Hãy chọn quyền")]
        public string Role { get; set; }

        public string Code
        {
            get
            {
                if (Role == null || Role == "")
                {
                    return Name;
                }
                return Name + "-" + Role;
            }
        }
    }
}