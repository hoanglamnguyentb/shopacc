using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.ModuleService.DTO;
using Hinet.Service.OperationService.DTO;
using System.Collections.Generic;

namespace Hinet.Service.OperationService
{
	public interface IOperationService : IEntityService<Operation>
	{
		Operation getByCode(string code);

		PageListResultBO<OperationDTO> GetDataByPage(OperationSearchDTO searchParams, int pageIndex = 1, int pageSize = 20);

		List<Operation> GetDanhSachOperationOfModule(long idModule);

		List<ModuleMenuDTO> GetListOperationOfUser(long userId);

		bool CheckCode(string code, long? id = null);
	}
}