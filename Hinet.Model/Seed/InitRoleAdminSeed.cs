using System.Linq;

namespace Hinet.Model.Seed
{
    public class InitRoleAdminSeed
    {
        public static void Init(DbContext context)
        {
            var AccountAdminName = "admin";
            var admin = context.Users.Where(x => x.UserName == AccountAdminName).FirstOrDefault();
            if (admin != null)
            {
                var listOperation = context.Operation.ToList();
            }
        }
    }
}