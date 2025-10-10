using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hinet.Web.Filters
{
    public class PermissionStatusAccess : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// HoSoThuongNhan_12
        /// Code=HoSoThuongNhan_
        /// Param=12
        /// </summary>
        public string Code { get; set; }

        public string ParamValid { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(ParamValid))
            {
                return;
            }
            var param = filterContext.ActionParameters[ParamValid];

            var codePermission = Code + param;
            if (string.IsNullOrEmpty(codePermission))
            {
                return;
            }

            var userinfo = SessionManager.GetUserInfo() as UserDto;
            var isAccess = true;
            if (userinfo != null)
            {
                if (userinfo.ListOperations != null && userinfo.ListOperations.Any(x => x.Code == codePermission))
                {
                    isAccess = true;
                }
                else
                {
                    isAccess = false;
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