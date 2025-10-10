using Autofac;
using log4net;
using System.Web.Http;

namespace Hinet.Web.Filters
{
    public class BaseApiController : ApiController
    {
        private ILog _loger;
        //protected long? CurrentUserId = null;
        //protected UserDto CurrentUserInfo;
        //private readonly IAppUserService _appUserService;
        //private readonly IOperationService _operationService;

        private static IContainer Container;

        public BaseApiController()
        {
            _loger = LogManager.GetLogger("RollingLogFileAppender");
            //_appUserService = DependencyResolver.Current.GetService<IAppUserService>();
            //_operationService = DependencyResolver.Current.GetService<IOperationService>();
            //CurrentUserInfo = SessionManager.GetUserInfo() as UserDto;
            //if (CurrentUserInfo != null)
            //{
            //    CurrentUserId = CurrentUserInfo.Id;

            //}
        }

        ///// <summary>
        ///// Kiểm tra xem user hiện tại có quyền không
        ///// </summary>
        ///// <param name="permission"></param>
        ///// <returns></returns>
        //public bool HasPermission(string permission)
        //{
        //    if (CurrentUserInfo != null && CurrentUserInfo.ListOperations != null)
        //    {
        //        if (CurrentUserInfo.ListOperations.Any(x => x.Code == permission))
        //        {
        //            return true;
        //        }

        //    }
        //    return false;
        //}
        //public bool HasRole(string roleCode)
        //{
        //    if (CurrentUserInfo != null && CurrentUserInfo.ListRoles != null)
        //    {
        //        if (CurrentUserInfo.ListRoles.Any(x => x.Code == roleCode))
        //        {
        //            return true;
        //        }

        //    }
        //    return false;
        //}

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

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session != null)
        //    {
        //        bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
        //       || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
        //        if (!skipAuthorization)
        //        {
        //            var userInfo = SessionManager.GetUserInfo() as UserDto;
        //            if (filterContext.HttpContext.Session.IsNewSession || userInfo == null)
        //            {
        //                if (filterContext.HttpContext.Request.IsAjaxRequest())
        //                {
        //                    if (((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType == typeof(JsonResult))
        //                    {
        //                        var rs = new JsonResultBO(false);
        //                        rs.Message = "Phiên làm việc của bạn đã hết";
        //                        filterContext.Result = Json(rs);
        //                    }
        //                    else if (((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType == typeof(PartialViewResult))
        //                    {
        //                        filterContext.Result =
        //                        RedirectToAction("TimeOutSession", "Errors", new { area = "" });
        //                    }

        //                }
        //                else
        //                {
        //                    filterContext.Result =
        //                   RedirectToAction("login", "accountadmin", new { area = "" });
        //                }

        //                return;
        //            }
        //            //else
        //            //{
        //            //        //filterContext.Result = RedirectToAction("Login", "AccountAdmin", new { Area = "" });

        //            //}

        //        }
        //    }

        //    base.OnActionExecuting(filterContext);
        //}
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    _loger.Error("Lỗi hệ thống", filterContext.Exception);
        //    TempData["filterContext"] = filterContext;
        //    //filterContext.ExceptionHandled = true;

        //    //// Redirect on error:
        //    //filterContext.Result = RedirectToAction("Index", "Errors", filterContext.Exception);

        //    // OR set the result without redirection:
        //    //filterContext.Result = new ViewResult
        //    //{
        //    //    ViewName = "~/Views/Errors/Index.cshtml"
        //    //};
        //}
    }
}