using AutoMapper;
using CommonHelper.String;
using Hinet.Model.IdentityEntities;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.OperationService;
using Hinet.Web.Core;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Web.Common;

namespace Hinet.Web.Controllers
{
	[Authorize]
	public class AccountAdminController : BaseController
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		//private IDatabase _cacheDatabase;
		private IOperationService _operationService;

		private IAppUserService _appUserService;
		private IDM_DulieuDanhmucService _iDM_DuLieuDanhMucService;
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;

		public AccountAdminController(
			IOperationService operationService,
			IDM_DulieuDanhmucService iDM_DuLieuDanhMucService,
			 ILog Ilog,
			 IMapper mapper,

		IAppUserService appUserService)
		//:this(new UserManager<AppUser,long>(new UserStore<AppUser,AppRole,long,AppLogin,AppUserRole,AppClaim>(new QLNSContext())))
		{
			_mapper = mapper;
			_operationService = operationService;
			//_cacheDatabase = cacheDatabase;
			_appUserService = appUserService;
			_iDM_DuLieuDanhMucService = iDM_DuLieuDanhMucService;
			_Ilog = Ilog;
		}

		public AccountAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[AllowAnonymous]
		public string UpdateToken()
		{
			_appUserService.updateToken();
			return "ok";
		}

		[AllowAnonymous]
		public ActionResult LoginSSO(string token)
		{
			if (!string.IsNullOrEmpty(token))
			{
				var userCheck = _appUserService.GetWithToken(token);
				if (userCheck != null)
				{
					var userDto = _appUserService.GetDtoById(userCheck.Id);

					SessionManager.SetValue(SessionManager.USER_INFO, userDto);
					var listOperation = _operationService.GetListOperationOfUser(userCheck.Id);

					//_cacheDatabase.StringSet("Operation:" + userId, JsonConvert.SerializeObject(listOperation));
					SessionManager.SetValue(SessionManager.LIST_PERMISSTION, listOperation);

					//Lưu thông tin người đăng nhập
					RepositoryConnectUser.SaveConnect(userCheck.Id);

					return RedirectToAction("Index", "Dashboard", new { area = "DashboardArea" });
				}
			}

			return new HttpNotFoundResult();
		}

		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = await UserManager.FindByNameAsync(model.UserName);
			if (user == null)
			{
				ModelState.AddModelError("", "Thông tin đăng nhập không tồn tại");
				return View(model);
			}
			var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, true, shouldLockout: false);

			switch (result)
			{
				case SignInStatus.Success:
					//return RedirectToLocal(returnUrl);
					var userId = SignInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId<long>();
					var userDto = _appUserService.GetDtoById(userId);
					SessionManager.SetValue(SessionManager.USER_INFO, userDto);
					var listOperation = _operationService.GetListOperationOfUser(userId);
					SessionManager.SetValue(SessionManager.LIST_PERMISSTION, listOperation);
					//Lưu thông tin người đăng nhập
					RepositoryConnectUser.SaveConnect(user.Id);

					return RedirectToAction("Index", "Dashboard", new { area = "DashboardArea" });

				case SignInStatus.LockedOut:
					ViewBag.TimeAutoUnlock = UserManager.GetLockoutEndDate(user.Id).AddHours(7);
					return View("Lockout");

				case SignInStatus.RequiresVerification:
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = true });

				case SignInStatus.Failure:
					if (user != null)
					{
						await UserManager.AccessFailedAsync(user.Id);
						var message = string.Empty;
						if (UserManager.GetAccessFailedCount(user.Id) < UserManager.MaxFailedAccessAttemptsBeforeLockout)
						{
							message = "Bạn đã nhập sai thông tin tài khoản " + UserManager.GetAccessFailedCount(user.Id) + "/" + UserManager.MaxFailedAccessAttemptsBeforeLockout;
						}
						else
						{
							message = "Tài khoản của bạn đã bị khóa. Sẽ tự động mở lúc " + UserManager.GetLockoutEndDate(user.Id).AddHours(7);
						}

						ModelState.AddModelError("", message);
						return View(model);
					}
					ModelState.AddModelError("", "Thông tin đăng nhập không đúng");
					return View(model);

				default:
					ModelState.AddModelError("", "Thông tin đăng nhập không đúng");
					return View(model);
			}
		}

		//
		// POST: /Account/LogOff
		[HttpGet]
		//[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public ActionResult LogOff()
		{
			var infoUser = SessionManager.GetValue(SessionManager.USER_INFO) as AppUser;
			if (infoUser != null)
			{
				RepositoryConnectUser.RemoveConnect(infoUser.Id);
			}
			SessionManager.SetValue(SessionManager.USER_INFO, null);
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			Session["IsFirstVisit"] = true;

			return RedirectToAction("Login", "AccountAdmin");
		}

		//
		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_userManager != null)
				{
					_userManager.Dispose();
					_userManager = null;
				}

				if (_signInManager != null)
				{
					_signInManager.Dispose();
					_signInManager = null;
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Thay đổi ngôn ngữ
		/// </summary>
		/// <param name="lang"></param>
		/// <returns></returns>
		///
		//[AllowAnonymous]
		//public ActionResult ChangeLang(string lang, string returnUrl)
		//{
		//    if (ConstantExtension.GetListData<LangConstant>().Contains(lang))
		//    {
		//        Response.Cookies["Language"].Value = lang;
		//        System.Threading.Thread.CurrentThread.CurrentCulture =
		//           new System.Globalization.CultureInfo(lang);
		//        System.Threading.Thread.CurrentThread.CurrentUICulture =
		//            new System.Globalization.CultureInfo(lang);
		//    }
		//    else
		//    {
		//        System.Threading.Thread.CurrentThread.CurrentCulture =
		//            new System.Globalization.CultureInfo("");
		//        System.Threading.Thread.CurrentThread.CurrentUICulture =
		//            new System.Globalization.CultureInfo("en");
		//    }
		//    if (string.IsNullOrEmpty(returnUrl))
		//    {
		//        return RedirectToAction("index", "home");
		//    }
		//    return Redirect(returnUrl);
		//}

		#region Helpers

		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}

		internal class ChallengeResult : HttpUnauthorizedResult
		{
			public ChallengeResult(string provider, string redirectUri)
				: this(provider, redirectUri, null)
			{
			}

			public ChallengeResult(string provider, string redirectUri, string userId)
			{
				LoginProvider = provider;
				RedirectUri = redirectUri;
				UserId = userId;
			}

			public string LoginProvider { get; set; }
			public string RedirectUri { get; set; }
			public string UserId { get; set; }

			public override void ExecuteResult(ControllerContext context)
			{
				var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
				if (UserId != null)
				{
					properties.Dictionary[XsrfKey] = UserId;
				}
				context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
			}
		}

		#endregion Helpers

		//
		// GET: /Manage/ChangePassword
		public ActionResult ChangePassword()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult ChangePass(string token)
		{
			var model = new SetPasswordByToken();
			model.token = token;
			return View(model);
		}

		//
		// POST: /Manage/ChangePassword
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
				if (user != null)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				}
				return View("ActionDone", new ResultRequestPageSuccessVM { Title = "Đổi mật khẩu", Message = "Thay đổi mật khẩu thành công" });
			}
			AddErrors(result);
			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePass(SetPasswordByToken model)
		{
			// check token password
			var getToken = _appUserService.CheckToken(model.token);

			if (getToken != null)
			{
				var tokenUpdatePass = UserManager.GeneratePasswordResetToken(getToken.Id);
				var result = UserManager.ResetPassword(getToken.Id, tokenUpdatePass, model.NewPassword);
				if (result.Succeeded)
				{
					var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
					if (user != null)
					{
						await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
					}
					return View("Login");
				}

				AddErrors(result);
			}
			else
			{
				return View(model);
			}
			return View(model);
		}

		[AllowAnonymous]
		public ActionResult ForgotPassword()
		{
			return View();
		}

		//
		// POST: /Account/ForgotPassword
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var usr = UserManager.FindByName(model.UserName);

				if (usr == null)
				{
					ModelState.AddModelError("AllError", "Không tìm thấy thông tin tài khoản " + model.UserName);
					return View();
				}
				else
				{
					//if (usr.UserName == model.UserName && usr.Email == model.Email)
					//{
					//    ViewBag.IdUser = usr.Id;
					//    return View("ResetForgotPassword");
					//}
					if (string.IsNullOrEmpty(usr.Email))
					{
						ModelState.AddModelError("AllError", "Tài khoản chưa cập nhật Email. Vui lòng liên hệ admin để được hỗ trợ.");
						return View();
					}
					else if (usr.Email.ToLower() != model.Email.ToLower())
					{
						ModelState.AddModelError("AllError", "Tài khoản và email không khớp nhau.");
						return View();
					}

					var user = _appUserService.GetUserByUsername(model.UserName);
					var token = string.Format("{0}-{1}", user.Id, StringUtilities.GenerateCoupon(10));

					user.Token = token;
					_appUserService.Update(user);
					int movideId = 0;
				}

				return View("ForgotPassword");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		public ActionResult ResetForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ResetForgotPassword(RenewPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				//ModelState.AddModelError("AllError", "Lõi " + ModelState.GetErrors());
				return View();
			}
			var objU = _appUserService.GetById(model.IdUser);

			_appUserService.Update(objU);
			var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			if (string.IsNullOrEmpty(objU.SecurityStamp))
			{
				UserManager.UpdateSecurityStamp(objU.Id);
			}

			var tokenUpdatePass = UserManager.GeneratePasswordResetToken(objU.Id);
			var defaultPass = "12345678";
			var rs = UserManager.ResetPassword(objU.Id, tokenUpdatePass, defaultPass);

			model.OldPassword = "12345678";

			var result = await UserManager.ChangePasswordAsync(model.IdUser, model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
				if (user != null)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				}
				return View("Login");
			}
			AddErrors(result);
			return View();
		}

		[HttpGet]
		//[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<ActionResult> QuickLogin(string userName)
		{
			//Đăng xuất
			var infoUser = SessionManager.GetValue(SessionManager.USER_INFO) as AppUser;
			if (infoUser != null)
			{
				RepositoryConnectUser.RemoveConnect(infoUser.Id);
			}
			SessionManager.SetValue(SessionManager.USER_INFO, null);
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			Session["IsFirstVisit"] = true;

			var result = await SignInManager.PasswordSignInAsync(userName, "12345678", true, shouldLockout: false);

			if (result == SignInStatus.Success)
			{
				var userId = SignInManager.AuthenticationManager.AuthenticationResponseGrant.Identity.GetUserId<long>();

				var userDto = _appUserService.GetDtoById(userId);

				SessionManager.SetValue(SessionManager.USER_INFO, userDto);
				var listOperation = _operationService.GetListOperationOfUser(userId);
				SessionManager.SetValue(SessionManager.LIST_PERMISSTION, listOperation);
				//Lưu thông tin người đăng nhập
				RepositoryConnectUser.SaveConnect(userId);

				return RedirectToAction("Index", "Dashboard", new { area = "DashboardArea" });
			}
			else
			{
				return RedirectToAction("Login", "AccountAdmin");
			}
		}
	}
}