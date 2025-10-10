using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Service.AppUserService
{
	public interface IAppUserService : IEntityService<AppUser>
	{
		PageListResultBO<UserDto> GetDaTaByPage(AppUserSearchDto searchModel, int pageIndex = 1, int pageSize = 20);

		AppUser GetById(long id);

		bool CheckExistUserName(string userName, long? id = null);

		bool CheckExistEmail(string email, long? id = null);

		UserDto GetDtoById(long id);

		AppUser CheckToken(string token);

		AppUser GetUserByUsername(string mst);

		UserDto GetDtoByUserName(string id);

		List<UserDto> GetListUserByRoles(List<int> roleIds);

		bool CheckUserHasRoleByCurrentUserId(string VaiTro, long? CurrentUserId);

		AppUser GetWithToken(string token);

		void updateToken();

		List<UserRole> GetListRoleByUserId(long userId);

		List<Role> GetRoleByCode(long id);

		AppUserVM_Api GetAppUserVM_Api(long Id);

		bool CheckFullName(string fullname);

		List<UserDto> GetQuickLoginUser();
	}
}