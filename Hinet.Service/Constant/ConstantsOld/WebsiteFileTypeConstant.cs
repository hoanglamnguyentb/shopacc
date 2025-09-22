using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class WebsiteFileTypeConstant
    {
        //Các định dạng file cho phép: .doc,.docx,.xls,.xlsx,.jpg,.gif,.png,.pdf
        [DisplayName("Bản scan Đơn đăng ký cung cấp dịch vụ TMĐT.")]
        public static int Loai1 { get; set; } = 1;

        [DisplayName("Bản scan Giấy chứng nhận đăng ký kinh doanh (hoặc tài liệu tương đương).")]
        public static int Loai2 { get; set; } = 2;

        [DisplayName("Đề án hoạt động.")]
        public static int Loai3 { get; set; } = 3;

        [DisplayName("Quy chế quản lý hoạt động của website cung cấp dịch vụ TMĐT (Có thể up bản mềm).")]
        public static int Loai4 { get; set; } = 4;

        [DisplayName("Bản scan Mẫu hợp đồng cung cấp dịch vụ của website TMĐT với đối tác tham gia mua bán hàng hóa, cung ứng dịch vụ trên website đó.")]
        public static int Loai5 { get; set; } = 5;

        [DisplayName("Các điều kiện giao dịch chung áp dụng cho hoạt động mua bán hàng hóa, cung ứng dịch vụ trên website (nếu có). ")]
        public static int Loai6 { get; set; } = 6;
    }

    public class websiteNotiFileTypeConstant
    {
        [DisplayName("Bản scan Giấy chứng nhận đăng ký kinh doanh/ Giấy chứng nhận đầu tư/ Giấy phép đầu tư (đối với doanh nghiệp)")]
        public static int Loai1 { get; set; } = 1;

        [DisplayName("Bản scan Quyết định thành lập (đối với tổ chức)")]
        public static int Loai2 { get; set; } = 2;

        [DisplayName("Bản scan Chứng minh thư nhân dân (đối với cá nhân)")]
        public static int Loai3 { get; set; } = 3;
    }

    public class AppFileTypeConstant
    {
        //Các định dạng file cho phép: .doc,.docx,.xls,.xlsx,.jpg,.gif,.png,.pdf
        [DisplayName("Bản scan Đơn đăng ký cung cấp dịch vụ TMĐT.")]
        public static int Loai1 { get; set; } = 1;

        [DisplayName("Bản scan Giấy chứng nhận đăng ký kinh doanh (hoặc tài liệu tương đương).")]
        public static int Loai2 { get; set; } = 2;

        [DisplayName("Đề án hoạt động.")]
        public static int Loai3 { get; set; } = 3;

        [DisplayName("Quy chế quản lý hoạt động của ứng dụng cung cấp dịch vụ TMĐT (Có thể up bản mềm).")]
        public static int Loai4 { get; set; } = 4;

        [DisplayName("Bản scan Mẫu hợp đồng cung cấp dịch vụ của ứng dụng TMĐT với đối tác tham gia mua bán hàng hóa, cung ứng dịch vụ trên ứng dụng đó.")]
        public static int Loai5 { get; set; } = 5;

        [DisplayName("Các điều kiện giao dịch chung áp dụng cho hoạt động mua bán hàng hóa, cung ứng dịch vụ trên ứng dụng (nếu có). ")]
        public static int Loai6 { get; set; } = 6;
    }

    public class AppNotiFileTypeConstant
    {
        [DisplayName("Bản scan Giấy chứng nhận đăng ký kinh doanh/ Giấy chứng nhận đầu tư/ Giấy phép đầu tư (đối với doanh nghiệp)")]
        public static int Loai1 { get; set; } = 1;

        [DisplayName("Bản scan Quyết định thành lập (đối với tổ chức)")]
        public static int Loai2 { get; set; } = 2;

        [DisplayName("Bản scan Chứng minh thư nhân dân (đối với cá nhân)")]
        public static int Loai3 { get; set; } = 3;
    }

    public class ProductFileTypeConstant
    {
    }

    public class DGTNFileTypeConstant
    {
        [DisplayName("Bản scan đơn đăng ký hoạt động đáng giá tín nhiệm website TMĐT")]
        public static int Loai1 { get; set; } = 1;

        [DisplayName("Bản sao có chứng thực QĐTL (đối với Tổ chức), giấy chứng nhận ĐKKD, giấy chứng nhận đầu tư, hoặc giấy phếp đầu tư (đối với Thương nhân)")]
        public static int Loai2 { get; set; } = 2;

        [DisplayName("Đề án hoạt động đánh giá tín nhiệm website TMĐT")]
        public static int Loai3 { get; set; } = 3;
    }
}