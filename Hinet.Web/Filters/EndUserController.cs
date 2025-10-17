using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.OperationService;
using Hinet.Service.SiteConfigService;
using Hinet.Service.SiteConfigService.Dto;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Runtime.Caching;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Hinet.Web.Filters
{
    public class EndUserController : Controller
    {
        private ILog _loger;
        protected UserDto CurrentUserInfo;
        private readonly IAppUserService _appUserService;
        private readonly IOperationService _operationService;
        private readonly ISiteConfigService _siteConfigService;

        protected long? CurrentUserId = null;

        public EndUserController()
        {
            var hostComlain = WebConfigurationManager.AppSettings["ComplainSite"];
            var hostReport = WebConfigurationManager.AppSettings["BaoCaoSite"];
            SessionManager.SetValue(SessionManager.HOST_COMPLAIN, hostComlain);
            SessionManager.SetValue(SessionManager.HOST_REPORT, hostReport);

            _loger = LogManager.GetLogger("RollingLogFileAppender");
            _appUserService = DependencyResolver.Current.GetService<IAppUserService>();
            _operationService = DependencyResolver.Current.GetService<IOperationService>();
            _siteConfigService = DependencyResolver.Current.GetService<ISiteConfigService>();

            CurrentUserInfo = SessionManager.GetUserInfo() as UserDto;
            if (CurrentUserInfo != null)
            {
                CurrentUserId = CurrentUserInfo.Id;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                ViewBag.SiteConfig = GetSiteConfigCached();
            }
            catch (Exception ex)
            {
                ViewBag.SiteConfig = null;
                _loger.Error("Lỗi load SiteConfig", ex);
            }

            if (filterContext.HttpContext.Session != null)
            {
                bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
               || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
                if (!skipAuthorization)
                {
                    var userInfo = SessionManager.GetUserInfo() as UserDto;
                    if (filterContext.HttpContext.Session.IsNewSession || userInfo == null)
                    {
                        HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            if (((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType == typeof(JsonResult))
                            {
                                var rs = new JsonResultBO(false);
                                rs.Message = "Phiên làm việc của bạn đã hết";
                                filterContext.Result = Json(rs);
                            }
                            else if (((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType == typeof(PartialViewResult))
                            {
                                filterContext.Result =
                                RedirectToAction("TimeOutSession", "Error", new { area = "" });
                            }
                        }
                        else
                        {
                            filterContext.Result = RedirectToAction("Index", "Home", new { area = "" });
                        }

                        return;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            _loger.Error("Lỗi hệ thống", filterContext.Exception);
            TempData["filterContext"] = filterContext;
        }

        protected void RefreshSession()
        {
            try
            {
                if (CurrentUserId.HasValue)
                {
                    SessionManager.Remove(SessionManager.USER_INFO);
                    var userDto = _appUserService.GetDtoById(CurrentUserId.Value);
                    SessionManager.SetValue(SessionManager.USER_INFO, userDto);
                    CurrentUserInfo = userDto; // Cập nhật luôn biến cache trong controller
                }
            }
            catch (Exception ex)
            {
                _loger.Error("Lỗi khi RefreshSession", ex);
            }
        }

        private SiteConfig GetSiteConfigCached()
        {
            ObjectCache cache = MemoryCache.Default;
            string cacheKey = "SiteConfig";

            var config = cache.Get(cacheKey) as SiteConfigDto;
            if (config != null)
                return config;

            config = _siteConfigService.GetActiveConfig();
            if (config != null)
            {
                //var policy = new CacheItemPolicy
                //{
                //    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10)
                //};
                var policy = new CacheItemPolicy();
                cache.Set(cacheKey, config, policy);
            }

            return config;
        }

    }
}