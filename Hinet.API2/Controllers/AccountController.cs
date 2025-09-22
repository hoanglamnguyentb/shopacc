using AutoMapper;
using CommonHelper.String;
using Hinet.API2.Common;
using Hinet.API2.Core;
using Hinet.API2.Models;
using Hinet.Model.IdentityEntities;
using Hinet.Service.AppUserService;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.EmailTemplateService;
using Hinet.Service.HistoryService;
using Hinet.Service.NotificationService;
using Hinet.Service.OperationService;
using Hinet.Service.QLHeThongTichHopService;
using Hinet.Service.RoleService;
using Hinet.Service.UserOperationService;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace Hinet.API2.Controllers
{
    [RoutePrefix("api/ct/account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private IAppUserService _appUserService;
        private IOperationService _operationService;
        private ILog _Ilog;
        private IMapper _mapper;
        private IDM_DulieuDanhmucService _iDM_DuLieuDanhMucService;
        private IHistoryService _IHistoryService;
        private INotificationService _notificationService;
        private IEmailTemplateService _emailTemplateService;
        private string PathSaveUploads = WebConfigurationManager.AppSettings["PathSaveUploads"];
        private IRoleService _roleService;
        private readonly IUserOperationService _userOperationService;
        private readonly IQLHeThongTichHopService _qLHeThongTichHopService;

        public AccountController(IOperationService operationService,
            IDM_DulieuDanhmucService iDM_DuLieuDanhMucService,
            ILog Ilog,
            INotificationService notificationService,
            IMapper mapper,
            IEmailTemplateService emailTemplateService,
            IHistoryService IHistoryService,
            IAppUserService appUserService,
            IRoleService roleService,
             IUserOperationService userOperationService
,
             IQLHeThongTichHopService qLHeThongTichHopService)
        {
            _emailTemplateService = emailTemplateService;
            _notificationService = notificationService;
            _IHistoryService = IHistoryService;
            _mapper = mapper;
            _Ilog = Ilog;
            _appUserService = appUserService;
            _iDM_DuLieuDanhMucService = iDM_DuLieuDanhMucService;
            _operationService = operationService;
            _roleService = roleService;
            _userOperationService = userOperationService;
            _qLHeThongTichHopService = qLHeThongTichHopService;
        }

        //public AccountController(ApplicationUserManager userManager,
        //    ApplicationSignInManager signInManager,
        //    ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        //{
        //    UserManager = userManager;
        //    AccessTokenFormat = accessTokenFormat;
        //    SignInManager = signInManager;
        //    _signInManager = signInManager;
        //}
        public ApplicationSignInManager SignInManager
        {
            get
            {
                var owctx = Request.GetOwinContext();
                var sgn = owctx.Get<ApplicationSignInManager>();
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // POST api/Account/Logout
        /// <summary>
        /// API login hệ thống
        /// </summary>
        /// <remarks>
        /// Xác thực người dùng và trả về token truy cập
        /// Đặt token bearer vào các request cần xác thực
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("post/login")]
        [HttpPost]
        public async Task<APIResponseDto<LoginDoneVM>> Login(LoginViewModel model)
        {
            try
            {
                var HOSTWEB = WebConfigurationManager.AppSettings["HOSTWEB"];
                var modelRs = new APIResponseDto<LoginDoneVM>();
                modelRs.Data = new LoginDoneVM();
                //modelRs.Status = false;
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        modelRs.Status = true;
                        var objData = _appUserService.GetDtoByUserName(model.UserName);
                        var timeOutDate = DateTime.Now.AddHours(24);
                        var token = await TokenJWTProvider.CreateJWTAsync(objData, TokenJWTProvider.Issuer, TokenJWTProvider.Audien, 24);
                        modelRs.Data.TimeoutToken = timeOutDate;
                        modelRs.Data.Token = token;
                        if (!string.IsNullOrEmpty(objData.Avatar))
                        {
                            objData.Avatar = Path.Combine(HOSTWEB, objData.Avatar);
                        }

                        modelRs.Data.AccountInfo = _appUserService.GetAppUserVM_Api(objData.Id);
                        //if (modelRs.Data.AccountInfo == null)
                        //{
                        //    modelRs.Data.AccountInfo = new UserDto();
                        //}

                        return modelRs;

                    case SignInStatus.LockedOut:
                        modelRs.Status = false;
                        modelRs.Message = "Tài khoản đã bị khóa";

                        return modelRs;

                    case SignInStatus.RequiresVerification:
                        modelRs.Status = false;
                        modelRs.Message = "Chờ xác thực";
                        return modelRs;

                    case SignInStatus.Failure:
                        modelRs.Status = false;
                        modelRs.Message = "Sai thông tin xác thực";
                        return modelRs;

                    default:
                        return modelRs;
                }
            }
            catch (Exception exx)
            {
                _Ilog.Error(exx.Message, exx);
                throw exx;
            }
        }

        [Route("gettoken")]
        [HttpPost]
        public async Task<APIResponseDto<LoginDoneVM>> LoginApi(LoginViewModel model)
        {
            var modelRs = new APIResponseDto<LoginDoneVM>();
            modelRs.Data = new LoginDoneVM();
            modelRs.Status = false;
            try
            {
                //var check = _qLHeThongTichHopService.FindBy(x => x.TaiKhoan == model.UserName).FirstOrDefault();
                var appUser = _appUserService.GetDtoByUserName(model.UserName);
                if (appUser != null)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var timeOutDate = DateTime.Now.AddHours(24);
                        var token = await TokenJWTProvider.CreateJWTAsync(appUser, TokenJWTProvider.Issuer, TokenJWTProvider.Audien, 24);
                        modelRs.Data.TimeoutToken = timeOutDate;
                        modelRs.Data.Token = token;
                    }
                }
            }
            catch (Exception exx)
            {
                _Ilog.Error(exx.Message, exx);
            }
            return modelRs;
            //return modelRs;
        }

        private string GetErrors(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            string result = string.Empty;
            foreach (var item in modelState)
            {
                var state = item.Value;
                if (state.Errors.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in state.Errors)
                    {
                        sb.Append(error.ErrorMessage);
                    }
                    result = sb.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePassword")]
        [AuthorAPI]
        public async Task<APIResponseDto<string>> ChangePassword(ChangePasswordViewModel model)
        {
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            var resultAPI = new APIResponseDto<string>();

            try
            {
                var result = await UserManager.ChangePasswordAsync(CurrentUserId, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    resultAPI.Status = true;

                    return resultAPI;
                }
                else
                {
                    AddErrors(result);
                    resultAPI.Status = false;
                    resultAPI.Message = GetErrors(ModelState);
                }
            }
            catch (Exception ex)
            {
                resultAPI.Status = false;
                resultAPI.Message = ex.Message;
            }
            return resultAPI;
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<APIResponseDto<string>> ForgotPassword(ForgotPasswordViewModel model)
        {
            var resultAPI = new APIResponseDto<string>();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            if (ModelState.IsValid)
            {
                var usr = UserManager.FindByName(model.UserName);
                if (usr == null)
                {
                    resultAPI.Message = "Không tìm thấy thông tin tài khoản " + model.UserName;
                    resultAPI.Status = false;
                    return resultAPI;
                }
                else
                {
                    if (string.IsNullOrEmpty(usr.Email))
                    {
                        resultAPI.Message = "Tài khoản chưa cập nhật Email. Vui lòng liên hệ admin để được hỗ trợ.";
                        resultAPI.Status = false;
                        return resultAPI;
                    }
                    else if (usr.Email.ToLower() != model.Email.ToLower())
                    {
                        resultAPI.Message = "Tài khoản và email không khớp nhau.";
                        resultAPI.Status = false;
                        return resultAPI;
                    }

                    var user = _appUserService.GetUserByUsername(model.UserName);
                    var token = string.Format("{0}-{1}", user.Id, StringUtilities.GenerateCoupon(10));

                    user.Token = token;
                    //var password = "12345678Aa@";
                    var tokenUpdatePass = UserManager.GeneratePasswordResetToken(usr.Id);
                    var defaultPass = "12345678Aa@";
                    var rs = UserManager.ResetPassword(user.Id, tokenUpdatePass, defaultPass);
                    resultAPI.Status = true;
                    resultAPI.Message = "Đã gửi yêu cầu quên mật khẩu thành công!";

                    #region send mail

                    //var mailTemplate = _emailTemplateService.GetByCode(EmailTemplateConstant.QuenMatKhau);
                    //if (mailTemplate != null)
                    //{
                    //var maildto = _mapper.Map<Service.MailTemplateDTO.QuenMatKhauMailDto>(usr);

                    //var ClientHost = WebConfigurationManager.AppSettings["LinkHeThong"];
                    //var MailMacDinh = WebConfigurationManager.AppSettings["UpdatePasswordWithMail"];
                    //maildto.Token = string.Format("{0}{1}{2}", ClientHost, "/AccountAdmin/ChangePass?token=", token);
                    //EmailProvider.SendMailForgotPassword(user);
                    //var subject = "Quên mật khẩu";
                    //if (model.UserName == "admin")
                    //    {
                    //        Task.Run(() => EmailProvider.sendEmailSingle(message, subject, MailMacDinh));

                    //    }
                    //    Task.Run(() => EmailProvider.sendEmailSingle(message, subject, usr.Email));
                    //}
                    //else
                    //{
                    //    resultAPI.Status = false;
                    //    resultAPI.Message = "Không tìm thấy mẫu email quên mật khẩu";

                    //    _Ilog.Error("Không tìm thấy mẫu email quên mật khẩu");
                    //    return resultAPI;
                    //}

                    #endregion send mail
                }
            }
            else
            {
                resultAPI.Status = false;
                resultAPI.Message = "Không nhận được thông tin";
            }

            return resultAPI;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //// POST api/Account/Logout
        //[Route("Logout")]
        //public IHttpActionResult Logout()
        //{
        //    Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //    return Ok();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        [Route("GetDsNguoiDungPhongBan")]
        [AuthorAPI]
        [HttpGet]
        public async Task<APIResponseDto<List<UserDto>>> GetDsNguoiDungPhongBan()
        {
            var result = new APIResponseDto<List<UserDto>>();
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            var userInfo = _appUserService.GetDtoById(CurrentUserId);
            //var listUser = _appUserService.GetAllByMaTinhAndDepartment(userInfo.TinhProvince, userInfo.IdDepartment, (int)CurrentUserId);
            //result.Data = listUser;
            return result;
        }

        /// <summary>
        /// Xem thông tin cá nhân trên app mobile
        /// </summary>
        /// <returns></returns>
        [Route("GetThongTinCaNhan")]
        [AuthorAPI]
        [HttpGet]
        public async Task<APIResponseDto<UserDto>> GetThongTinCaNhan(long id)
        {
            var result = new APIResponseDto<UserDto>();
            var CurrentUserId = Thread.CurrentPrincipal.Identity.Name.ToLongOrZero();
            var userInfo = id > 0 ? _appUserService.GetDtoById(id) : null;
            HttpStatusCode statusCode = HttpStatusCode.OK;
            if (userInfo != null)
            {
                result.Status = true;
                result.Data = userInfo;
                //Lưu log
                System.Web.HttpContext context = System.Web.HttpContext.Current;
            }
            else
            {
                result.Status = false;
                result.Data = null;
                result.Message = "Không có bản ghi nào";
            }
            return result;
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion Helpers
    }
}