using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web.Filters
{
	public class PermissionAccess : ActionFilterAttribute, IActionFilter
	{
		//public List<string> lstCode { get; set; }
		public string Code { get; set; }

		void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
		{
			return;
			if (string.IsNullOrEmpty(Code))
			{
				return;
			}

			List<string> lstAction = Code.Split('|').ToList();
			lstAction = lstAction.Where(x => !string.IsNullOrEmpty(x)).ToList();
			if (!lstAction.Any())
			{
				return;
			}

			var userinfo = SessionManager.GetUserInfo() as UserDto;
			var isAccess = true;
			if (userinfo != null)
			{
				if (userinfo.ListOperations != null)
				{
					for (int i = 0; i < lstAction.Count; i++)
					{
						if (userinfo.ListOperations.Any(x => x.Code == lstAction[i]))
						{
							isAccess = true;
							break;
						}
						else
						{
							isAccess = false;
						}
					}
				}
			}
			else
			{
				isAccess = false;
			}

			if (!isAccess)
			{
				if (((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType == typeof(JsonResult))
				{
					var rs = new JsonResultBO(false);
					rs.Message = "Bạn không có quyền truy cập";
					var jsresult = new JsonResult();
					jsresult.ContentType = "json";
					//jsresult.Data = JsonConvert.SerializeObject(rs);
					jsresult.Data = rs;
					filterContext.Result = jsresult;
				}
				else if (((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo.ReturnType == typeof(PartialViewResult))
				{
					filterContext.Result = new RedirectToRouteResult(new
					 RouteValueDictionary(new { controller = "Home", action = "UnAuthorPartial", area = "" }));
				}
				else
				{
					filterContext.Result = new RedirectToRouteResult(new
					 RouteValueDictionary(new { controller = "Home", action = "UnAuthor", area = "" }));
				}
			}
		}
	}
}