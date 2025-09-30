using System;
using System.Collections.Generic;

namespace Hinet.API2.Models.DuLieu
{
    public class NhapDuLieuVM
    {
        public string KyHieuCongTrinh { get; set; }
        public string MaTram { get; set; }
        public DateTime? ThoiGianDo { get; set; }
        public List<DuLieu> DuLieus { get; set; }
        public string TrangThaiDo { get; set; }
    }

    public class DuLieu
    {
        public string MaThongSo { get; set; }
        public double? GiaTri { get; set; }
        public string DonVi { get; set; }
        public string TrangThaiDo { get; set; }
        public DateTime? ThoiGianDo { get; set; }
    }
}