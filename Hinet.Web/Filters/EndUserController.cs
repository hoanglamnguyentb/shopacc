using Hinet.Service.AppUserService;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.OperationService;
using log4net;
using Microsoft.AspNet.Identity;
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

			CurrentUserInfo = SessionManager.GetUserInfo() as UserDto;
			if (CurrentUserInfo != null)
			{
				CurrentUserId = CurrentUserInfo.Id;
			}
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
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
							filterContext.Result =
						   RedirectToAction("login", "account", new { area = "" });
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
			//filterContext.ExceptionHandled = true;

			//// Redirect on error:
			//filterContext.Result = RedirectToAction("Index", "Errors", filterContext.Exception);

			// OR set the result without redirection:
			//filterContext.Result = new ViewResult
			//{
			//    ViewName = "~/Views/Errors/Index.cshtml"
			//};
		}
	}
}