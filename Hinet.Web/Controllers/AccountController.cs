using AutoMapper;
using BotDetect.Web.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.NotificationService;
using Hinet.Service.OperationService;
using Hinet.Service.RoleService;
using Hinet.Service.UserRoleService;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Web.Common;

namespace Hinet.Web.Controllers
{
    [Authorize]
    public class AccountController : EndUserController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        //private IDatabase _cacheDatabase;
        private IOperationService _operationService;

        private IAppUserService _appUserService;
        private IDM_DulieuDanhmucService _iDM_DuLieuDanhMucService;
        private readonly ILog _Ilog;
        private readonly INotificationService _notificationService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;

        public AccountController(
            IOperationService operationService,
            IDM_DulieuDanhmucService iDM_DuLieuDanhMucService,
            ILog Ilog,
            IRoleService roleService,
            INotificationService notificationService,
            IMapper mapper,
            IUserRoleService userRoleService,

        IAppUserService appUserService)
        //:this(new UserManager<AppUser,long>(new UserStore<AppUser,AppRole,long,AppLogin,AppUserRole,AppClaim>(new QLNSContext())))
        {
            _userRoleService = userRoleService;
            _roleService = roleService;
            _mapper = mapper;
            _notificationService = notificationService;
            _operationService = operationService;
            //_cacheDatabase = cacheDatabase;
            _appUserService = appUserService;
            _iDM_DuLieuDanhMucService = iDM_DuLieuDanhMucService;
            _Ilog = Ilog;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult OldUser(long id)
        {
            var appuser = _appUserService.GetById(id);
            if (appuser == null)
            {
                return new HttpNotFoundResult();
            }
            return View(appuser);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidationActionFilter("CaptchaUpdatePass", "UpdatePasswordCaptcha", "Sai mã xác nhận!")]
        public async Task<ActionResult> SaveOldUser(long id)
        {
            var appuser = _appUserService.GetById(id);

            if (appuser == null)
            {
                return new HttpNotFoundResult();
            }
            if (string.IsNullOrEmpty(appuser.SecurityStamp))
            {
                await UserManager.UpdateSecurityStampAsync(appuser.Id);
            }
            appuser = _appUserService.GetById(id);
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(appuser.Email))
                {
                    ModelState.AddModelError("AllError", "Tài khoản chưa cập nhật Email. Vui lòng liên hệ admin để được hỗ trợ.");
                    return View("OldUser", appuser);
                }

                var token = UserManager.GeneratePasswordResetToken(appuser.Id);

                MvcCaptcha.ResetCaptcha("UpdatePasswordCaptcha");
                return View("UpdatePassResult", appuser);
            }
            else
            {
                return View("OldUser", appuser);
            }
        }

        [AllowAnonymous]
        public PartialViewResult LoginHeader(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
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
                    //_cacheDatabase.StringSet("Operation:" + userId, JsonConvert.SerializeObject(listOperation));
                    SessionManager.SetValue(SessionManager.LIST_PERMISSTION, listOperation);

                    //if (userDto.TypeAccount == AccountTypeConstant.BussinessUser)
                    //{
                    //	return RedirectToAction("Index", "Dashboard", new { area = "DashboardArea" });
                    //}
                    //else if (userDto.TypeAccount == AccountTypeConstant.NvXang)
                    //{
                    //	return RedirectToAction("Index", "Dashboard", new { area = "DashboardArea" });
                    //}
                    return Json(new { success = true, message = "Đăng nhập thành công!" });

                case SignInStatus.LockedOut:
                    ViewBag.TimeAutoUnlock = UserManager.GetLockoutEndDate(user.Id).AddHours(7);
                    return Json(new { success = false, message = "Đăng nhập không thành công!" });


                //case SignInStatus.RequiresVerification:
                //	return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = true });

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
                        return Json(new { success = false, message });

                    }
                    //ModelState.AddModelError("", "");
                    return Json(new { success = false, message = "Thông tin đăng nhập không đúng!" });


                default:
                    return Json(new { success = false, message = "Thông tin đăng nhập không đúng!" });

            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserName = model.UserName?.Trim();

                var existingUser = await UserManager.FindByNameAsync(model.UserName);
                if (existingUser != null)
                {
                    return Json(new { success = false, message = "Tài khoản đã tồn tại, vui lòng chọn tên khác." });
                }

                var user = new AppUser
                {
                    UserName = model.UserName,
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    try
                    {
                        // Gán role mặc định
                        var roleInfo = _roleService.GetIdByCode(RoleConstant.KHACH);
                        var userRole = new UserRole
                        {
                            UserId = user.Id,
                            RoleId = (int)roleInfo
                        };
                        _userRoleService.Create(userRole);

                        // Đăng nhập ngay
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // Set session
                        var userDto = _appUserService.GetDtoById(user.Id);
                        SessionManager.SetValue(SessionManager.USER_INFO, userDto);

                        var listOperation = _operationService.GetListOperationOfUser(user.Id);
                        SessionManager.SetValue(SessionManager.LIST_PERMISSTION, listOperation);

                        return Json(new { success = true, message = "Đăng ký thành công!" });
                    }
                    catch (Exception ex)
                    {
                        _Ilog.Error("Lỗi khi gán role/khởi tạo session", ex);
                        return Json(new { success = false, message = "Đăng ký thành công nhưng xảy ra lỗi khi khởi tạo session." });
                    }
                }

                // Lấy lỗi từ Identity
                var errorMsg = string.Join("; ", result.Errors);
                return Json(new { success = false, message = errorMsg });
            }

            // ModelState không valid
            var modelError = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault() ?? "Dữ liệu không hợp lệ";

            return Json(new { success = false, message = modelError });
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(long userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
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

                    var token = UserManager.GeneratePasswordResetToken(usr.Id);
                }

                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string username, string code)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.UserName = username;
            model.Code = code;
            return code == null ? View("Error") : View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        #region command

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new AppUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff

        #endregion command

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            SessionManager.SetValue(SessionManager.USER_INFO, null);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        ////
        //// GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

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

        private string CheckExistUserName(string username)
        {
            var temp = -1;
            var name = username;
            do
            {
                temp++;
                username = temp > 0 ? name + temp.ToString() : name;
            } while (_appUserService.CheckExistUserName(username, null));

            return username;
        }

		//[HttpGet]
		//public ActionResult SignInGoogle()
		//{
		//    var redirectUri = Url.Action(nameof(HandleGoogleCallback), "Authentication");
		//    var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUri);
		//    return new ChallengeResult("Google", properties);
		//}


		//[HttpGet]
		//public ActionResult HandleGoogleCallback()
		//{
		//    var result = await _userService.CreateWithGoogleAsync();
		//    if (result.IsSucceed) return RedirectToAction("Index", "Home", new { area = "" });
		//    ViewBag.LoginError = result.Message;
		//    return RedirectToAction(nameof(Login));
		//}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin(string provider)
        {
            // Request a redirect to the external login provider
            var returnUrl = Url.Action("Index", "Home");
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var email = loginInfo.Email;
            var user = await UserManager.FindByEmailAsync(email);
            //kiểm tra xem có tài khoản chưa, nếu chưa có thì tạo mới, nếu có rồi thì lấy tài khoản đó đăng nhập
            if (user == null)
            {
                // Tạo tài khoản mới
                var userName = email.Split('@')[0] + DateTime.Now.Ticks.ToString();
                user = new AppUser { UserName = userName, Email = email };
                var createResult = await UserManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    // Gán role mặc định
                    var roleInfo = _roleService.GetIdByCode(RoleConstant.KHACH);
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = (int)roleInfo
                    };
                    _userRoleService.Create(userRole);
                    // Đăng nhập ngay sau khi tạo tài khoản
                }
            }
            // Set session
            var userDto = _appUserService.GetDtoById(user.Id);
            SessionManager.SetValue(SessionManager.USER_INFO, userDto);
            var listOperation = _operationService.GetListOperationOfUser(user.Id);
            SessionManager.SetValue(SessionManager.LIST_PERMISSTION, listOperation);
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: true);
            return RedirectToLocal(returnUrl);

        }


    }
}