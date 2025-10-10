using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Hinet.API2.Core
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                var usernamePasswordArray = decodedAuthenticationToken.Split(':');
                var userName = usernamePasswordArray[0];
                var password = usernamePasswordArray[1];

                var currentDate = DateTime.Now;
                // Replace this with your own system of security / means of validating credentials
                //var dbContext = new HinetContext();
                //var isValid = dbContext.ConnectedAccount.Where(x => x.Account == userName && x.Password == password && x.IsActive == true && x.UsingStart <= currentDate && x.UsingEnd >= currentDate).Any();
                //if (isValid)
                //{
                //    var principal = new GenericPrincipal(new GenericIdentity(userName), null);
                //    Thread.CurrentPrincipal = principal;
                //    //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, "User " + userName + " successfully authenticated");
                //    return;
                //}
                if (userName == "apiaccount" && password == "12345678Abc!!")
                {
                    return;
                }
            }

            HandleUnathorized(actionContext);
        }

        private static void HandleUnathorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Data' location = 'http://localhost:");
        }
    }
}