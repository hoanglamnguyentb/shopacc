using Hinet.API2.Common;
using Nest;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.UI.WebControls;

namespace Hinet.API2.Core
{
    public class AuthorAPIAttribute : AuthorizationFilterAttribute
    {
        public string RoleActive { get; set; }
        public AuthorAPIAttribute(string role = null)
        {
            RoleActive = role;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var identity = FetchFromHeader(actionContext);
            if (identity != null)
            {
                var isValidToken = TokenJWTProvider.ValidateToken(identity);
                if (isValidToken.Item1 == false)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
                var tokenData = TokenJWTProvider.ReadJwtToken(identity);
                var userId = tokenData.Claims.Where(x => x.Type == "nameid").FirstOrDefault().Value;
                var ListRoles = tokenData.Claims.Where(x => x.Type == "role").FirstOrDefault().Value.Split(';').ToList();
                HttpContext.Current.Items["UserId"] = userId;

                if (string.IsNullOrEmpty(RoleActive) || !ListRoles.Any(x => x.Equals(RoleActive)))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }

                var gprincipal = new GenericPrincipal(new GenericIdentity(userId), null);
                Thread.CurrentPrincipal = gprincipal;
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// retrive header detail from the request
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Bearer")
                requestToken = authRequest.Parameter;

            return requestToken;
        }
    }
}