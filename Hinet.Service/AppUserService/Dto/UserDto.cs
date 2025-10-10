using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Service.ModuleService.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Service.AppUserService.Dto
{
	public class UserDto : AppUser
	{
		public string DepartmentName { get; set; }
		public List<ModuleMenuDTO> ListActions { get; set; }
		public List<Operation> ListOperations { get; set; }
		public List<Role> ListRoles { get; set; }
		public bool IsLock { get; set; }
		public string RoleTxt { get; set; }
	}

	public class AppUser_Api
	{
		public long Id { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime? BirthDay { get; set; }
		public int Gender { get; set; }
		public string Address { get; set; }

		[StringLength(250)]
		public string FullName { get; set; }
	}

	public class AppUserVM_Api
	{
		[Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập email")]
		[EmailAddress(ErrorMessage = "Sai định dạng email")]
		public string Email { get; set; }

		public string PhoneNumber { get; set; }
		public DateTime? BirthDay { get; set; }
		public int Gender { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		public string Password { get; set; }

		public string Address { get; set; }

		[StringLength(250)]
		[Required(ErrorMessage = "Vui lòng nhập đầy đủ họ và tên")]
		public string FullName { get; set; }

		public long Id { get; set; }
	}

	public class Register
	{
		[Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập email")]
		[EmailAddress(ErrorMessage = "Sai định dạng email")]
		public string Email { get; set; }

		public string PhoneNumber { get; set; }
		public DateTime? BirthDay { get; set; }
		public int Gender { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		[Compare("Password", ErrorMessage = "Vui lòng nhập mật khẩu giống nhau")]
		public string RePassword { get; set; }

		public string Address { get; set; }

		[StringLength(250)]
		[Required(ErrorMessage = "Vui lòng nhập đầy đủ họ và tên")]
		public string FullName { get; set; }
	}
}