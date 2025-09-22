using System.ComponentModel;

namespace Hinet.Service.Constant
{
    public class LogTypeItemConstant
    {
        //[DisplayName("Tài khoản cá nhân")]
        //public static string TaiKhoanCaNhan { get; set; } = "TaiKhoanCaNhan";
        //[DisplayName("Tài khoản thương nhân")]
        //public static string TaiKhoanThuongNhan { get; set; } = "TaiKhoanThuongNhan";
        [DisplayName("Tài khoản cá nhân")]
        public static string Personal { get; set; } = "Personal";

        [DisplayName("Tài khoản doanh nghiệp")]
        public static string Company { get; set; } = "Company";

        [DisplayName("Tài khoản tổ chức")]
        public static string Organization { get; set; } = "Organization";

        [DisplayName("Website")]
        public static string Website { get; set; } = "Website";

        [DisplayName("App")]
        public static string App { get; set; } = "App";

        //log content
        [DisplayName(" đề nghị chỉnh sửa/bổ sung thông tin của ")]
        public static int DeNghiChinhSuaBoSung { get; set; } = 6;

        [DisplayName(" đã duyệt hồ sơ của ")]
        public static int DaFuyetHoSo { get; set; } = 4;

        [DisplayName(" đã xác nhận hồ sơ của ")]
        public static int DaXacNhanHoSo { get; set; } = 5;

        [DisplayName(" đã từ chối hồ sơ của ")]
        public static int DaTuChoiHoSo { get; set; } = 3;

        [DisplayName(" đã nhận được đề nghị chỉnh sửa của ")]
        public static int DeNghiChinhSuaCua { get; set; } = 2;

        [DisplayName(" đã nhận được đề nghị chấm dứt đăng ký của ")]
        public static int DeNghiChamDutDangKyCua { get; set; } = 9;

        [DisplayName(" đã chấm dứt đăng ký của ")]
        public static int DaChamDutDangKyCua { get; set; } = 7;

        [DisplayName(" đã hủy đăng ký của ")]
        public static int DaHuyDangKyCua { get; set; } = 8;

        //[DisplayName(" đã nhận được đề nghị chấm dứt thông báo của ")]
        //public static int DaNhanDuocDuocDeNghiChamDutThongBaoCua { get; set; } = 9;
        //[DisplayName(" đã chấm dứt thông báo của ")]
        //public static int DaChamDutThongBaoCua { get; set; } = 10;
        //[DisplayName(" đã hủy thông báo của ")]
        //public static int DaHuyThongBaoCua { get; set; } = 11;
        [DisplayName(" đã yêu cầu gia hạn của ")]
        public static int DaYeuCauGiaHanCua { get; set; } = 11;

        [DisplayName("đã khóa hồ sơ của")]
        public static int DaKhoaHoSoCua { get; set; } = DaKhoaHoSoCua;

        [DisplayName("Đánh giá tin nhiệm")]
        public static string DanhGiaTinNhiem { get; set; } = "DanhGiaTinNhiem";

        [DisplayName("Danh sách Website được gắn biểu tượng tín nhiệm")]
        public static string DSWSGanBTTN { get; set; } = "DSWSGanBTTN";

        [DisplayName("Thông tin Website phản ánh")]
        public static string PhanAnhWSInfo { get; set; } = "PhanAnhWSInfo";

        [DisplayName("Thông tin Ứng dụng phản ánh")]
        public static string PhanAnhAppInfo { get; set; } = "PhanAnhAppInfo";

        [DisplayName("Website bị phản ánh")]
        public static string WSBiPhanAnh { get; set; } = "WSBiPhanAnh";

        [DisplayName("Ứng dụng bị phản ánh")]
        public static string AppBiPhanAnh { get; set; } = "AppBiPhanAnh";

        [DisplayName("Website vi phạm")]
        public static string WSViPham { get; set; } = "WSViPham";

        [DisplayName("Ứng dụng vi phạm")]
        public static string AppViPham { get; set; } = "AppViPham";

