using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Service.DM_DulieuDanhmucService.Dto
{
    public class DM_QuanTri_DuLieuDanhMucImport
    {
        [Required]
        [DisplayName("Nhóm quản trị")]
        public string GroupCode { get; set; }

        [Required]
        [DisplayName("Tên dữ liệu")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Mã tên dữ liệu")]
        public string Code { get; set; }

        [DisplayName("Số thứ tự")]
        public int? Priority { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}