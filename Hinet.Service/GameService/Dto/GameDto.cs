using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.Common;
using Hinet.Service.Constant;

namespace Hinet.Service.GameService.Dto
{
    public class GameDto : Game
    {
        public List<DanhMucGameDto> ListDanhMucGame { get; set; }
        public string ViTriHienThiTxt { get {
                return ConstantExtension.GetName<ViTriHienThiGameConstant>(ViTriHienThi);
            } }
    }
}