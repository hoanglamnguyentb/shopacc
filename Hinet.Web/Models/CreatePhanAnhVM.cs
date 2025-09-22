using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Models
{
    public class CreatePhanAnhVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Loại phản ánh")]
        public string LoaiPhanAnh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Nội dung phản ánh")]
        public string NoiDungPhanAnh { get; set; }

        [DisplayName("Nội dung trả lời")]
        public string NoiDungTraLoi { get; set; }

        [DisplayName("Tài liệu đính kèm")]
        public HttpPostedFileBase TaiLieuDinhKemInpFile { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Huyện")]
        public string Huyen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        [DisplayName("Xã")]
        public string Xa { get; set; }

        public List<SelectListItem> lstHuyen { get; set; }
        public List<SelectListItem> lstXa { get; set; }
        public List<SelectListItem> lstLoaiPhanAnh { get; set; }
    }
}