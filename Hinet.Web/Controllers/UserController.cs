using DocumentFormat.OpenXml.Wordprocessing;
using Hinet.Service.Constant;
using Hinet.Service.GiaoDichService;
using Hinet.Service.GiaoDichService.Dto;
using Hinet.Service.NotificationService;
using Hinet.Service.NotificationService.Dto;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    [RoutePrefix("tai-khoan")]  // Đặt prefix chung
    public class UserController : EndUserController
    {
        private readonly IGiaoDichService _giaoDichService;
        private readonly INotificationService _notificationService;
        public UserController(IGiaoDichService giaoDichService, INotificationService notificationService)
        {
            _giaoDichService = giaoDichService;
            _notificationService = notificationService;
        }

        [Route("~/thong-tin")]
        public ActionResult ThongTin()
        {
            ViewBag.MenuBottom = "tai-khoan";
            return View();
        }

        [Route("~/doi-mat-khau")]
        public ActionResult DoiMatKhau()
        {
            ViewBag.MenuBottom = "tai-khoan";
            return View();
        }

        [Route("~/thong-bao")]
        public ActionResult ThongBao(NotificationSearchDto search, int page = 1, int pageSize = 10)
        {
            search.ToUserFilter = CurrentUserId.GetValueOrDefault();
            var data = _notificationService.GetDaTaByPage(CurrentUserId, search, page, pageSize);
            ViewBag.MenuBottom = "tai-khoan";
            return View(data);
        }

        [Route("~/lich-su-giao-dich")]
        public ActionResult LichSuGiaoDich(GiaoDichSearchDto search, int page = 1, int pageSize = 10)
        {
            search.UserIdFilter = CurrentUserId.GetValueOrDefault();
            var data = _giaoDichService.GetDaTaByPage(search, page, pageSize);
            ViewData["LoaiGiaoDich"] = null;
            ViewBag.MenuBottom = "tai-khoan";
            return View(data);
        }


        [Route("~/lich-su-nap-tien")]
        public ActionResult LichSuNapTien()
        {
            var search = new GiaoDichSearchDto
            {
                UserIdFilter = CurrentUserId.GetValueOrDefault(),
                LoaiGiaoDichFilter = LoaiGiaoDichConstant.NAPTHUONG
            };
            var data = _giaoDichService.GetDaTaByPage(search, 1, 10);
            ViewData["LoaiGiaoDich"] = LoaiGiaoDichConstant.NAPTHUONG;
            ViewBag.MenuBottom = "tai-khoan";
            return View(data);
        }

        [Route("~/tai-khoan-da-mua")]
        public ActionResult TaiKhoanDaMua()
        {
            var search = new GiaoDichSearchDto
            {
                UserIdFilter = CurrentUserId.GetValueOrDefault(),
                LoaiGiaoDichFilter = LoaiGiaoDichConstant.MUAACC,
                TrangThaiFilter = TrangThaiGiaoDichConstant.DATHANHTOAN,
            };
            var data = _giaoDichService.GetDaTaByPage(search, 1, 10);
            ViewData["LoaiGiaoDich"] = LoaiGiaoDichConstant.MUAACC;
            ViewBag.MenuBottom = "tai-khoan";
            return View(data);
        }


        [Route("~/nap-topup")]
        public ActionResult NapTopup()
        {
            var search = new GiaoDichSearchDto
            {
                UserIdFilter = CurrentUserId.GetValueOrDefault(),
                LoaiGiaoDichFilter = LoaiGiaoDichConstant.NAPTOPUP
            };
            var data = _giaoDichService.GetDaTaByPage(search, 1, 10);
            ViewData["LoaiGiaoDich"] = LoaiGiaoDichConstant.NAPTOPUP;
            ViewBag.MenuBottom = "tai-khoan";
            return View(data);
        }

        #region Ajax
        [AllowAnonymous]
        public ActionResult LoadLichSuGiaoDich(GiaoDichSearchDto search, int page = 1, int pageSize = 10)
        {
            search.UserIdFilter = CurrentUserId.GetValueOrDefault();
            var data = _giaoDichService.GetDaTaByPage(search, page, pageSize);
            ViewData["LoaiGiaoDich"] = search.LoaiGiaoDichFilter;
            return PartialView("_LichSuGiaoDichList", data);
        }

        [AllowAnonymous]
        public ActionResult LoadThongBao(NotificationSearchDto search, int page = 1, int pageSize = 10)
        {
            search.ToUserFilter = CurrentUserId.GetValueOrDefault();
            var data = _notificationService.GetDaTaByPage(CurrentUserId, search, page, pageSize);
            return PartialView("_ThongBaoList", data);
        }


        public void MarkAsRead(long id)
        {
            var noti = _notificationService.GetById(id);
            if(noti != null)
            {
                noti.IsRead = true;
                _notificationService.Update(noti);
            }
        }

        #endregion
    }
}