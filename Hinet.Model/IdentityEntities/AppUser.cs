using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hinet.Model.IdentityEntities
{
	public class AppUser : IdentityUser<long, AppLogin, AppUserRole, AppClaim>
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, long> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public override long Id { get; set; }
		public override string UserName { get; set; }
		public override string Email { get; set; }
		public override string PhoneNumber { get; set; }
		public DateTime? BirthDay { get; set; }
		public int Gender { get; set; }
		public string Address { get; set; }

		[StringLength(250)]
		public string FullName { get; set; }

		public string Avatar { get; set; }

		public string TypeAccount { get; set; }

		public DateTime? CreatedDate { get; set; }

		[MaxLength(256)]
		public string CreatedBy { get; set; }

		public long? CreatedID { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public string UpdatedBy { get; set; }

		public long? UpdatedID { get; set; }

		public bool? IsDelete { get; set; }

		public DateTime? DeleteTime { get; set; }

		public long? DeleteId { get; set; }

		public string Detail { get; set; }
		public DateTime? LastLogin { get; set; }
		public bool? Block { get; set; }

		public bool IsSendMail { get; set; }
		public bool ErrorMessage { get; set; }
		public string Token { get; set; }
	}

	public class AppUserRole : IdentityUserRole<long>
	{
	}

	public class AppRole : IdentityRole<long, AppUserRole>
	{
	}

	public class AppClaim : IdentityUserClaim<long>
	{
	}

	public class AppLogin : IdentityUserLogin<long>
	{
	}
}