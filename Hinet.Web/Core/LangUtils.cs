using Hinet.Web.LocationResouces;

namespace Hinet
{
    public class LangUtils
    {
        public static string Get(string key)
        {
            var manager = ResourceWeb.ResourceManager;
            return manager.GetString(key);
        }
    }
}