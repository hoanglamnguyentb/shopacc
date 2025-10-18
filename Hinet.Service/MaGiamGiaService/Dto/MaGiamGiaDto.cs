using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.MaGiamGiaService.Dto
{
    public class MaGiamGiaDto : MaGiamGia
    {
        public List<GianHang> ListGianHang { get; set; }
    }
}