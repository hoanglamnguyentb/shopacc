using System.ComponentModel;

namespace Hinet
{
    public class PermissionCodeConst
    {
        #region Quản trị hệ thống

        [DisplayName("Dashboard")]
        public static string Dashboard { get; set; } = "Dashboard";

        [DisplayName("Quản lý ô chức năng dashboard")]
        public static string QLDashboardItem { get; set; } = "QLDashboardItem";

        [DisplayName("Quản  lý tài khoản")]
        public static string QLTaiKhoan { get; set; } = "QLTaiKhoan";

        [DisplayName("Quản lý chức năng")]
        public static string QLChucNang { get; set; } = "QLChucNang";

        [DisplayName("Hồ sơ ứng viên")]
        public static string HsUngVien { get; set; } = "HsUngVien";

        [DisplayName("Quản lý vai trò")]
        public static string QLVaiTro { get; set; } = "QLVaiTro";

        [DisplayName("Quản lý danh mục")]
        public static string QLDanhMuc { get; set; } = "QLDanhMuc";

        [DisplayName("Quản lý Khoa phòng")]
        public static string QLPhongBan { get; set; } = "QLPhongBan";

        [DisplayName("Quản lý cấu hình chung")]
        public static string QLCauHinhChung { get; set; } = "QLCauHinhChung";

        [DisplayName("Quản lý Văn bản pháp quy")]
        public static string LegalDocument_index { get; set; } = "LegalDocument_index";

        [DisplayName("Quản lý xuất Excel Văn bản pháp quy")]
        public static string LegalDocument_Excel { get; set; } = "LegalDocument_Excel";

        [DisplayName("Quản lý Luồng xử lý")]
        public static string WF_Stream_index { get; set; } = "WF_Stream_index";

        [DisplayName("Quản lý module")]
        public static string WF_Module_index { get; set; } = "WF_Module_index";

        [DisplayName("Quản lý Tỉnh/Thành phố")]
        public static string TINH_index { get; set; } = "TINH_index";

        [DisplayName("Quản lý Quận/Huyện")]
        public static string HUYEN_index { get; set; } = "HUYEN_index";

        [DisplayName("Quản lý Xã/Phường")]
        public static string XA_index { get; set; } = "XA_index";

        [DisplayName("Quản lý Dữ liệu nhật ký")]
        public static string History_index { get; set; } = "History_index";

        [DisplayName("Quản lý Hình ảnh Slide")]
        public static string SlideImage_index { get; set; } = "SlideImage_index";

        [DisplayName("Quản lý Loại ứng dụng")]
        public static string AppInfoType_index { get; set; } = "AppInfoType_index";

        [DisplayName("Quản lý thông tin hướng dẫn")]
        public static string QLTTHD_index { get; set; } = "QLTTHD_index";

        [DisplayName("Quản lý banner trang chủ")]
        public static string QuanLyCauHinhHeThong_index { get; set; } = "QuanLyCauHinhHeThong_index";

        [DisplayName("Quản lý Mẫu nội dung Email")]
        public static string EmailTemplate_index { get; set; } = "EmailTemplate_index";

        [DisplayName("Quản lý Mẫu trả lời")]
        public static string MauTraLoi_index { get; set; } = "MauTraLoi_index";

        [DisplayName("Quản lý Hạn xử lý")]
        public static string HanXuLy_index { get; set; } = "HanXuLy_index";

        /// <summary>
        /// Quản lý phân quyền chung của tất cả hệ thống
        /// </summary>
        [DisplayName("Quản lý thông tin chung của hệ thống")]
        public static string QLThongTinChungHeThong { get; set; } = "QLThongTinChungHeThong";

        [DisplayName("Quản lý xem thông tin cơ bản của hồ sơ")]
        public static string QLXemThongTinCoBanCuaHoSo { get; set; } = "QLXemThongTinCoBanCuaHoSo";

        [DisplayName("Quản lý nút xử lý hồ sơ của Admin")]
        public static string Super_admin { get; set; } = "Super_admin";

        [DisplayName("Quản lý nút sửa nhanh thông tin Dashboard")]
        public static string EditInfoDashBoard { get; set; } = "EditInfoDashBoard";

        [DisplayName("Quản lý nút thêm thiết lập menu Module")]
        public static string QLButtonAddModule { get; set; } = "QLButtonAddModule";

        [DisplayName("Đếm tổng số ")]
        public static string DEM_TONG_SO { get; set; } = "DEM_TONG_SO";

        [DisplayName("Thông báo của bạn")]
        public static string THONG_BAO_CUA_BAN { get; set; } = "THONG_BAO_CUA_BAN";

        #endregion Quản trị hệ thống

        #region Tòa soạn

        [DisplayName("Quản lý Nhân sự")]
        public static string LyLich2C_index { get; set; } = "LyLich2C_index";

