using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Hinet.Repository.GiaoDichRepository;
using Hinet.Service.AppUserService;
using Hinet.Service.BannerService;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DepositService;
using Hinet.Service.DichVuService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.NotificationService;
using Hinet.Service.NotificationService.Dto;
using Hinet.Service.RoleService;
using Hinet.Service.SiteConfigService;
using Hinet.Service.TinTucService;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    public class HomeController : EndUserController
    {
        private readonly List<(string ServiceName, string EntityName, Dictionary<string, string> ExportConfig)> _Config;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IMapper _mapper;
        private readonly ILog _Ilog;
        private readonly IDM_DulieuDanhmucService _DM_DulieuDanhmucService;
        private readonly IAppUserService _appUserService;
        private readonly IRoleService _roleService;
        private readonly IDichVuService _dichVuService;
        private readonly IBannerService _bannerService;
        private readonly IGameService _gameService;
        private readonly ITinTucService _tinTucService;
        private readonly IDepositService _depositService;
        private readonly INotificationService _notificationService;
        private readonly IGiaoDichService _giaoDichService;
        private readonly ISiteConfigService _siteConfigService;
        private readonly IDanhMucGameService _danhMucGameService;

        public HomeController(
                IDM_DulieuDanhmucService dM_DulieuDanhmucService,
                IMapper mapper, ILog iLog,
                IDM_DulieuDanhmucService DM_DulieuDanhmucService,
                IAppUserService appUserService, IDichVuService dichVuService,
                IBannerService bannerService, IGameService gameService,
                ITinTucService tinTucService, IDepositService depositService,
                INotificationService notificationService, IGiaoDichService giaoDichService,
                ISiteConfigService siteConfigService, IDanhMucGameService danhMucGameService)
        {
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _mapper = mapper;
            _Ilog = iLog;
            _DM_DulieuDanhmucService = DM_DulieuDanhmucService;
            _appUserService = appUserService;
            _dichVuService = dichVuService;
            _bannerService = bannerService;
            _gameService = gameService;
            _tinTucService = tinTucService;
            _depositService = depositService;
            _notificationService = notificationService;
            _giaoDichService = giaoDichService;
            _siteConfigService = siteConfigService;
            _danhMucGameService = danhMucGameService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var homeVM = new HomeVM();
            RefreshSession();
            homeVM.ListDichVu = _dichVuService.FindBy(x => x.KichHoat == true).OrderBy(x => x.STT).ToList();
            homeVM.ListBanner = _bannerService.FindBy(x => x.KichHoat == true).OrderBy(x => x.STT).ToList();
            homeVM.ListGame = _gameService.GetListGame();
            homeVM.ListTinTuc = _tinTucService.FindBy(x => x.TrangThai == TrangThaiTinTucConstant.XUATBAN)
                .OrderByDescending(x => x.CreatedDate).Take(3).ToList();
            homeVM.SiteConfig = _siteConfigService.FindBy(x => x.KichHoat == true).FirstOrDefault();
            ViewBag.MenuBottom = "home";
            return View(homeVM);
        }

        //nạp thẻ
        [AllowAnonymous]
        public ActionResult Recharge()
        {
            RefreshSession();
            ViewBag.MenuBottom = "recharge";
            return View();
        }

        //Nạp top up
        public ActionResult NapTopup()
        {
            ViewBag.ListGame = _gameService.GetAll().ToList();
            return View();
        }

        //tài khoản
        [AllowAnonymous]
        public ActionResult Accounts()
        {
            RefreshSession();
            return View();
        }
        //dịch vụ
        [AllowAnonymous]
        public ActionResult Services()
        {
            RefreshSession();
            return View();
        }
        //tin tức
        [AllowAnonymous]
        public ActionResult News()
        {
            RefreshSession();
            return View();
        }

        //tin tức
        [AllowAnonymous]
        public ActionResult AccountDetail()
        {
            RefreshSession();
            return View();
        }

        public ActionResult DepositHistory()
        {
            var viewData = _depositService.GetAll(CurrentUserId.Value);
            return View(viewData);
        }

        private void RefreshSession()
        {
            try
            {
                SessionManager.Remove(SessionManager.USER_INFO);
                var userDto = _appUserService.GetDtoById(CurrentUserId.Value);
                SessionManager.SetValue(SessionManager.USER_INFO, userDto);
            }
            catch
            {

            }
        }

        public ActionResult NotificationsPartial(int page = 1, int pageSize = 10)
        {
            var searchModel = new NotificationSearchDto
            {
                ToUserFilter = CurrentUserId,
                IsReadFilter = false,
            };
            var data = _notificationService.GetDaTaByPage(CurrentUserId, searchModel, page, pageSize);
            return PartialView("_NotificationsPartial", data.ListItem);
        }

        [HttpGet]
        public ActionResult GetUnreadNotificationCount()
        {
            var count = _notificationService.GetUnreadNotificationCount(CurrentUserId.GetValueOrDefault());
            return Json(new { count = count }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public PartialViewResult TopNapThe()
        {
            var listTop = _giaoDichService.GetTopNapTheThang(5);
            return PartialView("_TopNapThePartial", listTop);
        }

        [AllowAnonymous]
        public PartialViewResult DichVuNoiBat()
        {
            var listTop = _danhMucGameService.FindBy(x => x.NoiBat == true).ToList();
            return PartialView("_DichVuNoiBatPartial", listTop);
        }

        
    }
}