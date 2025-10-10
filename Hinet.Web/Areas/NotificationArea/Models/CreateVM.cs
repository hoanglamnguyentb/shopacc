using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.NotificationArea.Models
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public bool IsRead { get; set; }

        public long? FromUser { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public long? ToUser { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string Message { get; set; }

        public string Link { get; set; }
        public string Type { get; set; }
    }
}