        [DisplayName("Quản lý Lý lịch vs Đào tạo bồi dưỡng")]
        public static string LyLichVsDaoTaoBoiDuong_index { get; set; } = "LyLichVsDaoTaoBoiDuong_index";

        [DisplayName("Quản lý Lý lịch vs quá trình công tác")]
        public static string LyLichVsQuaTrinhCongTac_index { get; set; } = "LyLichVsQuaTrinhCongTac_index";

        [DisplayName("Quản lý Lý lịch vs Quá trình lương")]
        public static string LyLichVsQuaTrinhLuong_index { get; set; } = "LyLichVsQuaTrinhLuong_index";

        [DisplayName("Quản lý Lịch công tác")]
        public static string LichCongTac_index { get; set; } = "LichCongTac_index";

        [DisplayName("Quản lý Quan hệ gia đình")]
        public static string LyLichVsQuanHeGiaDinh_index { get; set; } = "LyLichVsQuanHeGiaDinh_index";

        #endregion Tòa soạn

        [DisplayName("Quản lý Thông tin chấm công")]
        public static string ThongTinChamCong_index { get; set; } = "ThongTinChamCong_index";

        [DisplayName("Quản lý Tiêu chi")]
        public static string Payment_index { get; set; } = "Payment_index";

        [DisplayName("Quản lý tiến trình công việc")]
        public static string QlTienTrinhCongViec_index { get; set; } = "QlTienTrinhCongViec_index";

        [DisplayName("Quản lý Layout")]
        public static string QuanLyLayout_index { get; set; } = "QuanLyLayout_index";

        [DisplayName("Quản lý Thôi Việc")]
        public static string QLThoiViec_index { get; set; } = "QLThoiViec_index";

        [DisplayName("Quản lý Điều kiện xét năng lương")]
        public static string DieuKienXetNangLuongTX_index { get; set; } = "DieuKienXetNangLuongTX_index";

        [DisplayName("Quản lý tin bài")]
        public static string QLTinBai_index { get; set; } = "QLTinBai_index";

        [DisplayName("Quản lý Danh mục công việc")]
        public static string BIDanhMucCongViec_index { get; set; } = "BIDanhMucCongViec_index";

        [DisplayName("Quản lý Danh mục ấn phẩm")]
        public static string DanhmucAnpham_index { get; set; } = "DanhmucAnpham_index";

        [DisplayName("Quản lý Chuyên mục")]
        public static string QLChuyenMuc_index { get; set; } = "QLChuyenMuc_index";

        [DisplayName("Quản lý Quản Các Lý Chức Vụ Trong Đảng")]
        public static string QLCacChucVuTrongDang_index { get; set; } = "QLCacChucVuTrongDang_index";

        [DisplayName("Quản lý Các Chúc Vụ Đoàn Thể")]
        public static string QLCacChucVuDoanThe_index { get; set; } = "QLCacChucVuDoanThe_index";

        [DisplayName("Quản lý Đảm Nhiệm Chức Vụ Đoàn Thể")]
        public static string QLDamNhiemChucVuDoanThe_index { get; set; } = "QLDamNhiemChucVuDoanThe_index";

        [DisplayName("Quản lý Nghiên Cứu Khoa Học")]
        public static string QLNghienCuuKhoaHoc_index { get; set; } = "QLNghienCuuKhoaHoc_index";

        [DisplayName("Quản lý Nhân Sự Đi Học")]
        public static string QLNhanSuDiHoc_index { get; set; } = "QLNhanSuDiHoc_index";

        [DisplayName("Quản lý Lịch Trực")]
        public static string QLLichTruc_index { get; set; } = "QLLichTruc_index";

        [DisplayName("Quản lý Chức Vụ Trong Đảng")]
        public static string QLChucVuTrongDang_index { get; set; } = "QLChucVuTrongDang_index";

        [DisplayName("Quản lý bút danh")]
        public static string QLButDanh_index { get; set; } = "QLButDanh_index";

        [DisplayName("Quản lý Lịch trực Vs Ngày trực")]
        public static string LichTrucVsNgayTruc_index { get; set; } = "LichTrucVsNgayTruc_index";

        #region Quản lý công việc

        [DisplayName("Quản lý công việc")]
        public static string QLWork_index { get; set; } = "QLWork_index";

        #endregion Quản lý công việc

        #region Quản lý kế hoạch

        [DisplayName("Quản lý kế hoạch tin bài")]
        public static string QLPlan_index { get; set; } = "QLPlan_index";

        [DisplayName("Kế hoạch tin bài mới tạo")]
        public static string QLPlan_MoiTao { get; set; } = "QLPlan_MoiTao";

        [DisplayName("Kế hoạch tin bài chờ duyệt")]
        public static string QLPlan_ChoDuyet { get; set; } = "QLPlan_ChoDuyet";

