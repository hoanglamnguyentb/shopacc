using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;

namespace Hinet.Model.Seed
{
    public class InitAccountSeed
    {
        public static void init(Hinet.Model.DbContext context)
        {
            var AccountAdminName = "admin";
            var AccountAdminPassword = "12345678";
            var userAdmin = context.Users.Where(x => x.UserName == AccountAdminName).FirstOrDefault();
            if (userAdmin == null)
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<AppUser, AppRole, long, AppLogin, AppUserRole, AppClaim>(context);
                var userManager = new UserManager<AppUser, long>(userStore);
                var user = new AppUser { UserName = AccountAdminName, TypeAccount = "BussinessUser" };
                userManager.Create(user, AccountAdminPassword);
            }

            var operations = context.Operation.ToList();
            foreach (var item in operations)
            {
                var objUserOperation = context.UserOperation.Where(x => x.UserId == userAdmin.Id && x.OperationId == item.Id).FirstOrDefault();
                if (objUserOperation == null)
                {
                    objUserOperation = new UserOperation()
                    {
                        IsAccess = 1,
                        OperationId = item.Id,
                        UserId = userAdmin.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    context.UserOperation.Add(objUserOperation);
                }
            }
            context.SaveChanges();
        }
    }
}