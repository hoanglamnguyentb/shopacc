using Hinet.Service.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ServiceStack.LicenseUtils;

namespace Hinet.Web.Models
{
    public class GiaoDichVM
    {
        public int SoTien { get; set; }
        public string NoiDungChuyenKhoan { get; set; }
        public string QrUrl { get; set; }
        public string NguoiThuHuong { get; set; }
        public string SoTaiKhoanNguoiThuHuong { get; set; }
    }

}