        [DisplayName("Kế hoạch tin bài bị yêu cầu chỉnh sửa")]
        public static string QLPlan_YeuCauChinhSua { get; set; } = "QLPlan_YeuCauChinhSua";

        [DisplayName("Kế hoạch tin bài đã phê duyệt")]
        public static string QLPlan_DaPheDuyet { get; set; } = "QLPlan_DaPheDuyet";

        [DisplayName("Kế hoạch tin bài bị từ chối")]
        public static string QLPlan_TuChoi { get; set; } = "QLPlan_TuChoi";

        [DisplayName("Quyền sửa đổi kế hoạch tin bài")]
        public static string QLPlan_Edit { get; set; } = "QLPlan_Edit";

        [DisplayName("Quyền xóa kế hoạch tin bài")]
        public static string QLPlan_Delete { get; set; } = "QLPlan_Delete";

        [DisplayName("Quyền tạo mới kế hoạch tin bài")]
        public static string QLPlan_Create { get; set; } = "QLPlan_Create";

        [DisplayName("Quyền đăng ký tin bài")]
        public static string QLPlan_CreateWork { get; set; } = "QLPlan_CreateWork";

        [DisplayName("Quyền đăng ký tin bài trưởng trang")]
        public static string QLPlan_CreateWorkTP { get; set; } = "QLPlan_CreateWorkTP";

        [DisplayName("Quyền xem lịch sử thay đổi kế hoạch tin bài")]
        public static string QLPlan_Detail { get; set; } = "QLPlan_Detail";

        [DisplayName("Quyền phê duyệt tin bài dự kiến")]
        public static string QLWork_PheDuyetTinBai { get; set; } = "QLWork_PheDuyetTinBai";

        [DisplayName("Quyền phân công tin bài dự kiến")]
        public static string QLPlan_PhanCongWork { get; set; } = "QLPlan_PhanCongWork";

        [DisplayName("Quyền nhận thông báo phê duyệt tin bài dự kiến")]
        public static string QLPlan_NotiPheDuyetTinBai { get; set; } = "QLPlan_NotiPheDuyetTinBai";

        #endregion Quản lý kế hoạch

        #region Quản lý kế hoạch báo điện tử

        [DisplayName("Quản lý kế hoạch tin bài báo điện tử")]
        public static string QLPlanDienTu_index { get; set; } = "QLPlanDienTu_index";

        [DisplayName("Quyền sửa đổi kế hoạch tin bài báo điện tử")]
        public static string QLPlanDienTu_Edit { get; set; } = "QLPlanDienTu_Edit";

        [DisplayName("Quyền xóa kế hoạch tin bài báo điện tử")]
        public static string QLPlanDienTu_Delete { get; set; } = "QLPlanDienTu_Delete";

        [DisplayName("Quyền tạo mới kế hoạch tin bài báo điện tử")]
        public static string QLPlanDienTu_Create { get; set; } = "QLPlanDienTu_Create";

        [DisplayName("Quyền xem lịch sử thay đổi kế hoạch tin bài báo điện tử")]
        public static string QLPlanDienTu_Detail { get; set; } = "QLPlanDienTu_Detail";

        [DisplayName("Quyền phân công tin bài báo điện tử dự kiến")]
        public static string QLPlanDienTu_PhanCongWork { get; set; } = "QLPlanDienTu_PhanCongWork";

        [DisplayName("Quyền nhận thông báo phê duyệt tin bài báo điện tử dự kiến")]
        public static string QLPlanDienTu_NotiPheDuyetTinBai { get; set; } = "QLPlanDienTu_NotiPheDuyetTinBai";

        [DisplayName("Quyền phê duyệt kế hoạch tin bài báo điện tử dự kiến")]
        public static string QLPlanDienTu_PheDuyetTinBai { get; set; } = "QLPlanDienTu_PheDuyetTinBai";

        [DisplayName("Lịch đăng ký kế hoạch tin bài báo điện tử")]
        public static string QLPlanDienTu_Lich { get; set; } = "QLPlanDienTu_Lich";

        #endregion Quản lý kế hoạch báo điện tử

        #region

        [DisplayName("Quản lý tất cả tin bài dự kiến")]
        public static string QLWork_CurrentCreated { get; set; } = "QLWork_CurrentCreated";

        #endregion

        #region Quản lý lịch trực

        [DisplayName("Quyền duyệt lịch trực")]
        public static string DuyetLichTruc { get; set; } = "DuyetLichTruc";

        [DisplayName("Quyền hủy lịch trực")]
        public static string HuyLichTruc { get; set; } = "HuyLichTruc";

        [DisplayName("Quyền sửa lịch trực")]
        public static string QLLichTruc_edit { get; set; } = "QLLichTruc_edit";

