using Hinet.Model.Entities;
using System.Collections.Generic;

namespace Hinet.Service.UserRoleService
{
    public interface IUserRoleService : IEntityService<UserRole>
    {
        List<UserRole> GetRoleOfUser(long userId);

        bool SaveRole(List<int> listRoleId, long UserId);

        List<long> GetListUserIdByRole(string role);
    }
}