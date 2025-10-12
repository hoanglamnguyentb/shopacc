using Hinet.Service.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ServiceStack.LicenseUtils;

namespace Hinet.Web.Models
{
    public class GiaoDichNapVM
    {
        public long? DoiTuongId { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string LoaiGiaoDich { get; set; } = LoaiGiaoDichConstant.NAPTHUONG;
        public int SoTien { get; set; }
        public string NoiDung { get; set; }
    }
}