        [DisplayName("Quyền xóa lịch trực")]
        public static string QLLichTruc_delete { get; set; } = "QLLichTruc_delete";

        [DisplayName("Quyền phân công")]
        public static string QLLichTruc_phancong { get; set; } = "QLLichTruc_phancong";

        #endregion

        #region Quản lý lịch trực DienTu

        [DisplayName("Quyền duyệt lịch trực")]
        public static string DuyetLichTrucDienTu { get; set; } = "DuyetLichTrucDienTu";

        [DisplayName("Quyền hủy lịch trực")]
        public static string HuyLichTrucDienTu { get; set; } = "HuyLichTrucDienTu";

        [DisplayName("Quyền sửa lịch trực")]
        public static string QLLichTruc_editDienTu { get; set; } = "QLLichTruc_editDienTu";

        [DisplayName("Quyền xóa lịch trực")]
        public static string QLLichTruc_deleteDienTu { get; set; } = "QLLichTruc_deleteDienTu";

        #endregion

        #region Thời hạn hợp đồng

        [DisplayName("Thời hạn hợp đồng")]
        public static string THOIHANHOPDONG { get; set; } = "THOIHANHOPDONG";

        [DisplayName("Thời hạn hợp đồng thử việc")]
        public static string THOIHANTHUVIEC { get; set; } = "THOIHANTHUVIEC";

        #endregion

        [DisplayName("Quản lý Điều động, Luân chuyển")]
        public static string QLDieuDong_index { get; set; } = "QLDieuDong_index";

        [DisplayName("Quản lý Nghỉ Phép")]
        public static string QL_NghiPhep_index { get; set; } = "QL_NghiPhep_index";

        [DisplayName("Quản lý Tai Nạn Lao Động")]
        public static string QLTaiNanLaoDong_index { get; set; } = "QLTaiNanLaoDong_index";

        [DisplayName("Quản lý Loại hợp đồng")]
        public static string LoaiHopDong_index { get; set; } = "LoaiHopDong_index";

        [DisplayName("Quản lý Hợp đồng")]
        public static string LyLichHopDong_index { get; set; } = "LyLichHopDong_index";

        [DisplayName("Quản lý Khen thưởng kỷ luật")]
        public static string KhenThuongKyLuat_index { get; set; } = "KhenThuongKyLuat_index";

        [DisplayName("Quản lý Đơn vị quảng cáo")]
        public static string DonViQuangCao_index { get; set; } = "DonViQuangCao_index";

        [DisplayName("Quản lý Lịch sử thay đổi ảnh")]
        public static string HistoryChange_Image_index { get; set; } = "HistoryChange_Image_index";

        [DisplayName("Quản lý Quản trị tin hệ thống")]
        public static string QLTinHeThong_index { get; set; } = "QLTinHeThong_index";

        [DisplayName("Quản lý Quản trị tin hệ thống FileAttack")]
        public static string QLTinHeThongFileAttack_index { get; set; } = "QLTinHeThongFileAttack_index";

        public static string QLTinHeThong_DeleteFile { get; set; } = "QLTinHeThong_DeleteFile";
        public static string QLTinHeThong_Delete { get; set; } = "QLTinHeThong_Delete";
        public static string QLTinHeThong_Edit { get; set; } = "QLTinHeThong_Edit";
        public static string QLTinHeThong_create { get; set; } = "QLTinHeThong_Create";

        [DisplayName("Quản lý Nghỉ Lễ")]
        public static string QLNgayLe_index { get; set; } = "QLNgayLe_index";

        [DisplayName("Quản lý Kiêm Nhiệm")]
        public static string QLKiemNhiem_index { get; set; } = "QLKiemNhiem_index";

        #region Quản lý quyết định bổ nhiệm

        [DisplayName("Quản lý Quyết định bổ nhiệm")]
        public static string QLQuyetDinhBoNhiem_index { get; set; } = "QLQuyetDinhBoNhiem_index";

        [DisplayName("Quản lý Quyết định bổ nhiệm sắp hết hạn")]
        public static string QLQuyetDinhBoNhiemSapHet_index { get; set; } = "QLQuyetDinhBoNhiemSapHet_index";

        [DisplayName("Quyền thêm mới quyết định bổ nhiệm")]
        public static string QLQuyetDinhBoNhiem_create { get; set; } = "QLQuyetDinhBoNhiem_create";

        [DisplayName("Quyền sửa quyết định bổ nhiệm")]
        public static string QLQuyetDinhBoNhiem_edit { get; set; } = "QLQuyetDinhBoNhiem_edit";

        [DisplayName("Quyền xóa quyết định bổ nhiệm")]
        public static string QLQuyetDinhBoNhiem_delete { get; set; } = "QLQuyetDinhBoNhiem_delete";

