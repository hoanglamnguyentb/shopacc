using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ThuocTinhGianHangService.Dto
{
    public class ThuocTinhGianHangDto : ThuocTinhGianHang
    {
        public List<DM_DulieuDanhmuc> ListDuLieuDanhMuc { get; set; }

    }
}