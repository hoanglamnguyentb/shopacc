using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.DepositService.Dto;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Service.DepositService
{
    public interface IDepositService:IEntityService<Deposit>
    {
        PageListResultBO<DepositDto> GetDaTaByPage(DepositSearchDto searchModel, int pageIndex = 1, int pageSize = 20);
        Deposit GetById(long id);
        List<DepositDto> GetAll(long? id = null);
    }
}