        [DisplayName("Quyền quản lý nhân sự sắp hết hợp đồng")]
        public static string LyLich2C_index_HetHopDong { get; set; } = "LyLich2C_index_HetHopDong";

        [DisplayName("Quyền quản lý nhân sự nâng lương")]
        public static string LyLich2C_indexNangLuong { get; set; } = "LyLich2C_indexNangLuong";

        #endregion

        #region Quyền văn bản pháp quy

        [DisplayName("Quyền thêm mới văn bản")]
        public static string LegalDocument_Add { get; set; } = "LegalDocument_Add";

        [DisplayName("Quyền thêm sửa văn bản")]
        public static string LegalDocument_edit { get; set; } = "LegalDocument_edit";

        [DisplayName("Quyền thêm xóa văn bản")]
        public static string LegalDocument_delete { get; set; } = "LegalDocument_delete";

        [DisplayName("Quyền duyệt văn bản")]
        public static string LegalDocument_Duyet { get; set; } = "LegalDocument_Duyet";

        [DisplayName("Quyền hủy duyệt văn bản")]
        public static string LegalDocument_HuyDuyet { get; set; } = "LegalDocument_HuyDuyet";

        #endregion

        #region Quyền quảng cáo

        [DisplayName("Quyền đăng")]
        public static string QLQuangCao_QuyenDang { get; set; } = "QLQuangCao_QuyenDang";

        [DisplayName("Quyền gỡ")]
        public static string QLQuangCao_QuyenGo { get; set; } = "QLQuangCao_QuyenGo";

        [DisplayName("Quyền xóa")]
        public static string QLQuangCao_QuyenXoa { get; set; } = "QLQuangCao_QuyenXoa";

        [DisplayName("Quyền sửa")]
        public static string QLQuangCao_QuyenSua { get; set; } = "QLQuangCao_QuyenSua";

        [DisplayName("Quyền thêm mới")]
        public static string QLQuangCao_QuyenThemMoi { get; set; } = "QLQuangCao_QuyenThemMoi";

        #endregion

        #region Quản lý chuyên đề quảng cáo

        [DisplayName("Quản lý Chuyên đề quảng cáo")]
        public static string QLChuyenDeQuangCao_index { get; set; } = "QLChuyenDeQuangCao_index";

        [DisplayName("Quyền tạo mới")]
        public static string QLChuyenDeQuangCao_create { get; set; } = "QLChuyenDeQuangCao_create";

        [DisplayName("Quyền tạo mới")]
        public static string QLChuyenDeQuangCao_edit { get; set; } = "QLChuyenDeQuangCao_edit";

        [DisplayName("Quyền Xóa")]
        public static string QLChuyenDeQuangCao_delete { get; set; } = "QLChuyenDeQuangCao_delete";

        [DisplayName("Quản lý Cập nhật tiến độ quảng cáo")]
        public static string CapNhatTienDoQuangCao_index { get; set; } = "CapNhatTienDoQuangCao_index";

        #endregion

        #region Quản lý tạo đàm trực tuyến

        [DisplayName("Quản lý biên tập tọa đàm trực tuyến")]
        public static string BienTapToaDamTT_index { get; set; } = "BienTapToaDamTT_index";

        #endregion

        #region Quản lý đánh giá nhân sự

        [DisplayName("Quản lý Đánh giá nhân sự")]
        public static string LyLichVsDanhGiaNhanSu_index { get; set; } = "LyLichVsDanhGiaNhanSu_index";

        public static string LyLichVsDanhGiaNhanSu_create { get; set; } = "LyLichVsDanhGiaNhanSu_create";
        public static string LyLichVsDanhGiaNhanSu_edit { get; set; } = "LyLichVsDanhGiaNhanSu_edit";
        public static string LyLichVsDanhGiaNhanSu_delete { get; set; } = "LyLichVsDanhGiaNhanSu_delete";

        #endregion

        #region Quản lý quảng cáo báo in

        [DisplayName("Quản lý Quảng cáo báo in")]
        public static string QLQuangCaoBaoIn_index { get; set; } = "QLQuangCaoBaoIn_index";

        public static string QLQuangCaoBaoIn_indexAll { get; set; } = "QLQuangCaoBaoIn_indexAll";
        public static string QLQuangCaoBaoIn_ChoKyThuatXuLy { get; set; } = "QLQuangCaoBaoIn_ChoKyThuatXuLy";
        public static string QLQuangCaoBaoIn_ChoThuKyXuLy { get; set; } = "QLQuangCaoBaoIn_ChoThuKyXuLy";
        public static string QLQuangCaoBaoIn_create { get; set; } = "QLQuangCaoBaoIn_create";
        public static string QLQuangCaoBaoIn_edit { get; set; } = "QLQuangCaoBaoIn_edit";
        public static string QLQuangCaoBaoIn_delete { get; set; } = "QLQuangCaoBaoIn_delete";

