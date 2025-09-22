using AutoMapper;
using CommonHelper.String;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Repository;
using Hinet.Repository.AppUserRepository;
using Hinet.Repository.DanhmucRepository;
using Hinet.Repository.RoleOperationRepository;
using Hinet.Repository.RoleRepository;
using Hinet.Repository.UserOperationRepository;
using Hinet.Repository.UserRoleRepository;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.OperationService;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace Hinet.Service.AppUserService
{
	public class AppUserService : EntityService<AppUser>, IAppUserService
	{
		private IUnitOfWork _unitOfWork;
		private IAppUserRepository _appUserRepository;
		private IRoleRepository _roleRepository;
		private IOperationService _operationService;
		private IUserRoleRepository _userRoleRepository;
		private IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private IDM_DulieuDanhmucRepository _dM_DulieuDanhmucRepository;
		private ILog _loger;
		private IMapper _mapper;
		private IUserRoleRepository repoUserRole;
		private IRoleOperationRepository _roleOperationRepository;
		private IUserOperationRepository _userOperationRepository;

		public AppUserService(IUnitOfWork unitOfWork, IAppUserRepository appUserRepository, ILog loger,
			IRoleOperationRepository roleOperationRepository,
			IUserRoleRepository userRoleRepository,
			IDM_DulieuDanhmucRepository dM_DulieuDanhmucRepository,
			IMapper mapper,
			IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			IRoleRepository roleRepository,
			IOperationService operationService,
			IUserOperationRepository userOperationRepository,

		IUserRoleRepository repoUserRole)
			: base(unitOfWork, appUserRepository)
		{
			_userOperationRepository = userOperationRepository;
			_roleOperationRepository = roleOperationRepository;
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_userRoleRepository = userRoleRepository;
			_roleRepository = roleRepository;
			_unitOfWork = unitOfWork;
			_appUserRepository = appUserRepository;
			_loger = loger;
			_mapper = mapper;
			_operationService = operationService;
			_dM_DulieuDanhmucRepository = dM_DulieuDanhmucRepository;
			this.repoUserRole = repoUserRole;
		}

		public AppUser GetWithToken(string token)
		{
			var data = _appUserRepository.GetAllAsQueryable().Where(x => x.Token == token).FirstOrDefault();
			return data;
		}

		public void updateToken()
		{
			var lstdata = _appUserRepository.GetAllAsQueryable().ToList();
			foreach (var item in lstdata)
			{
				if (string.IsNullOrEmpty(item.Token))
				{
					item.Token = item.UserName.md5EndCode();
					Update(item);
				}
			}
		}

		/// <summary>
		/// Lấy tất cả người dùng theo vai trò và Khoa phòng
		/// nếu Khoa phòng null thì lấy theo toàn bộ hệ thống
		/// </summary>
		/// <param name="VaiTro"></param>
		/// <param name="DepartmentId"></param>
		/// <returns></returns>
		///
		public List<AppUser> GetUsersByRoleAndDepartment(string VaiTro, long? DepartmentId)
		{
			var getRole = _roleRepository.GetAllAsQueryable().Where(x => x.Code == VaiTro).FirstOrDefault();
			if (getRole == null)
			{
				return null;
			}
			var userHasRole = from userrole in _userRoleRepository.GetAllAsQueryable().Where(x => x.RoleId == getRole.Id)
							  join user in _appUserRepository.GetAllAsQueryable() on userrole.UserId equals user.Id
							  select user;
			return userHasRole.ToList();
		}

		public List<SelectListItem> DropListUsersByRoleAndDepartment(string VaiTro, long? DepartmentId)
		{
			var getRole = _roleRepository.GetAllAsQueryable().Where(x => x.Code == VaiTro).FirstOrDefault();
			if (getRole == null)
			{
				return null;
			}
			var userHasRole = from userrole in _userRoleRepository.GetAllAsQueryable().Where(x => x.RoleId == getRole.Id)
							  join user in _appUserRepository.GetAllAsQueryable() on userrole.UserId equals user.Id
							  select user;

			var listUser = new List<SelectListItem>();
			foreach (var item in userHasRole)
			{
				listUser.Add(new SelectListItem()
				{
					Text = item.FullName.ToString(),
					Value = item.Id.ToString(),
				});
			}

			return listUser;
		}

		public List<AppUser> GetUsersByPermissionAndDepartment(string PermissionCode, long? DepartmentId)
		{
			var operation = _operationService.getByCode(PermissionCode);
			if (operation == null)
			{
				return null;
			}

			var userOperation = from userPer in _userOperationRepository.GetAllAsQueryable().Where(x => x.OperationId == operation.Id)
								join user in _appUserRepository.GetAllAsQueryable() on userPer.UserId equals user.Id
								select user;

			var listRoleHasPermission = _roleOperationRepository.GetAllAsQueryable().Where(x => x.OperationId == operation.Id).Select(x => x.RoleId).ToList();
			if (listRoleHasPermission != null && listRoleHasPermission.Any())
			{
				var userHasRole = from userrole in _userRoleRepository.GetAllAsQueryable().Where(x => listRoleHasPermission.Contains(x.RoleId))
								  join user in _appUserRepository.GetAllAsQueryable() on userrole.UserId equals user.Id
								  select user;

				userOperation = userOperation.Concat(userHasRole);
			}

			userOperation = userOperation.Distinct();
			return userOperation.ToList();
		}

		public PageListResultBO<UserDto> GetDaTaByPage(AppUserSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
		{
			var userRoleDbSet = _userRoleRepository.DBSet();
			var RoleDbSet = _roleRepository.DBSet();
			//var query = from user in _appUserRepository.GetAllAsQueryable().Where(x => x.TypeAccount == AccountTypeConstant.BussinessUser)
			var query = from user in _appUserRepository.GetAllAsQueryable()
						select new UserDto
						{
							UserName = user.UserName,
							FullName = user.FullName,
							Id = user.Id,
							AccessFailedCount = user.AccessFailedCount,
							Address = user.Address,
							Avatar = user.Avatar,
							BirthDay = user.BirthDay,
							Email = user.Email,
							EmailConfirmed = user.EmailConfirmed,
							Gender = user.Gender,
							LockoutEnabled = user.LockoutEnabled,
							LockoutEndDateUtc = user.LockoutEndDateUtc,
							PhoneNumber = user.PhoneNumber,
							PhoneNumberConfirmed = user.PhoneNumberConfirmed,
							TypeAccount = user.TypeAccount,

							ListRoles = (from userRole in userRoleDbSet.Where(x => x.UserId == user.Id)
										 join roletbl in RoleDbSet on userRole.RoleId equals roletbl.Id
										 select roletbl).ToList(),
							//IsSendMail = user.IsSendMail,
							//ErrorMessage = user.ErrorMessage
						};

			if (searchModel != null)
			{
				if (!string.IsNullOrEmpty(searchModel.UserNameFilter))
				{
					query = query.Where(x => x.UserName.Contains(searchModel.UserNameFilter));
				}

				if (!string.IsNullOrEmpty(searchModel.FullNameFilter))
				{
					query = query.Where(x => x.FullName.Contains(searchModel.FullNameFilter));
				}
				if (!string.IsNullOrEmpty(searchModel.EmailFilter))
				{
					query = query.Where(x => x.Email.Contains(searchModel.EmailFilter));
				}
				if (!string.IsNullOrEmpty(searchModel.AddressFilter))
				{
					query = query.Where(x => x.Address.Contains(searchModel.AddressFilter));
				}

				if (!string.IsNullOrEmpty(searchModel.sortQuery))
				{
					query = query.OrderBy(searchModel.sortQuery);
				}
				else
				{
					query = query.OrderByDescending(x => x.Id);
				}
			}
			else
			{
				query = query.OrderByDescending(x => x.Id);
			}
			var resultmodel = new PageListResultBO<UserDto>();

			if (pageSize == -1)
			{
				var dataPageList = query.ToList();
				resultmodel.Count = dataPageList.Count;
				resultmodel.TotalPage = 1;
				resultmodel.ListItem = dataPageList;
			}
			else
			{
				var dataPageList = query.ToPagedList(pageIndex, pageSize);
				resultmodel.Count = dataPageList.TotalItemCount;
				resultmodel.TotalPage = dataPageList.PageCount;
				resultmodel.ListItem = dataPageList.ToList();
			}
			//foreach (var item in resultmodel.ListItem)
			//{
			//    item.ListRoles= (from userRole in _userRoleRepository.GetAllAsQueryable().Where(x => x.UserId == item.Id)
			//           join roletbl in _roleRepository.GetAllAsQueryable() on userRole.RoleId equals roletbl.Id
			//           select roletbl).ToList();
			//}
			var CurrentDateTime = DateTime.UtcNow;
			foreach (var item in resultmodel.ListItem)
			{
				if (item.LockoutEndDateUtc != null && item.LockoutEndDateUtc > CurrentDateTime)
				{
					item.IsLock = true;
				}
				else
				{
					item.IsLock = false;
				}
			}

			return resultmodel;
		}

		public AppUser GetById(long id)
		{
			return _appUserRepository.GetById(id);
		}

		/// <summary>
		/// Lấy thông tin dto của user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public UserDto GetDtoById(long id)
		{
			var userRoleDbSet = _userRoleRepository.DBSet();
			var RoleDbSet = _roleRepository.DBSet();
			var query = (from user in _appUserRepository.GetAllAsQueryable().Where(x => x.Id == id)
						 select new UserDto
						 {
							 UserName = user.UserName,
							 FullName = user.FullName,
							 Id = user.Id,
							 AccessFailedCount = user.AccessFailedCount,
							 Address = user.Address,
							 Avatar = user.Avatar,
							 BirthDay = user.BirthDay,
							 Email = user.Email,
							 EmailConfirmed = user.EmailConfirmed,
							 Gender = user.Gender,
							 LockoutEnabled = user.LockoutEnabled,
							 LockoutEndDateUtc = user.LockoutEndDateUtc,
							 PhoneNumber = user.PhoneNumber,
							 PhoneNumberConfirmed = user.PhoneNumberConfirmed,
							 TypeAccount = user.TypeAccount,
							 ListRoles = (from userRole in userRoleDbSet.Where(x => x.UserId == user.Id)
										  join roletbl in RoleDbSet on userRole.RoleId equals roletbl.Id
										  select roletbl).ToList(),
						 }).FirstOrDefault();
			query.ListActions = _operationService.GetListOperationOfUser(query.Id);
			query.ListOperations = new List<Operation>();

			if (query.ListActions != null)
			{
				foreach (var item in query.ListActions)
				{
					if (item.ListOperation != null && item.ListOperation.Any())
					{
						query.ListOperations.AddRange(item.ListOperation);
					}
				}
			}

			var groupRoleCodes = query.ListRoles.Select(x => x.Code);

			return query;
		}

		public UserDto GetDtoByUserName(string id)
		{
			var userRoleDbSet = _userRoleRepository.DBSet();
			var RoleDbSet = _roleRepository.DBSet();
			var query = (from user in _appUserRepository.GetAllAsQueryable().Where(x => x.UserName == id)

						 select new UserDto
						 {
							 UserName = user.UserName,
							 FullName = user.FullName,
							 Id = user.Id,
							 AccessFailedCount = user.AccessFailedCount,
							 Address = user.Address,
							 Avatar = user.Avatar,
							 BirthDay = user.BirthDay,
							 Email = user.Email,
							 EmailConfirmed = user.EmailConfirmed,
							 Gender = user.Gender,
							 LockoutEnabled = user.LockoutEnabled,
							 LockoutEndDateUtc = user.LockoutEndDateUtc,
							 PhoneNumber = user.PhoneNumber,
							 PhoneNumberConfirmed = user.PhoneNumberConfirmed,
							 TypeAccount = user.TypeAccount,

							 ListRoles = (from userRole in userRoleDbSet.Where(x => x.UserId == user.Id)
										  join roletbl in RoleDbSet on userRole.RoleId equals roletbl.Id
										  select roletbl).ToList(),
						 }).FirstOrDefault();
			query.ListActions = _operationService.GetListOperationOfUser(query.Id);
			query.ListOperations = new List<Operation>();
			if (query.ListActions != null)
			{
				foreach (var item in query.ListActions)
				{
					if (item.ListOperation != null && item.ListOperation.Any())
					{
						query.ListOperations.AddRange(item.ListOperation);
					}
				}
			}

			var groupRoleCodes = query.ListRoles.Select(x => x.Code);

			return query;
		}

		/// <summary>
		/// Kiểm tra sự tồn tại của Tài khoản trên hệ thống
		/// </summary>
		/// <returns>
		/// true: Tồn tại
		///
		/// </returns>
		public bool CheckExistUserName(string userName, long? id = null)
		{
			return _appUserRepository.GetAllAsQueryable().Where(x => x.UserName != null && x.UserName.Equals(userName) && (id.HasValue ? x.Id != id : true)).Any();
		}

		public AppUser GetUserByUsername(string mst)
		{
			return _appUserRepository.GetAllAsQueryable().Where(x => x.UserName != null && x.UserName.Equals(mst)).FirstOrDefault();
		}

		/// <summary>
		/// Kiểm tra sự tồn tại của Email trên hệ thống
		/// </summary>
		/// <returns>
		/// true: Tồn tại
		///
		/// </returns>
		public bool CheckExistEmail(string email, long? id = null)
		{
			return _appUserRepository.GetAll().Where(x => x.Email != null && x.Email.ToLower().Equals(email.ToLower()) && (id.HasValue ? x.Id != id : true)).Any();
		}

		public AppUser CheckToken(string token)
		{
			return _appUserRepository.GetAllAsQueryable().Where(x => x.Token == token).FirstOrDefault();
		}

		/// <summary>
		/// author:duynn
		/// </summary>
		/// <param name="roleIds"></param>
		/// <returns></returns>
		public List<UserDto> GetListUserByRoles(List<int> roleIds)
		{
			var result = (from user in _appUserRepository.GetAllAsQueryable()
						  join userRole in repoUserRole.GetAllAsQueryable()
						  on user.Id equals userRole.UserId
						  into groupUserRole
						  from gUserRole in groupUserRole
						  where roleIds.Contains(gUserRole.RoleId)
						  group user by user.Id
						  into groupNewUserRole
						  select new UserDto()
						  {
							  Id = groupNewUserRole.Key,
							  FullName = groupNewUserRole.Select(x => x.FullName).FirstOrDefault(),
							  Email = groupNewUserRole.Select(x => x.Email).FirstOrDefault()
						  }).ToList();
			return result;
		}

		public bool CheckUserHasRoleByCurrentUserId(string VaiTro, long? CurrentUserId)
		{
			var getRole = _roleRepository.GetAllAsQueryable().Where(x => x.Code == VaiTro).FirstOrDefault();
			if (getRole == null)
			{
				return false;
			}
			var userDto = GetDtoById(CurrentUserId.GetValueOrDefault());
			if (userDto.ListRoles != null && userDto.ListRoles.Any(x => x.Code == VaiTro))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<UserRole> GetListRoleByUserId(long userId)
		{
			var listItem = _userRoleRepository.GetAll().Where(x => x.UserId == userId).ToList();
			return listItem;
		}

		public List<Role> GetRoleByCode(long id)
		{
			var item = _roleRepository.GetAll().Where(x => x.Id == id).ToList();
			return item;
		}

		public AppUserVM_Api GetAppUserVM_Api(long Id)
		{
			return _appUserRepository.GetAllAsQueryable().Where(x => x.Id == Id).Select(x => new AppUserVM_Api()
			{
				Address = x.Address,
				BirthDay = x.BirthDay,
				Email = x.Email,
				FullName = x.FullName,
				Gender = x.Gender,
				PhoneNumber = x.PhoneNumber,
				UserName = x.UserName,
				Id = x.Id
			}).FirstOrDefault();
		}

		public bool CheckFullName(string fullname)
		{
			var query = _appUserRepository.GetAllAsQueryable().Any(x => x.FullName == fullname);
			return query;
		}

		public List<UserDto> GetQuickLoginUser()
		{
			var query = from Role in _roleRepository.GetAllAsQueryable()

						join UserRole in _userRoleRepository.GetAllAsQueryable()
						on Role.Id equals UserRole.RoleId into UserRole
						from UserRoletlb in UserRole.DefaultIfEmpty()

						join User in _appUserRepository.GetAllAsQueryable()
						on UserRoletlb.UserId equals User.Id into User
						from Usertbl in User.DefaultIfEmpty()
						where Usertbl != null

						//group Usertbl by Role into GroupData
						select new UserDto
						{
							UserName = Usertbl.UserName,
							RoleTxt = Role.Name,
							FullName = Usertbl.FullName,
						};
			return query.ToList();
		}
	}
}