        [DisplayName("nhận xử lý hồ sơ")]
        public static string NhanXuLyHoSo { get; set; } = "NhanXuLyHoSo";

        [DisplayName("đã chấm dứt thông báo của")]
        public static int DaChamDutThongBao { get; set; } = DaChamDutThongBao;

        [DisplayName("đã hủy thông báo của")]
        public static int DaHuyThongBao { get; set; } = DaHuyThongBao;

        [DisplayName("yêu cầu gia hạn thông báo của")]
        public static int YeuCauGiaHanThongBao { get; set; } = YeuCauGiaHanThongBao;

        [DisplayName("nhận được yêu cầu gia hạn thông báo của")]
        public static int NhanDuocYeuCauGiaHanThongBao { get; set; } = NhanDuocYeuCauGiaHanThongBao;

        [DisplayName("đã đưa ra khỏi danh sách đánh giá tín nhiệm")]
        public static int DuaKhoiDanhSach { get; set; } = DuaKhoiDanhSach;

        [DisplayName("đã đưa vào danh sách đánh giá tín nhiệm")]
        public static int DuaVaoDanhSach { get; set; } = DuaVaoDanhSach;

        [DisplayName("Tiếp nhận xử lý hồ sơ")]
        public static string TiepNhanXuLyHoSo { get; set; } = "TiepNhanXuLyHoSo";