        [DisplayName("Quản lý Quảng cáo file Attach ")]
        public static string QLQuangCaoBaoInFileAttach_index { get; set; } = "QLQuangCaoBaoInFileAttach_index";

        public static string QLQuangCaoBaoIn_GuiKyThuat { get; set; } = "QLQuangCaoBaoIn_GuiKyThuat";
        public static string QLQuangCaoBaoIn_GuiThuKy { get; set; } = "QLQuangCaoBaoIn_GuiThuKy";
        public static string QLQuangCaoBaoIn_KyThuatTraVe { get; set; } = "QLQuangCaoBaoIn_KyThuatTraVe";
        public static string QLQuangCaoBaoIn_ThuKyTraVe { get; set; } = "QLQuangCaoBaoIn_ThuKyTraVe";
        public static string QLQuangCaoBaoIn_Duyet { get; set; } = "QLQuangCaoBaoIn_Duyet";
        public static string QLQuangCaoBaoIn_NhanThongBaoDuyet { get; set; } = "QLQuangCaoBaoIn_NhanThongBaoDuyet";
        #endregion

        #region Quản lý thư mục ảnh

        [DisplayName("Quản lý Thư viện ảnh ")]
        public static string FolderUpload_index { get; set; } = "FolderUpload_index";

        [DisplayName("Quản lý File ảnh")]
        public static string FolderUploadFileAttach_index { get; set; } = "FolderUploadFileAttach_index";

        #endregion

        #region Quản lý tài sản

        [DisplayName("Quản lý tài sản")]
        public static string QLTaiSan_index { get; set; } = "QLTaiSan_index";

        [DisplayName("Quyền cấp tài sản")]
        public static string QLTaiSan_CapTaiSan { get; set; } = "QLTaiSan_CapTaiSan";

        [DisplayName("Quyền thu hồi tài sản")]
        public static string QLTaiSan_ThuHoiTaiSan { get; set; } = "QLTaiSan_ThuHoiTaiSan";

        [DisplayName("Quyền tạo mới thông tin tài sản")]
        public static string QLTaiSan_create { get; set; } = "QLTaiSan_create";

        [DisplayName("Quyền chỉnh sửa thông tin tài sản")]
        public static string QLTaiSan_edit { get; set; } = "QLTaiSan_edit";

        [DisplayName("Quyền xóa thông tin tài sản")]
        public static string QLTaiSan_delete { get; set; } = "QLTaiSan_delete";

        [DisplayName("Quản lý loại tài sản")]
        public static string QLPhanLoaiTaiSan_index { get; set; } = "QLPhanLoaiTaiSan_index";

        [DisplayName("Quyền tạo mới loại tài sản")]
        public static string QLPhanLoaiTaiSan_create { get; set; } = "QLPhanLoaiTaiSan_create";

        [DisplayName("Quyền chỉnh sửa loại tài sản")]
        public static string QLPhanLoaiTaiSan_edit { get; set; } = "QLPhanLoaiTaiSan_edit";

        [DisplayName("Quyền xóa loại tài sản")]
        public static string QLPhanLoaiTaiSan_delete { get; set; } = "QLPhanLoaiTaiSan_delete";

        [DisplayName("Quản lý người dùng tài sản")]
        public static string QLNguoiDungTaiSan_index { get; set; } = "QLNguoiDungTaiSan_index";

        [DisplayName("Quyền chỉnh sửa thông tin người dùng tài sản")]
        public static string QLNguoiDungTaiSan_edit { get; set; } = "QLNguoiDungTaiSan_edit";

        [DisplayName("Quyền chỉnh xem lịch sử tài sản")]
        public static string QLTaiSan_history { get; set; } = "QLTaiSan_history";

        [DisplayName("Quyền nhận thông báo tài sản")]
        public static string QLTaiSan_noti { get; set; } = "QLTaiSan_noti";

        [DisplayName("Quyền nhập thông tin nâng cấp tài sản")]
        public static string QLTaiSan_NangCapTaiSan { get; set; } = "QLTaiSan_NangCapTaiSan";

        #endregion

        #region Quản lý tuyển dụng

        [DisplayName("Quản lý Hồ sơ ứng viên")]
        public static string QLHoSoUngVien_index { get; set; } = "QLHoSoUngVien_index";

        public static string QLHoSoUngVien_create { get; set; } = "QLHoSoUngVien_create";
        public static string QLHoSoUngVien_edit { get; set; } = "QLHoSoUngVien_edit";
        public static string QLHoSoUngVien_delete { get; set; } = "QLHoSoUngVien_delete";
        #endregion
        #region Quản lý đợt tuyển dụng

        [DisplayName("Quản lý Đợt tuyển dụng")]
        public static string QLDotTuyenDung_index { get; set; } = "QLDotTuyenDung_index";

