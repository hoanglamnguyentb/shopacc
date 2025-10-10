using Hinet.Service.QLSoLieuKhaiThacDuLieuService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Models
{
    public class BaoCaoThongKeVM
    {
        public SoLieuKhaiThacThang SoLieuKhaiThacThangNay { get; set; }
        public SoLieuKhaiThacThang SoLieuKhaiThacThangTruoc { get; set; }

        public BaoCaoThongKeVM()
        {
            SoLieuKhaiThacThangNay = new SoLieuKhaiThacThang();
            SoLieuKhaiThacThangTruoc = new SoLieuKhaiThacThang();
        }
    }

    public class SoLieuKhaiThacThang
    {
        public DateTime ThoiGian { get; set; }
        public List<QLSoLieuKhaiThacDuLieuDto> listSoLieuKhaiThac { get; set; }

    }
}