        //Notification.Message: chuyển hồ sơ của bạn cho từ BussinessUser sang BussinessUser khác
        [DisplayName(" chuyển hồ sơ của bạn cho ")]
        public static string ChuyenHoSoCuaBanCho { get; set; } = "ChuyenHoSoCuaBanCho";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý website của ")]
        public static string DaNhanXuLyWebsiteCuaBan { get; set; } = "DaNhanXuLyWebsiteCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý ứng dụng của ")]
        public static string DaNhanXuLyAppCuaBan { get; set; } = "DaNhanXuLyAppCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý tài khoản cá nhân của ")]
        public static string DaNhanXuLyPersonalCuaBan { get; set; } = "DaNhanXuLyPersonalCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý tài khoản doanh nghiệp của ")]
        public static string DaNhanXuLyCompanyCuaBan { get; set; } = "DaNhanXuLyCompanyCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý hồ sơ đánh giá tín nhiệm của ")]
        public static string DaNhanXuLyDanhGiaTinNhiemCuaBan { get; set; } = "DaNhanXuLyDanhGiaTinNhiemCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý hồ sơ sản phẩm của ")]
        public static string DaNhanXuLyProductCuaBan { get; set; } = "DaNhanXuLyProductCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý hồ sơ dịch vụ của ")]
        public static string DaNhanXuLyDichVuCuaBan { get; set; } = "DaNhanXuLyDichVuCuaBan";

        //Notification.Message: BussinessUser đã nhận xử lý hồ sơ của bạn
        [DisplayName(" đã nhận xử lý tài khoản tổ chức của bạn!")]
        public static string DaNhanXuLyOrganizationCuaBan { get; set; } = "DaNhanXuLyOrganizationCuaBan";

        [DisplayName(" đã gửi thông báo")]
        public static string EndUserBaoCao { get; set; } = "EndUserBaoCao";

        [DisplayName(" đã chuyển đổi hồ sơ đăng ký sang hồ sơ thông báo.")]
        public static string ChuyenDoiHoSoDangKySangHoSoThongBao { get; set; } = "ChuyenDoiHoSoDangKySangHoSoThongBao";

        [DisplayName(" đã chuyển đổi hồ sơ thông báo sang hồ sơ đăng ký")]
        public static string ChuyenDoiHoSoThongBaoSangHoSoDangKy { get; set; } = "ChuyenDoiHoSoThongBaoSangHoSoDangKy";

        [DisplayName("Sửa thông tin hồ sơ")]
        public static string SuaThongTinHoSo { get; set; } = "SuaThongTinHoSo";

        [DisplayName(" đã sửa thông tin hồ sơ đăng ký webiste cung cấp dịch vụ của ")]
        public static string SuaThongTinHoSoDangKyWebisteCCDV { get; set; } = "SuaThongTinHoSoDangKyWebisteCCDV";

        [DisplayName(" đã sửa thông tin hồ sơ thông báo webiste bán hàng của")]
        public static string SuaThongTinHoSoWebisteSaleNoti { get; set; } = "SuaThongTinHoSoWebisteSaleNoti";

        [DisplayName(" đã sửa thông tin hồ sơ đăng ký ứng dụng cung cấp dịch vụ của ")]
        public static string SuaThongTinHoSoDangKyAppCCDV { get; set; } = "SuaThongTinHoSoDangKyAppCCDV";

        [DisplayName(" đã sửa thông tin hồ sơ thông báo ứng dụng bán hàng của ")]
        public static string SuaThongTinHoSoAppSaleNoti { get; set; } = "SuaThongTinHoSoAppSaleNoti";

        [DisplayName(" đã sửa thông tin hồ sơ cá nhân của bạn")]
        public static string SuaThongTinHoSoPersonalInfo { get; set; } = "SuaThongTinHoSoPersonalInfo";

        [DisplayName(" đã sửa thông tin hồ sơ doanh nghiệp của bạn")]
        public static string SuaThongTinHoSoCompanyInfo { get; set; } = "SuaThongTinHoSoCompanyInfo";

        [DisplayName(" đã sửa thông tin hồ sơ tổ chức của bạn")]
        public static string SuaThongTinHoSoOrganizationInfo { get; set; } = "SuaThongTinHoSoOrganizationInfo";

        [DisplayName(" đã sửa thông tin hồ sơ đánh giá tín nhiệm của bạn")]
        public static string SuaThongTinHoSoDGTN { get; set; } = "SuaThongTinHoSoDGTN";

        [DisplayName(" đã sửa thông tin hồ sơ sản phẩm của bạn")]
        public static string SuaThongTinHoSoSanPham { get; set; } = "SuaThongTinHoSoSanPham";

        [DisplayName(" đã sửa thông tin hồ sơ dịch vụ của bạn")]
        public static string SuaThongTinHoSoDichVu { get; set; } = "SuaThongTinHoSoDichVu";

        [DisplayName("Cảnh báo thông tin hồ sơ Website")]
        public static string CanhBaoThongTinHoSoWebsite { get; set; } = "CanhBaoThongTinHoSoWebsite";

        [DisplayName(" đã cảnh báo thông tin hồ sơ website cung cấp dịch vụ của ")]
        public static string CanhBaoThongTinHoSoWebsiteCCDV { get; set; } = "CanhBaoThongTinHoSoWebsiteCCDV";

        [DisplayName(" đã cảnh báo thông tin hồ sơ website thông báo bán hàng của ")]
        public static string CanhBaoThongTinHoSoWebsiteSaleNoti { get; set; } = "CanhBaoThongTinHoSoWebsiteSaleNoti";

        [DisplayName("Cảnh báo thông tin hồ sơ Ứng dụng")]
        public static string CanhBaoThongTinHoSoApp { get; set; } = "CanhBaoThongTinHoSoApp";

        [DisplayName(" đã cảnh báo thông tin hồ sơ ứng dụng cung cấp dịch vụ của ")]
        public static string CanhBaoThongTinHoSoAppCCDV { get; set; } = "CanhBaoThongTinHoSoAppCCDV";

        [DisplayName(" đã cảnh báo thông tin hồ sơ ứng dụng thông báo bán hàng của ")]
        public static string CanhBaoThongTinHoSoAppSaleNoti { get; set; } = "CanhBaoThongTinHoSoAppSaleNoti";

        [DisplayName("Vi phạm thông tin hồ sơ Website")]
        public static string ViPhamThongTinHoSoWebsite { get; set; } = "ViPhamThongTinHoSoWebsite";

        [DisplayName(" đã chuyển hồ sơ website cung cấp dịch vụ của ")]
        public static string DaChuyenWebsiteCCDV { get; set; } = "DaChuyenWebsiteCCDV";

        [DisplayName(" đã chuyển hồ sơ website thông báo bán hàng của ")]
        public static string DaChuyenWebsiteSaleNoti { get; set; } = "DaChuyenWebsiteSaleNoti";

        [DisplayName("Vi phạm thông tin hồ sơ Ứng dụng")]
        public static string ViPhamThongTinHoSoApp { get; set; } = "ViPhamThongTinHoSoApp";

        [DisplayName(" đã chuyển hồ sơ ứng dụng cung cấp dịch vụ của ")]
        public static string DaChuyenAppCCDV { get; set; } = "DaChuyenAppCCDV";

        [DisplayName(" đã chuyển hồ sơ ứng dụng thông báo bán hàng của ")]
        public static string DaChuyenAppSaleNoti { get; set; } = "DaChuyenAppSaleNoti";

        [DisplayName(" gửi đợt báo cáo tới bạn")]
        public static string GuiDotBaoCaoToiBan { get; set; } = "GuiDotBaoCaoToiBan";

        [DisplayName(" đã phê duyệt kê khai đợt báo cáo của bạn")]
        public static string DaPheDuyetKeKhaiDotBaoCaoCuaBan { get; set; } = "DaPheDuyetKeKhaiDotBaoCaoCuaBan";

        [DisplayName(" đã yêu cầu bạn bổ sung kê khai đợt báo cáo của bạn")]
        public static string DaYeuCauBanBoSungKeKhaiDotBaoCaoCuaBan { get; set; } = "DaYeuCauBanBoSungKeKhaiDotBaoCaoCuaBan";

        [DisplayName(" đã từ chối kê khai đợt báo cáo của bạn")]
        public static string DaTuChoiKeKhaiDotBaoCaoCuaBan { get; set; } = "DaTuChoiKeKhaiDotBaoCaoCuaBan";

        [DisplayName(" đã chỉnh sửa kê khai đợt báo cáo của bạn")]
        public static string DaChinhSuaKeKhaiDotBaoCaoCuaBan { get; set; } = "DaChinhSuaKeKhaiDotBaoCaoCuaBan";

        [DisplayName(" tạo hồ sơ cá nhân")]
        public static string TaoHoSoCaNhan { get; set; } = "TaoHoSoCaNhan";
    }

    public class LogTypeItemConstantPersonal
    {
        //log content hố sơ cá nhân
        [DisplayName(" đã chuyển trạng thái hồ sơ thành chờ duyệt ")]
        public static int ChoDuyet { get; set; } = 0;

        [DisplayName(" yêu cầu chỉnh sửa/bổ sung thông tin hồ sơ ")]
        public static int DeNghiChinhSuaBoSung { get; set; } = 2;

        [DisplayName(" đã duyệt hồ sơ ")]
        public static int DaFuyetHoSo { get; set; } = 1;

        [DisplayName(" đã từ chối hồ sơ ")]
        public static int DaTuChoiHoSo { get; set; } = 4;

        [DisplayName(" đã nhận được đề nghị chỉnh sửa hồ sơ")]
        public static int DeNghiChinhSuaCua { get; set; } = 3;

        [DisplayName(" đã khóa hồ sơ ")]
        public static int DaKhoaHoSoCua { get; set; } = 5;
    }

    public class LogTypeItemConstantCompany
    {
        //log content hố sơ thương nhân
        [DisplayName(" yêu cầu chỉnh sửa/bổ sung thông tin hồ sơ ")]
        public static int DeNghiChinhSuaBoSung { get; set; } = 2;

        [DisplayName(" đã duyệt hồ sơ ")]
        public static int DaFuyetHoSo { get; set; } = 1;

        [DisplayName(" đã từ chối hồ sơ ")]
        public static int DaTuChoiHoSo { get; set; } = 4;

        [DisplayName(" đã nhận được đề nghị chỉnh sửa hồ sơ ")]
        public static int DeNghiChinhSuaCua { get; set; } = 3;

        [DisplayName(" đã khóa hồ sơ ")]
        public static int DaKhoaHoSoCua { get; set; } = 5;

        [DisplayName(" đã nhận được đề nghị chấm dứt hồ sơ ")]
        public static int DeNghiChamDut { get; set; } = 6;

        [DisplayName(" đã chấm dứt hồ sơ ")]
        public static int DaChamDut { get; set; } = 7;

        [DisplayName(" đã xác nhân hồ sơ ")]
        public static int DaXacNhan { get; set; } = 8;
    }

    public class LogTypeItemConstantOrganization
    {
        //log content hố sơ tổ chức
        [DisplayName(" đề nghị chỉnh sửa/bổ sung thông tin hồ sơ ")]
        public static int DeNghiChinhSuaBoSung { get; set; } = 2;

        [DisplayName(" đã duyệt hồ sơ ")]
        public static int DaFuyetHoSo { get; set; } = 1;

        [DisplayName(" đã từ chối hồ sơ ")]
        public static int DaTuChoiHoSo { get; set; } = 4;

        [DisplayName(" đã nhận được đề nghị chỉnh sửa hồ sơ ")]
        public static int DeNghiChinhSuaCua { get; set; } = 3;

        [DisplayName(" đã khóa hồ sơ ")]
        public static int DaKhoaHoSoCua { get; set; } = 5;

        [DisplayName(" đã nhận được đề nghị chấm dứt hồ sơ ")]
        public static int DeNghiChamDut { get; set; } = 6;

        [DisplayName(" đã chấm dứt hồ sơ ")]
        public static int DaChamDut { get; set; } = 7;

        [DisplayName(" đã xác nhân hồ sơ ")]
        public static int DaXacNhan { get; set; } = 8;
    }

    public class LogTypeItemConstantDanhGiaTinNhiem
    {
        //log content hố sơ đánh giá tím nhiệm
        [DisplayName(" đề nghị chỉnh sửa/bổ sung thông tin của ")]
        public static int DeNghiChinhSuaBoSung { get; set; } = 6;

        [DisplayName(" đã duyệt hồ sơ của ")]
        public static int DaFuyetHoSo { get; set; } = 4;

        [DisplayName(" đã xác nhận hồ sơ của ")]
        public static int DaXacNhanHoSo { get; set; } = 5;

        [DisplayName(" đã từ chối hồ sơ của ")]
        public static int DaTuChoiHoSo { get; set; } = 3;

        [DisplayName(" đã nhận được đề nghị chỉnh sửa của ")]
        public static int DeNghiChinhSuaCua { get; set; } = 2;

        [DisplayName(" đã nhận được đề nghị chấm dứt đăng ký của ")]
        public static int DeNghiChamDutDangKyCua { get; set; } = 9;

        [DisplayName(" đã chấm dứt đăng ký của ")]
        public static int DaChamDutDangKyCua { get; set; } = 7;

        [DisplayName(" đã hủy đăng ký của ")]
        public static int DaHuyDangKyCua { get; set; } = 8;
    }

    public class LogTypeItemConstantProduct
    {
        //log content hố sơ đánh giá tím nhiệm
        [DisplayName(" tạo mới hồ sơ sản phẩm ")]
        public static string TaoMoiHoSoSanPham { get; set; } = "TaoMoiHoSoSanPham";

        [DisplayName(" tạo mới hồ sơ dịch vụ ")]
        public static string TaoMoiHoSoDichVu { get; set; } = "TaoMoiHoSoDichVu";

        //log content hố sơ đánh giá tím nhiệm
        [DisplayName(" đề nghị chỉnh sửa/bổ sung thông tin của ")]
        public static int DeNghiChinhSuaBoSung { get; set; } = 6;

        [DisplayName(" đã duyệt hồ sơ của ")]
        public static int DaDuyetHoSo { get; set; } = 4;

        [DisplayName(" đã xác nhận hồ sơ của ")]
        public static int DaXacNhanHoSo { get; set; } = 5;

        [DisplayName(" đã từ chối hồ sơ của ")]
        public static int DaTuChoiHoSo { get; set; } = 3;

        [DisplayName(" đã nhận được đề nghị chỉnh sửa của ")]
        public static int DeNghiChinhSuaCua { get; set; } = 2;

        [DisplayName(" đã nhận được đề nghị chấm dứt đăng ký của ")]
        public static int DeNghiChamDutDangKyCua { get; set; } = 9;

        [DisplayName(" đã chấm dứt đăng ký của ")]
        public static int DaChamDutDangKyCua { get; set; } = 7;

        [DisplayName(" đã hủy đăng ký của ")]
        public static int DaHuyDangKyCua { get; set; } = 8;
    }

    public class LogTypeItemConstantKeHoach
    {
        [DisplayName(" đã chuyển kế hoạch công việc thành mới tạo của ")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName(" đã chuyển kế hoạch công việc thành chờ duyệt của ")]
        public static string ChoDuyet { get; set; } = "ChoDuyet";

        [DisplayName(" đã duyệt kế hoạch công việc của ")]
        public static string DaDuyet { get; set; } = "DaDuyet";

        [DisplayName(" đã từ chối kế hoạch công việc của ")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName(" đề nghị chỉnh sửa/bổ sung kế hoạch công việc của ")]
        public static string YeuCauChinhSua { get; set; } = "YeuCauChinhSua";
    }

    public class LogTypeItemConstantDanhMucCongViec
    {
        [DisplayName(" đã chuyển danh mục công việc thành đang thực hiện ")]
        public static string DangThucHien { get; set; } = "DangThucHien";

        [DisplayName(" đã chuyển danh mục công việc thành đã hoàn thành ")]
        public static string DaHoanThanh { get; set; } = "DaHoanThanh";

        [DisplayName(" đã chuyển danh mục công việc thành dừng thực hiện ")]
        public static string DungThucHien { get; set; } = "DungThucHien";

        [DisplayName(" đã chuyển danh mục công việc thành chưa thực hiện ")]
        public static string ChuaThucHien { get; set; } = "ChuaThucHien";
    }

    public class LogTypeItemConstantNhuanBut
    {
        [DisplayName(" đã phê duyệt yêu cầu nhuận bút ")]
        public static string DaPheDuyet { get; set; } = "PheDuyet";

        [DisplayName(" đã lập phiếu chi cho yêu cầu nhuận bút ")]
        public static string DaChiTra { get; set; } = "DaChiTra";

        [DisplayName(" đã chấm điểm ")]
        public static string DaChamDiem { get; set; } = "DaChamDiem";
    }

    public class LogTypeItemConstantPlan
    {
        [DisplayName(" đã chuyển kế hoạch thành mới tạo ")]
        public static string MoiTao { get; set; } = "MoiTao";

        [DisplayName(" đã chuyển kế hoạch thành chờ duyệt ")]
        public static string ChoDuyet { get; set; } = "ChoDuyet";

        [DisplayName(" đã duyệt kế hoạch ")]
        public static string DaPheDuyet { get; set; } = "DaPheDuyet";

        [DisplayName(" đã từ chối kế hoạch ")]
        public static string TuChoi { get; set; } = "TuChoi";

        [DisplayName(" đề nghị chỉnh sửa/bổ sung kế hoạch ")]
        public static string YeuCauChinhSua { get; set; } = "YeuCauChinhSua";
    }

    public class LogTypeItemConstantHopDong
    {
        [DisplayName(" đã chuyển hợp đồng thành bản thảo ")]
        public static string BanThao { get; set; } = "BanThao";

        [DisplayName(" đã ký hợp đồng ")]
        public static string DaKy { get; set; } = "DaKy";

        [DisplayName(" đã hủy hợp đồng ")]
        public static string DaHuy { get; set; } = "DaHuy";

        [DisplayName(" đã kết thúc hợp đồng ")]
        public static string DaKetThuc { get; set; } = "DaKetThuc";
    }
}