        public static string QLDotTuyenDung_create { get; set; } = "QLDotTuyenDung_create";
        public static string QLDotTuyenDung_edit { get; set; } = "QLDotTuyenDung_edit";
        public static string QLDotTuyenDung_delete { get; set; } = "QLDotTuyenDung_delete";
        #endregion

        #region Quản lý nhu cầu tuyển dụng

        [DisplayName("Quản lý Nhu cầu tuyển dụng")]
        public static string QLNhuCauTuyenDung_index { get; set; } = "QLNhuCauTuyenDung_index";

        public static string QLNhuCauTuyenDung_create { get; set; } = "QLNhuCauTuyenDung_create";
        public static string QLNhuCauTuyenDung_edit { get; set; } = "QLNhuCauTuyenDung_edit";
        public static string QLNhuCauTuyenDung_delete { get; set; } = "QLNhuCauTuyenDung_delete";

        #endregion

        #region Nhu cầu tuyển dụng vs Hệ tuyển dụng

        [DisplayName("Quản lý Nhu cầu tuyển dụng vs Hệ tuyển dụng")]
        public static string QLNhuCauTuyenDungVsHeTuyenDung_index { get; set; } = "QLNhuCauTuyenDungVsHeTuyenDung_index";

        public static string QLNhuCauTuyenDungVsHeTuyenDung_create { get; set; } = "QLNhuCauTuyenDungVsHeTuyenDung_create";
        public static string QLNhuCauTuyenDungVsHeTuyenDung_edit { get; set; } = "QLNhuCauTuyenDungVsHeTuyenDung_edit";
        public static string QLNhuCauTuyenDungVsHeTuyenDung_delete { get; set; } = "QLNhuCauTuyenDungVsHeTuyenDung_delete";
        #endregion

        #region Quản lý đánh giá ứng viên

        [DisplayName("Quản lý Đánh giá ứng viên")]
        public static string QLDanhGiaUngVien_index { get; set; } = "QLDanhGiaUngVien_index";

        public static string QLDanhGiaUngVien_create { get; set; } = "QLDanhGiaUngVien_create";
        public static string QLDanhGiaUngVien_edit { get; set; } = "QLDanhGiaUngVien_edit";
        public static string QLDanhGiaUngVien_delete { get; set; } = "QLDanhGiaUngVien_delete";

        [DisplayName("Quản lý Nhận xét ứng viên")]
        public static string DanhGiaUngVienVsNhanXet_index { get; set; } = "DanhGiaUngVienVsNhanXet_index";

        #endregion

        #region Quản lý hợp đồng

        [DisplayName("Mã chức năng hợp đồng")]
        public static string QLHopDong { get; set; } = "QLHopDong";

        [DisplayName("Quản lý tất cả hợp đồng")]
        public static string HDQLHopDong_index { get; set; } = "HDQLHopDong_index";

        [DisplayName("Hợp đồng hết hạn")]
        public static string HDQLHopDong_indexHetHan { get; set; } = "HDQLHopDong_indexHetHan";

        [DisplayName("Hợp đồng bản thảo")]
        public static string HDQLHopDong_BanThao { get; set; } = "HDQLHopDong_BanThao";

        [DisplayName("Hợp đồng đã ký")]
        public static string HDQLHopDong_DaKy { get; set; } = "HDQLHopDong_DaKy";

        [DisplayName("Hợp đồng đã hủy")]
        public static string HDQLHopDong_DaHuy { get; set; } = "HDQLHopDong_DaHuy";

        [DisplayName("Hợp đồng đã kết thúc")]
        public static string HDQLHopDong_DaKetThuc { get; set; } = "HDQLHopDong_DaKetThuc";

        [DisplayName("Quyền sửa hợp đồng")]
        public static string HDQLHopDong_Edit { get; set; } = "HDQLHopDong_Edit";

        [DisplayName("Quyền xóa hợp đồng")]
        public static string HDQLHopDong_Delete { get; set; } = "HDQLHopDong_Delete";

        [DisplayName("Quyền tạo mới hợp đồng")]
        public static string HDQLHopDong_Create { get; set; } = "HDQLHopDong_Create";

        [DisplayName("Quyền thêm phụ lục hợp đồng")]
        public static string HDQLHopDong_AddPhuLuc { get; set; } = "HDQLHopDong_AddPhuLuc";

        [DisplayName("Quyền chỉnh sửa phụ lục hợp đồng")]
        public static string HDQLHopDong_EditPhuLuc { get; set; } = "HDQLHopDong_EditPhuLuc";

        [DisplayName("Quyền chỉnh xóa phụ lục hợp đồng")]
        public static string HDQLHopDong_DeletePhuLuc { get; set; } = "HDQLHopDong_DeletePhuLuc";

        #endregion

        #region Quản lý ngân hàng

