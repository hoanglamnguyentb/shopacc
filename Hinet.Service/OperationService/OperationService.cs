using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.ModuleRepository;
using Hinet.Repository.OperationRepository;
using Hinet.Repository.RoleOperationRepository;
using Hinet.Repository.RoleRepository;
using Hinet.Repository.UserOperationRepository;
using Hinet.Repository.UserRoleRepository;
using Hinet.Service.Common;
using Hinet.Service.ModuleService.DTO;
using Hinet.Service.OperationService.DTO;
using log4net;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Hinet.Service.OperationService
{
	public class OperationService : EntityService<Operation>, IOperationService
	{
		private IUnitOfWork _unitOfWork;
		private IOperationRepository _operationRepository;
		private IUserRoleRepository _userRoleRepository;
		private IRoleRepository _roleRepository;
		private IRoleOperationRepository _roleOperationRepository;
		private IUserOperationRepository _userOperationRepository;
		private ILog _loger;
		private IModuleRepository _moduleRepository;

		public OperationService(IUnitOfWork unitOfWork, IOperationRepository operationRepository, ILog loger,
			IUserRoleRepository userRoleRepository,
			IRoleRepository roleRepository,
			IRoleOperationRepository roleOperationRepository,
			IUserOperationRepository userOperationRepository,
			IModuleRepository moduleRepository
			) :
			base(unitOfWork, operationRepository)
		{
			_userOperationRepository = userOperationRepository;
			_roleOperationRepository = roleOperationRepository;
			_roleRepository = roleRepository;
			_moduleRepository = moduleRepository;
			_userRoleRepository = userRoleRepository;
			_unitOfWork = unitOfWork;
			_operationRepository = operationRepository;
			_loger = loger;
		}

		public Operation getByCode(string code)
		{
			return _operationRepository.GetAllAsQueryable().Where(x => x.Code == code).FirstOrDefault();
		}

		public PageListResultBO<OperationDTO> GetDataByPage(OperationSearchDTO searchParams, int pageIndex = 1, int pageSize = 20)
		{
			var queryResult = (from operation in this._operationRepository.GetAllAsQueryable()
							   select new OperationDTO()
							   {
								   Id = operation.Id,
								   Name = operation.Name,
								   Code = operation.Code,
								   URL = operation.URL,
								   IsShow = operation.IsShow,
								   ModuleId = operation.ModuleId,
								   Order = operation.Order,
								   Icon = operation.Icon,
							   });
			if (searchParams != null)
			{
				queryResult = queryResult.Where(x => x.ModuleId == searchParams.QueryModuleId);
				if (!string.IsNullOrEmpty(searchParams.QueryName))
				{
					searchParams.QueryName = searchParams.QueryName.Trim().ToLower();
					queryResult = queryResult.Where(x => x.Name.Trim().ToLower().Contains(searchParams.QueryName));
				}
				if (searchParams.QueryIsShow != null)
				{
					queryResult = queryResult.Where(x => x.IsShow == searchParams.QueryIsShow.Value);
				}

				if (!string.IsNullOrEmpty(searchParams.sortQuery))
				{
					queryResult = queryResult.OrderBy(searchParams.sortQuery);
				}
				else
				{
					queryResult = queryResult.OrderBy(x => x.Order)
						.ThenBy(x => x.Name);
				}
			}
			else
			{
				queryResult = queryResult.OrderBy(x => x.Order)
						.ThenBy(x => x.Name);
			}

			var result = new PageListResultBO<OperationDTO>();
			if (pageSize == -1)
			{
				var pagedList = queryResult.ToList();
				result.Count = pagedList.Count;
				result.TotalPage = 1;
				result.ListItem = pagedList;
			}
			else
			{
				var dataPageList = queryResult.ToPagedList(pageIndex, pageSize);
				result.Count = dataPageList.TotalItemCount;
				result.TotalPage = dataPageList.PageCount;
				result.ListItem = dataPageList.ToList();
			}
			return result;
		}

		public List<Operation> GetDanhSachOperationOfModule(long idModule)
		{
			var lstChucNang = _operationRepository.GetAllAsQueryable().Where(x => x.ModuleId == idModule).ToList();
			return lstChucNang;
		}

		/// <summary>
		/// Lấy danh sách thao tác theo User
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>

		public List<ModuleMenuDTO> GetListOperationOfUser(long userId)
		{
			var listOperation = new List<ModuleMenuDTO>();
			var listRoleIdOfUser = (from userRole in _userRoleRepository.GetAllAsQueryable().Where(x => x.UserId == userId)
									join role in _roleRepository.GetAllAsQueryable()
									on userRole.RoleId equals role.Id
									select role).Select(x => x.Id).ToList();

			var listOperationId = _roleOperationRepository.GetAllAsQueryable().Where(x => x.IsAccess == 1 && listRoleIdOfUser.Contains(x.RoleId)).Select(x => x.OperationId);
			listOperationId = listOperationId.Concat(_userOperationRepository.GetAllAsQueryable().Where(x => x.IsAccess == 1 && x.UserId == userId).Select(x => x.OperationId));

			listOperation = (from operationId in listOperationId
							 join operation in _operationRepository.GetAllAsQueryable() on operationId equals operation.Id
							 group operation by operation.ModuleId into groupMenu
							 join module in _moduleRepository.GetAllAsQueryable() on groupMenu.Key equals module.Id
							 select new ModuleMenuDTO()
							 {
								 Id = groupMenu.Key,
								 ClassCss = module.ClassCss,
								 Code = module.Code,
								 CreatedBy = module.CreatedBy,
								 Icon = module.Icon,
								 CreatedDate = module.CreatedDate,
								 IsShow = module.IsShow,
								 Link = module.Link,
								 Name = module.Name,
								 Order = module.Order,
								 StyleCss = module.StyleCss,
								 UpdatedBy = module.UpdatedBy,
								 UpdatedDate = module.UpdatedDate,
								 ListOperation = groupMenu.OrderBy(x => x.Order).ThenBy(x => x.Id).ToList()
							 }).OrderBy(x => x.Order)
							   .ToList();

			if (listOperation != null)
			{
				listOperation = listOperation.Distinct().ToList();
				foreach (var item in listOperation)
				{
					if (item.ListOperation != null)
					{
						item.ListOperation = item.ListOperation.Distinct().ToList();
					}
				}
			}
			return listOperation;
		}

		public bool CheckCode(string code, long? id = null) => _operationRepository.GetAllAsQueryable().Any(x => x.Code != null && x.Code == code && (id.HasValue ? x.Id != id : true));
	}
}