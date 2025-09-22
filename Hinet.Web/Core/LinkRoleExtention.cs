using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;

namespace Hinet.Web.Core
{
    public static class LinkRoleExtention
    {
        public static MvcHtmlString ShowStatusDuAn(this HtmlHelper helper, ShowStatusDto objStatus)
        {
            var color = "#ffffff";
            var bgcolor = "#ecf0f1";
            var icon = "<i class='fa fa-circle'></i>";
            if (objStatus != null)
            {
                if (!string.IsNullOrEmpty(objStatus.Color))
                {
                    color = objStatus.Color;
                }
                if (!string.IsNullOrEmpty(objStatus.BgColor))
                {
                    bgcolor = objStatus.BgColor;
                }
                if (!string.IsNullOrEmpty(objStatus.Icon))
                {
                    icon = "<i class='" + objStatus.Icon + "'></i>";
                }

                var builder = new TagBuilder("div");

                builder.InnerHtml = $"{icon} <span>{objStatus.Name}</span>";

                builder.MergeAttribute("class", "statusInTable");
                builder.MergeAttribute("style", $"color:{color}; background-color: {bgcolor}");
                return new MvcHtmlString(builder.ToString());
            }
            return new MvcHtmlString(new TagBuilder("div").ToString());
        }

        public static MvcHtmlString LinkRole(this HtmlHelper helper, string id, string Url, string Name, string Role = "", string strClass = "", string title = "")
        {
            var strUrl = Url;

            var builder = new TagBuilder("a");
            builder.GenerateId(id);
            builder.InnerHtml = Name;
            builder.AddCssClass(strClass);
            builder.MergeAttribute("href", strUrl);
            builder.MergeAttribute("class", strClass);
            builder.MergeAttribute("title", title);

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString LinkRole(this HtmlHelper helper, string Url, string Name, string Role = "", string strClass = "", string title = "")
        {
            var CurrentUser = SessionManager.GetUserInfo() as UserDto;
            //Nếu Role - Mã thao tác tồn tại thì xét quyền truy cập
            if (!string.IsNullOrEmpty(Role))
            {
                //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
                if (CurrentUser == null || CurrentUser.ListOperations == null)
                {
                    return null;
                }
                else
                {
                    //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
                    if (!CurrentUser.ListOperations.Any(x => x.Code.Equals(Role)))
                    {
                        return null;
                    }
                }
            }

            var strUrl = Url;

            var builder = new TagBuilder("a");
            builder.InnerHtml = Name;
            builder.MergeAttribute("href", strUrl);
            builder.MergeAttribute("class", strClass);
            builder.MergeAttribute("title", title);

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString GetLinkStatus(this HtmlHelper helper, StatusRefer ObjRefer, string strClass = "", string[] prams = null)
        {
            var builder = new TagBuilder("a");
            var txtResultLink = string.Empty;
            var url = ObjRefer.LinkPattern;
            if (prams != null)
            {
                for (var i = 0; i < prams.Count(); i++)
                {
                    url = url.Replace("{" + i + "}", prams[i]);
                }
            }
            var icon = "";
            if (ObjRefer.Icon != null)
            {
                icon = "<i class='" + ObjRefer.Icon + "'> </i> ";
            }
            switch (ObjRefer.Type)
            {
                case 1:
                    builder.AddCssClass(strClass);
                    builder.MergeAttribute("href", url);
                    builder.MergeAttribute("title", ObjRefer.Name);
                    //builder.MergeAttribute("onClick", strUrl);
                    builder.InnerHtml = icon + ObjRefer.Name;
                    return new MvcHtmlString(builder.ToString());

                case 2:
                    builder.AddCssClass(strClass);
                    builder.MergeAttribute("href", "javascripts:void(0)");
                    builder.MergeAttribute("title", ObjRefer.Name);
                    builder.MergeAttribute("onClick", "EditAction(\"" + url + "\")");
                    builder.InnerHtml = icon + ObjRefer.Name;
                    return new MvcHtmlString(builder.ToString());

                case 3:
                    builder.AddCssClass(strClass);
                    builder.MergeAttribute("href", "javascripts:void(0)");
                    builder.MergeAttribute("title", ObjRefer.Name);
                    builder.MergeAttribute("onClick", "confirmLink(\"" + url + "\")");
                    builder.InnerHtml = icon + ObjRefer.Name;
                    return new MvcHtmlString(builder.ToString());
            }
            //var CurrentUser = SessionManager.GetUserInfo() as UserDto;
            ////Nếu Role - Mã thao tác tồn tại thì xét quyền truy cập
            //if (!string.IsNullOrEmpty(Role))
            //{
            //    //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
            //    if (CurrentUser == null || CurrentUser.ListOperations == null)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
            //        if (!CurrentUser.ListOperations.Any(x => x.Code.Equals(Role)))
            //        {
            //            return null;
            //        }
            //    }
            //}

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString LinkRole(this HtmlHelper helper, string Url, string Name, string Role = "", string strClass = "", string[] Param = null, string title = "")
        {
            var CurrentUser = SessionManager.GetUserInfo() as UserDto;
            //Nếu Role - Mã thao tác tồn tại thì xét quyền truy cập
            if (!string.IsNullOrEmpty(Role))
            {
                //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
                if (CurrentUser == null || CurrentUser.ListOperations == null)
                {
                    return null;
                }
                else
                {
                    //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
                    if (!CurrentUser.ListOperations.Any(x => x.Code.Equals(Role)))
                    {
                        return null;
                    }
                }
            }

            var strUrl = Url;
            strUrl = strUrl + "(";
            //Nếu liên kết là Method JavaScript

            if (Param != null && Param.Any())
            {
                strUrl += string.Join(",", Param);
            }

            strUrl += ")";
            var builder = new TagBuilder("a");
            builder.InnerHtml = Name;
            builder.AddCssClass(strClass);
            builder.MergeAttribute("href", "javascript:void(0)");
            builder.MergeAttribute("title", title);
            builder.MergeAttribute("onClick", strUrl);

            return new MvcHtmlString(builder.ToString());
        }

        //public static MvcHtmlString LinkRole(this HtmlHelper helper, string id, string Url, string Name, string Role = "", string strClass = "", string[] Param = null, bool IsJavaScriptFunction = false)
        //{
        //    var CurrentUser = SessionManager.GetUserInfo() as UserInfoBO;
        //    //Nếu Role - Mã thao tác tồn tại thì xét quyền truy cập
        //    if (!string.IsNullOrEmpty(Role))
        //    {
        //        //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
        //        if (CurrentUser == null || CurrentUser.ListThaoTac == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
        //            if (!CurrentUser.ListThaoTac.Any(x => x.MA_THAOTAC.Equals(Role)))
        //            {
        //                return null;
        //            }
        //        }
        //    }

        //    var strUrl = Url;
        //    //Nếu liên kết là Method JavaScript
        //    if (IsJavaScriptFunction)
        //    {
        //        strUrl = strUrl + "(";
        //        if (Param != null && Param.Any())
        //        {
        //            strUrl += string.Join(",", Param);
        //        }
        //        strUrl += ")";
        //    }
        //    var builder = new TagBuilder("a");
        //    builder.GenerateId(id);
        //    builder.InnerHtml = Name;
        //    builder.AddCssClass(strClass);
        //    if (IsJavaScriptFunction)
        //    {
        //        builder.MergeAttribute("href", "javascript:void(0)");
        //        builder.MergeAttribute("onClick", strUrl);

        //    }
        //    else
        //    {
        //        builder.MergeAttribute("href", strUrl);
        //    }

        //    return new MvcHtmlString(builder.ToString());
        //}

        public static MvcHtmlString GetRole(this HtmlHelper helper)
        {
            var CurrentUser = SessionManager.GetUserInfo() as UserDto;
            if (CurrentUser == null || CurrentUser.ListOperations == null)
            {
                return null;
            }
            else
            {
                //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
                var strRole = JsonConvert.SerializeObject(CurrentUser.ListOperations.Select(x => x.Code).ToList());
                return new MvcHtmlString(strRole);
            }
            return null;
        }

        public static bool HasRole(this HtmlHelper helper, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return false;
            }

            var CurrentUser = SessionManager.GetUserInfo() as UserDto;
            if (CurrentUser == null || CurrentUser.ListOperations == null)
            {
                return false;
            }
            else
            {
                if (CurrentUser.ListOperations.Any(x => x.Code.Equals(role)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return false;
            }

            var CurrentUser = SessionManager.GetUserInfo() as UserDto;
            if (CurrentUser == null || CurrentUser.ListOperations == null)
            {
                return false;
            }
            else
            {
                if (CurrentUser.ListOperations.Any(x => x.Code.Equals(role)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}