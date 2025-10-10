using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Service.DM_NhomDanhmucService.Dto
{
    public class DM_NhomDanhmucImportDto
    {
        [Required]
        [DisplayName("Tên danh mục")]
        public string GroupName { get; set; }

        [Required]
        [DisplayName("Mã danh mục")]
        public string GroupCode { get; set; }
    }
}