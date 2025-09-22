using System.Web;

namespace Hinet.Web
{
    public class SessionManager
    {
        public const string USER_INFO = "UserInfo";
        public const string COMPANY_INFOMATION = "CompanyInfomationDto";
        public const string LIST_PERMISSTION = "LIST_PERMISSTION";
        public const string HOST_COMPLAIN = "HOST_COMPLAIN";
        public const string HOST_REPORT = "HOST_REPORT";
        public const string USER_ROLE = "USER_ROLE";
        public const string TRANGTHAI_TINBAI = "TRANGTHAI_TINBAI";
        public const string TRANGTHAI_ANPHAM = "TRANGTHAI_ANPHAM";
        public const string LOAI_HOP_DONG = "LOAI_HOP_DONG";

        public static void SetValue(string Key, object Value)
        {
            HttpContext context = HttpContext.Current;
            context.Session[Key] = Value;
        }

        public static object GetValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            return context.Session[Key];
        }

        public static T GetValue<T>(string Key)
        {
            HttpContext context = HttpContext.Current;
            return (T)context.Session[Key];
        }

        public static void Remove(string Key)
        {
            HttpContext context = HttpContext.Current;
            context.Session.Remove(Key);
        }

        public static void Clear()
        {
            HttpContext context = HttpContext.Current;
            context.Session.RemoveAll();
        }

        public static bool HasValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            return context.Session[Key] != null;
        }

        public static object GetUserInfo()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[USER_INFO];
        }

        public static object GetCompanyOWNInfo()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[COMPANY_INFOMATION];
        }

        public static object GetHostComplain()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[HOST_COMPLAIN];
        }

        public static object GetHostReport()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[HOST_REPORT];
        }

        public static object GetListPermistion()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[LIST_PERMISSTION];
        }

        public static object GetListTrangThaiTinBai()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[TRANGTHAI_TINBAI];
        }

        public static object GetListTrangThaiAnPham()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[TRANGTHAI_ANPHAM];
        }

        public static object GetListLoaiHopDong()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[LOAI_HOP_DONG];
        }
    }
}