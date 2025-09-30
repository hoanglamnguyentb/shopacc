using System;
using System.IO;
using System.Web.Hosting;

namespace Hinet.Web.Common
{
    public class FileProvider
    {
        public static void SetFilePathEditor(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var getPhysicPath = HostingEnvironment.MapPath(path);
                    if (!Directory.Exists(getPhysicPath))
                    {
                        Directory.CreateDirectory(getPhysicPath);
                    }
                    SessionManager.SetValue("PathFileUser", path);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}