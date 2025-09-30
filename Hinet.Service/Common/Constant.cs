namespace Hinet.Service.Common
{
    public class Constant
    {
        public enum WORKFLOW_SHOWTYPE
        {
            USER,
            ROLE
        }

        public enum EDIT_OBJECT_ENUM
        {
            FAIL,
            SUCCESS
        }

        public enum WORKFLOW_REQUEST_EDIT
        {
            NEW,
            PENDING,
            RESOLVED
        }

        public enum FILE_TYPE_ENUM
        {
            IMAGE,
            PDF,
            OTHER,
            WORD
        }

        public class MODULE_INFO
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Link { get; set; }
        }

        public class MODULE_CONSTANT
        {
            public static string TAILIEUDINHKEM_TRAMBTS = "TAILIEUDINHKEM_TRAMBTS";
            public static string LISTEXTENSION = ".xlsx,.docx,.doc,.png,.jpeg,.jpg,.pdf";
            public static string TAILIEUDINHKEM_GIAYPHEPTANSO = "TAILIEUDINHKEM_GIAYPHEPTANSO";
            public static string TAILIEUDINHKEM_GIAYPHEPKIEMDINH = "TAILIEUDINHKEM_GIAYPHEPKIEMDINH";
            public static string TAILIEUDINHKEM_HATANGCAPMANG = "TAILIEUDINHKEM_HATANGCAPMANG";

            //Constant abten
            public static string VANBANDINHKEM_ANTEN = "VANBANDINHKEM_ANTEN";
            public static string GIAYPHEPDINHKEM_ANTEN = "GIAYPHEPDINHKEM_ANTEN";
            public static string GIAYKIEMDINH_ANTEN = "GIAYKIEMDINH_ANTEN";
            public static string VANBANDINHKEM_BUUCHINH = "VANBANDINHKEM_BUUCHINH";
            public static string TAILIEUDINHKIEM_TUYENTRUYENDANVIBA = "TAILIEUDINHKIEM_TUYENTRUYENDANVIBA";
            public static string QUYHOACHANTEN = "QUYHOACHANTEN";


            //Constant duongg dan thu

            public static string TAILIEUDINHKEMDUONGDANTHU = "TAILIEUDINHKEMDUONGDANTHU";

            //Constant tenbang vungcamtrienkhaibts
            public static string VUNGCAMTRIENKHAIBTS { get; set; } = "VUNGCAMTRIENKHAIBTS";

            public static string LOAPHATTHANHDINHKEM { get; set; } = "LOAPHATTHANHDINHKEM";

            public static string DIEMCCINTERNETVATROCHOIGIAYPHEPDK { get; set; } = "DIEMCCINTERNETVATROCHOIGIAYPHEPDK";

            //Constant doanhnghiep
            public static string VANBANXACNHAN_DNBC = "VANBANXACNHAN_DNBC";
            public static string FILEDINHKEM = "FILEDINHKEM";


            public static string TAILIEUDINHKEMDIEMCCDVVT = "TAILIEUDINHKEMDIEMCCDVVT";
            public static string TAILIEUDINHKEMDIEMCCINTERNET = "TAILIEUDINHKEMDIEMCCINTERNET";

            // Constant biểu mẫu báo cáo
            public static string MADIABAN_TONGCONG= "00";


        }

        public class CAUHINH_HETHONG_CONSTANT
        {
            public const string TEN_HETHONG = "TEN_HETHONG";
            public const string CAUHINH_HETHONG = "CAUHINH_HETHONG";
            public const string TEN_TINH = "TEN_TINH";
            public const string FAX = "FAX";
            public const string EMAIL = "EMAIL";
            public const string DIENTHOAI = "DIENTHOAI";
            public const string TEN_DONVI = "TEN_DONVI";
            public const string DIACHI_DONVI = "DIACHI_DONVI";
            public const string NAM_SANXUAT = "NAM_SANXUAT";
            public const string DIACHI_HETHONG = "DIACHI_HETHONG";
            public const string MA_TINH = "MA_TINH";
        }
    }
}