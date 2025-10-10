using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Models
{
    public class GiaoDichTopupVM
    {
        public long DoiTuongId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số tiền")]
        [Range(1000, long.MaxValue, ErrorMessage = "Số tiền phải lớn hơn hoặc bằng 1.000")]
        [Display(Name = "Số tiền nạp")]
        public int SoTien { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [StringLength(250, ErrorMessage = "Nội dung không được vượt quá 250 ký tự")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản cần nạp")]
        [StringLength(100, ErrorMessage = "Tên tài khoản không được vượt quá 100 ký tự")]
        [Display(Name = "Tên tài khoản cần nạp")]
        public string TenTaiKhoanCanNap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu tài khoản")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu tài khoản nạp")]
        public string MatKhauTaiKhoanNap { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("MatKhauTaiKhoanNap", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string XacNhanMatKhauTaiKhoanNap { get; set; }

    }

}