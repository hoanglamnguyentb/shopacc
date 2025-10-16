using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.DanhMucGameTaiKhoanService;
using Hinet.Service.GameService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.TaiKhoanService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Web.Filters;
using Hinet.Web.Models.GameVM;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    [RoutePrefix("game")]  // Đặt prefix chung
    public class GameController : EndUserController
    {
        private readonly IGameService _gameService;
        private readonly IDanhMucGameTaiKhoanService _danhMucGameTaiKhoanService;
        private readonly IDanhMucGameService _danhMucGameService;
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IGiaoDichService _giaoDichService;
        private readonly IAppUserService _appUserService;

        public GameController(IGameService gameService, 
            IDanhMucGameService danhMucGameService, ITaiKhoanService taiKhoanService, 
            IDanhMucGameTaiKhoanService danhMucGameTaiKhoanService, 
            IGiaoDichService giaoDichService, IAppUserService appUserService)
        {
            _gameService = gameService;
            _danhMucGameService = danhMucGameService;
            _taiKhoanService = taiKhoanService;
            _danhMucGameTaiKhoanService = danhMucGameTaiKhoanService;
            _giaoDichService = giaoDichService;
            _appUserService = appUserService;
        }

        // GET: Game
        [AllowAnonymous]
        [Route("{slug?}")]
        public ActionResult Index(string slug = null)
        {
            var vm = new IndexVM
            {
                Game = slug != null ? _gameService.GetBySlug(slug) : null,
                ListDanhMucGame = _gameService.GetListDanhMucGameBySlug(slug),
            };
            ViewBag.MenuBottom = "mua-acc";
            return View(vm);
        }

        // GET: Game
        //mua-acc/slug
        [AllowAnonymous]
        [Route("~/mua-acc/{slug?}")]
        public ActionResult DanhMuc(string slug, TaiKhoanSearchDto search, int page = 1, int pageSize = 20)
        {
            RefreshSession();
            var danhMuc = _danhMucGameService.GetBySlug(slug);
            var game = _gameService.GetById(danhMuc.GameId);
            var vm = new DanhMucGameVM
            {
                Game = game,
                DanhMucGame = danhMuc,
                TaiKhoanPagedResult = _gameService.GetTaiKhoanPagedByDanhMucSlug(slug, search, page, pageSize)
            };
            ViewBag.MenuBottom = "mua-acc";
            return View(vm);
        }

        [AllowAnonymous]
        [Route("~/acc/{code}")]
        public ActionResult ChiTietTaiKhoan(string code)
        {
            RefreshSession();
            var tk = _gameService.GetTaiKhoanByCode(code);
            // Lưu vào session list "DaXem"
            var daXem = Session["TaiKhoanDaXem"] as List<long> ?? new List<long>();
            if (!daXem.Contains(tk.Id))
            {
                daXem.Insert(0, tk.Id);
                if (daXem.Count > 10)
                    daXem = daXem.Take(10).ToList();
            }
            Session["TaiKhoanDaXem"] = daXem;
            var giaoDich = _giaoDichService
                .FindBy(x => x.DoiTuongId == tk.Id && x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                .FirstOrDefault();
            tk.IdNguoiMuaAcc = giaoDich?.UserId ?? null;
            ViewBag.MenuBottom = "mua-acc";
            return View(tk);
        }

        [Route("~/da-xem")]
        [AllowAnonymous]
        public ActionResult TKDaXem()
        {
            var daXem = Session["TaiKhoanDaXem"] as List<long> ?? new List<long>();
            var listTk = _gameService.GetListTaiKhoanDaXem(daXem);
            ViewBag.MenuBottom = "mua-acc";
            return View("TaiKhoanDaXem", listTk);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("~/search")]
        public ActionResult Search(string query)
        {
            var seachModel = new DanhMucGameSearchDto
            {
                NameFilter = query
            };
            var results = _danhMucGameService.GetDaTaByPage(seachModel, 1, -1).ListItem;
            ViewBag.MenuBottom = "mua-acc";
            return View(results);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("~/search/suggestions")]
        public JsonResult Suggestions(string q)
        {
            var seachModel = new DanhMucGameSearchDto
            {
                NameFilter = q
            };
            var results = _danhMucGameService.GetDaTaByPage(seachModel, 1, -1).ListItem;
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        #region Ajax
        [AllowAnonymous]
        [Route("LoadTaiKhoan")]
        public ActionResult LoadTaiKhoan(string slug, TaiKhoanSearchDto search, int page = 1, int pageSize = 20)
        {
            var result = _gameService.GetTaiKhoanPagedByDanhMucSlug(slug, search, page, pageSize);
            var danhMuc = _danhMucGameService.GetBySlug(slug);
            var game = _gameService.GetById(danhMuc.GameId);
            ViewData["DanhMuc"] = danhMuc;
            ViewData["Game"] = game;
            return PartialView("_TaiKhoanList", result);
        }
        #endregion

        #region Partial
        [AllowAnonymous]
        public PartialViewResult DanhMucGameKhac(int id)
        {
            var danhMucGameKhac = _gameService.GetListDanhMucGameKhac(id, 10);
            return PartialView("_DanhMucGameKhacPartial", danhMucGameKhac);
        }

        [AllowAnonymous]
        public PartialViewResult TaiKhoanLienQuan(int id)
        {
            var listTk = _gameService.GetListTaiKhoanLienQuan(id);
            return PartialView("_TaiKhoanLienQuanPartial", listTk);
        }

        [AllowAnonymous]
        public PartialViewResult TaiKhoanDaXem(int id)
        {
            var daXem = Session["TaiKhoanDaXem"] as List<long> ?? new List<long>();
            daXem.Remove(id);
            var listTk = _gameService.GetListTaiKhoanDaXem(daXem);
            return PartialView("_TaiKhoanDaXemPartial", listTk);
        }

        #endregion Partial

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

    }
}