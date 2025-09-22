using System.Net;
using System.Web;
using System.Web.Mvc;

public class IpFilterAttribute : AuthorizeAttribute
{
    private readonly string _ipAddress;

    public IpFilterAttribute(string ipAddress)
    {
        _ipAddress = ipAddress;
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        IPAddress remoteIpAddress = IPAddress.Parse(httpContext.Request.UserHostAddress);
        IPAddress allowedIpAddress = IPAddress.Parse(_ipAddress);

        return remoteIpAddress.Equals(allowedIpAddress);
    }
}