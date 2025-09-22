using System.Web.Mvc;

namespace Hinet.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuditAttribute() { AuditingLevel = 2 });
        }
    }
}