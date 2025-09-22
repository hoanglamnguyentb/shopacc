using Hinet.Service.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Areas.DepartmentArea.Models
{
    public class EditVM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [CheckModify]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [MinLength(3, ErrorMessage = "Tối thiểu 3 ký tự")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [CheckModify]
        public string Code { get; set; }

        public List<string> Province { get; set; }

        [CheckModify]
        public long? ParentId { get; set; }

        [CheckModify]
        public string Loai { get; set; }

        public long Type { get; set; }

        public int Level { get; set; }

        public int? DefaultRole { get; set; }
        public bool IsAllProvine { get; set; }
        public bool? IsHigh { get; set; }
        public string Mota { get; set; }
    }
}