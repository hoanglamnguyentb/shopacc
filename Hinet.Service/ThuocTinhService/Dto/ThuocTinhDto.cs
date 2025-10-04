using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.ThuocTinhService.Dto
{
    public class ThuocTinhDto : ThuocTinh
    {
        public List<DM_DulieuDanhmuc> ListDuLieuDanhMuc { get; set; }
    }
}