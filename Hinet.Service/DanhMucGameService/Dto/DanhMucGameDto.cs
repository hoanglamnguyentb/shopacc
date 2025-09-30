using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DanhMucGameService.Dto
{
    public class DanhMucGameDto : DanhMucGame
    {
        public int SoLuongTaiKhoan { get; set; }
    }
}