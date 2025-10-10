using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.DepartmentArea.Models
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Code { get; set; }

        public string Loai { get; set; }
        public string Mota { get; set; }

        public long? ParentId { get; set; }
        public List<string> Province { get; set; }

        public long Type { get; set; }

        public int Level { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public int? DefaultRole { get; set; }

        public bool IsAllProvine { get; set; }
    }
}