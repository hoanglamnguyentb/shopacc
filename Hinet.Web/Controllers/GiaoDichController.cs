using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Presentation.Command;
using DocumentFormat.OpenXml.Spreadsheet;
using Hinet.Model;
using Hinet.Model.Entities;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Service.AppUserService;
using Hinet.Service.BannerService;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DanhMucGameTaiKhoanService;
using Hinet.Service.DichVuService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.NotificationService;
using Hinet.Service.RoleService;
using Hinet.Service.TaiKhoanService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.TinTucService;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using Hinet.Web.Models.GameVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    [RoutePrefix("giao-dich")]  // Đặt prefix chung
    public class GiaoDichController : EndUserController
    {
        private readonly IGiaoDichService _giaoDichService;
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IAppUserService _appUserService;
        private readonly INotificationService _notificationService;
        private readonly IDanhMucGameService _danhMucGameService;
        private readonly IMapper _mapper;

        public GiaoDichController(IGiaoDichService giaoDichService, ITaiKhoanService taiKhoanService, IAppUserService appUserService, INotificationService notificationService, IDanhMucGameService danhMucGameService, IMapper mapper)
        {
            _giaoDichService = giaoDichService;
            _taiKhoanService = taiKhoanService;
            _appUserService = appUserService;
            _notificationService = notificationService;
            _danhMucGameService = danhMucGameService;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("mua-acc/{id?}")]
        public ActionResult MuaAcc(int id)
        {
            using (var db = new DbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var taiKhoan = _taiKhoanService.GetById(id);
                    if (taiKhoan == null || taiKhoan.TrangThai != TrangThaiTaiKhoanConstant.CHUABAN)
                    {
                        return Json(new { success = false, message = "Tài khoản không tồn tại hoặc đã được bán." });
                    }

                    var giaBan = taiKhoan.GiaKhuyenMai ?? taiKhoan.GiaGoc;
                    if (CurrentUserInfo.Balance < giaBan)
                    {
                        return Json(new { success = false, message = "Số dư của bạn không đủ để mua tài khoản này." });
                    }

                    var user = _appUserService.FindBy(x => x.Id == CurrentUserId).FirstOrDefault();
                    if (user == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                    }
                    user.Balance -= giaBan;
                    _appUserService.Update(user);
                    var userDto = _appUserService.GetDtoById(user.Id);
                    SessionManager.SetValue(SessionManager.USER_INFO, userDto);

                    taiKhoan.TrangThai = TrangThaiTaiKhoanConstant.DABAN;
                    _taiKhoanService.Update(taiKhoan);

                    var giaoDich = new GiaoDich
                    {
                        UserId = CurrentUserId.GetValueOrDefault(),
                        DoiTuongId = id,
                        LoaiDoiTuong = nameof(TaiKhoan),
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUA,
                        TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                        PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                        NgayGiaoDich = DateTime.Now,
                        NgayThanhToan = DateTime.Now,
                        SoTien = -giaBan,
                        NoiDung = $"Mua tài khoản {taiKhoan.Code}",
                    };
                    _giaoDichService.Create(giaoDich);
                    _notificationService.CreateNoti(
                        CurrentUserId.GetValueOrDefault(),
                        $"/acc/{taiKhoan.Code}",
                        $"Mua tài khoản {taiKhoan.Code} thành công!"
                    );
                    transaction.Commit();

                    return Json(new { success = true, message = "Thanh toán thành công!" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Đã xảy ra lỗi trong quá trình thanh toán: " + ex.Message });
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MuaAccRandom(int id) //Id của danh mục game
        {
            using (var db = new DbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var taiKhoan = _taiKhoanService.GetRanDomByDMGameId(id);
                    if (taiKhoan == null || taiKhoan.TrangThai != TrangThaiTaiKhoanConstant.CHUABAN)
                    {
                        return Json(new { success = false, message = "Tài khoản không tồn tại hoặc đã được bán." });
                    }
                    var danhMucGame = _danhMucGameService.GetById(taiKhoan.DanhMucGameId);
        
                    var giaBan = danhMucGame.GiaKhuyenMai ?? danhMucGame.GiaGoc;
                    if (CurrentUserInfo.Balance < giaBan)
                    {
                        return Json(new { success = false, message = "Số dư của bạn không đủ để mua tài khoản này." });
                    }

                    var user = _appUserService.FindBy(x => x.Id == CurrentUserId).FirstOrDefault();
                    if (user == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                    }
                    user.Balance -= giaBan;
                    _appUserService.Update(user);
                    var userDto = _appUserService.GetDtoById(user.Id);
                    SessionManager.SetValue(SessionManager.USER_INFO, userDto);

                    taiKhoan.TrangThai = TrangThaiTaiKhoanConstant.DABAN;
                    _taiKhoanService.Update(taiKhoan);

                    var giaoDich = new GiaoDich
                    {
                        UserId = CurrentUserId.GetValueOrDefault(),
                        DoiTuongId = id,
                        LoaiDoiTuong = nameof(TaiKhoan),
                        LoaiGiaoDich = LoaiGiaoDichConstant.MUA,
                        TrangThai = TrangThaiGiaoDichConstant.DATHANHTOAN,
                        PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG,
                        NgayGiaoDich = DateTime.Now,
                        NgayThanhToan = DateTime.Now,
                        SoTien = -giaBan,
                        NoiDung = $"Mua tài khoản {taiKhoan.Code}",
                    };
                    _giaoDichService.Create(giaoDich);
                    _notificationService.CreateNoti(
                        CurrentUserId.GetValueOrDefault(),
                        $"/acc/{taiKhoan.Code}",
                        $"Mua tài khoản {taiKhoan.Code} thành công!"
                    );
                    transaction.Commit();

                    return Json(new { success = true, message = "Thanh toán thành công!" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Đã xảy ra lỗi trong quá trình thanh toán: " + ex.Message });
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NapTopup(GiaoDichTopupVM model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });

            var EntityModel = _mapper.Map<GiaoDich>(model);
            EntityModel.LoaiGiaoDich = LoaiGiaoDichConstant.NAPTOPUP;
            EntityModel.TrangThai = TrangThaiGiaoDichConstant.CHOXULY;
            EntityModel.PhuongThucThanhToan = PhuongThucThanhToanConstant.NGANHANG;
            EntityModel.NgayGiaoDich = DateTime.Now;
            EntityModel.NgayThanhToan = DateTime.Now;

            _giaoDichService.Create(EntityModel);

            string qrUrl = $"https://img.vietqr.io/image/MB-9704228372-compact.png?amount={model.SoTien}&addInfo=Topup{EntityModel.Id}";

            return Json(new
            {
                success = true,
                transactionId = EntityModel.Id,
                amount = model.SoTien,
                qrData = qrUrl
            });
        }
    }
}