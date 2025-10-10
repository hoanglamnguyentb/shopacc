using Autofac;
using Hinet.Service.AppUserService;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.OperationService;
using Hinet.Service.SiteConfigService;
using log4net;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hinet.Web.Filters
{
    public class BaseController : Controller
    {
        private ILog _loger;
        protected long? CurrentUserId = null;
        protected UserDto CurrentUserInfo;
        private readonly IAppUserService _appUserService;
        private readonly IOperationService _operationService;

        private static IContainer Container;

        public BaseController()
        {
            _loger = LogManager.GetLogger("RollingLogFileAppender");
            _appUserService = DependencyResolver.Current.GetService<IAppUserService>();
            _operationService = DependencyResolver.Current.GetService<IOperationService>();
            CurrentUserInfo = SessionManager.GetUserInfo() as UserDto;
            if (CurrentUserInfo != null)
            {
                CurrentUserId = CurrentUserInfo.Id;
            }

        }

        /// <summary>
        /// Kiểm tra xem user hiện tại có quyền không
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool HasPermission(string permission)
        {
            if (CurrentUserInfo != null && CurrentUserInfo.ListOperations != null)
            {
                if (CurrentUserInfo.ListOperations.Any(x => x.Code == permission))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasRole(string roleCode)
        {
            if (CurrentUserInfo != null && CurrentUserInfo.ListRoles != null)
            {
                if (CurrentUserInfo.ListRoles.Any(x => x.Code == roleCode))
                {
                    return true;
                }
            }
            return false;
        }

        //protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        //{
        //    bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
        //      || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
        //    if (!skipAuthorization)
        //    {
        //        var userinfo = SessionManager.GetUserInfo() as UserDto;

        //        if (userinfo == null || userinfo.TypeAccount != AccountTypeConstant.BussinessUser)
        //        {
        //            filterContext.Result = RedirectToAction("Login", "AccountAdmin", new { Area = "" });
        //        }

        //    }
        //    base.OnAuthentication(filterContext);
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // --- Kiểm tra Session đăng nhập ---
            var httpContext = filterContext.HttpContext;
            if (httpContext?.Session != null)
            {
                bool skipAuthorization =
                    filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

                if (!skipAuthorization)
                {
                    var userInfo = SessionManager.GetUserInfo() as UserDto;
                    if (httpContext.Session.IsNewSession || userInfo == null)
                    {
                        if (httpContext.Request.IsAjaxRequest())
                        {
                            var actionType = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType;

                            if (actionType == typeof(JsonResult))
                            {
                                var rs = new JsonResultBO(false)
                                {
                                    Message = "Phiên làm việc của bạn đã hết"
                                };
                                filterContext.Result = Json(rs, JsonRequestBehavior.AllowGet);
                            }
                            else if (actionType == typeof(PartialViewResult))
                            {
                                filterContext.Result = RedirectToAction("TimeOutSession", "Errors", new { area = "" });
                            }
                        }
                        else
                        {
                            filterContext.Result = RedirectToAction("login", "accountadmin", new { area = "" });
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
    }
}