        [DisplayName("Quản lý Ngân Hàng")]
        public static string QLNganHang_index { get; set; } = "QLNganHang_index";

        #endregion

        #region Quản lý chấm công

        [DisplayName("Danh sách ca làm việc")]
        public static string QLChamCong_CaLamViec { get; set; } = "QLChamCong_CaLamViec";

        [DisplayName("Quyền được phép chấm công")]
        public static string QLChamCong_DuocPhepChamCong { get; set; } = "QLChamCong_DuocPhepChamCong";

        [DisplayName("Quyền xem tất cả thông tin chấm công")]
        public static string QLChamCong_ViewAll { get; set; } = "QLChamCong_ViewAll";

        #endregion

        #region Quản lý lương

        [DisplayName("Quản lý Ngạch")]
        public static string QLNgach_index { get; set; } = "QLNgach_index";

        [DisplayName("Quản lý Bậc Lương")]
        public static string QLBacLuong_index { get; set; } = "QLBacLuong_index";

        [DisplayName("Quản lý Lương Cơ Sở")]
        public static string QLLuongCoSo_index { get; set; } = "QLLuongCoSo_index";

        #endregion

        #region Quản lý phụ cấp

        [DisplayName("Quản lý Phụ cấp")]
        public static string QLPhuCap_index { get; set; } = "QLPhuCap_index";

        public static string QLPhuCap_create { get; set; } = "QLPhuCap_create";
        public static string QLPhuCap_edit { get; set; } = "QLPhuCap_edit";
        public static string QLPhuCap_delete { get; set; } = "QLPhuCap_delete";
        #endregion

        #region Quản lý đợt khám định kỳ

        [DisplayName("Quản lý đợt khám định kỳ")]
        public static string QLDotKhamDinhKy_index { get; set; } = "QLDotKhamDinhKy_index";

        public static string QLDotKhamDinhKy_create { get; set; } = "QLDotKhamDinhKy_create";
        public static string QLDotKhamDinhKy_edit { get; set; } = "QLDotKhamDinhKy_edit";
        public static string QLDotKhamDinhKy_delete { get; set; } = "QLDotKhamDinhKy_delete";
        #endregion

        #region Quản lý lượt khám định kỳ

        [DisplayName("Quản lý lượt khám định kỳ")]
        public static string QLLuotKhamDinhKy_index { get; set; } = "QLLuotKhamDinhKy_index";

        public static string QLLuotKhamDinhKy_create { get; set; } = "QLLuotKhamDinhKy_create";
        public static string QLLuotKhamDinhKy_edit { get; set; } = "QLLuotKhamDinhKy_edit";
        public static string QLLuotKhamDinhKy_delete { get; set; } = "QLLuotKhamDinhKy_delete";

        #endregion

        #region Quản lý đảm nhiệm chức vụ bệnh viện

        [DisplayName("Quản lý Đảm nhiệm chức vụ bệnh viện")]
        public static string DamNhiemChucVuBenhVien_index { get; set; } = "DamNhiemChucVuBenhVien_index";

        public static string DamNhiemChucVuBenhVien_create { get; set; } = "DamNhiemChucVuBenhVien_create";
        public static string DamNhiemChucVuBenhVien_edit { get; set; } = "DamNhiemChucVuBenhVien_edit";
        public static string DamNhiemChucVuBenhVien_delete { get; set; } = "DamNhiemChucVuBenhVien_delete";
        #endregion

        #region Quản lý thiết lập chấm công

        [DisplayName("Quản lý Thiết lập chấm công")]
        public static string QLThietLapChamCong_index { get; set; } = "QLThietLapChamCong_index";

        public static string QLThietLapChamCong_create { get; set; } = "QLThietLapChamCong_create";
        public static string QLThietLapChamCong_edit { get; set; } = "QLThietLapChamCong_edit";
        public static string QLThietLapChamCong_delete { get; set; } = "QLThietLapChamCong_delete";
        #endregion

        #region Quản lý phép

        [DisplayName("Quản lý Phép")]
        public static string QLPhep_index { get; set; } = "QLPhep_index";

        public static string QLPhep_create { get; set; } = "QLPhep_create";
        public static string QLPhep_edit { get; set; } = "QLPhep_edit";
        public static string QLPhep_delete { get; set; } = "QLPhep_delete";
        #endregion

        #region Quản lý hưu trí

        [DisplayName("Quản lý hưu trí")]
        public static string QLHuuTriTT_index { get; set; } = "QLHuuTriTT_index";

        public static string QLHuuTriTT_create { get; set; } = "QLHuuTriTT_create";
        public static string QLHuuTriTT_edit { get; set; } = "QLHuuTriTT_edit";
        public static string QLHuuTriTT_delete { get; set; } = "QLHuuTriTT_delete";
        #endregion
    }
}