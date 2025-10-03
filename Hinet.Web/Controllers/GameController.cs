using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Presentation.Command;
using Hinet.Model.Entities;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Service.AppUserService;
using Hinet.Service.BannerService;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DanhMucGameTaiKhoanService;
using Hinet.Service.DichVuService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.RoleService;
using Hinet.Service.TaiKhoanService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.TinTucService;
using Hinet.Web.Filters;
using Hinet.Web.Models.GameVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    public class GameController : EndUserController
    {
        private readonly IGameService _gameService;
        private readonly IDanhMucGameTaiKhoanService _danhMucGameTaiKhoanService;
        private readonly IDanhMucGameService _danhMucGameService;
        private readonly ITaiKhoanService _taiKhoanService;

        public GameController(IGameService gameService, IDanhMucGameService danhMucGameService, ITaiKhoanService taiKhoanService, IDanhMucGameTaiKhoanService danhMucGameTaiKhoanService)
        {
            _gameService = gameService;
            _danhMucGameService = danhMucGameService;
            _taiKhoanService = taiKhoanService;
            _danhMucGameTaiKhoanService = danhMucGameTaiKhoanService;
        }

        // GET: Game
        [AllowAnonymous]
        public ActionResult Index(string slug)
        {
            var vm = new IndexVM
            {
                Game = _gameService.GetBySlug(slug),
                ListDanhMucGame = _gameService.GetListDanhMucGameBySlug(slug),
            };
            return View(vm);
        }

        // GET: Game
        [AllowAnonymous]
        public ActionResult DanhMuc(string slug, TaiKhoanSearchDto search, int page = 1, int pageSize = 4)
        {
            var danhMuc = _danhMucGameService.GetBySlug(slug);
            var game = _gameService.GetById(danhMuc.GameId);
            var vm = new DanhMucGameVM
            {
                Game = game,
                DanhMucGame = danhMuc,
                TaiKhoanPagedResult = _gameService.GetTaiKhoanPagedByDanhMucSlug(slug, search, page, pageSize)
            };
            return View(vm);
        }

        [AllowAnonymous]
        public ActionResult ChiTietTaiKhoan(string code)
        {
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
            return View(tk);
        }


        [AllowAnonymous]
        public ActionResult LoadTaiKhoan(string slug, TaiKhoanSearchDto search, int page = 1, int pageSize = 4)
        {
            var result = _gameService.GetTaiKhoanPagedByDanhMucSlug(slug, search, page, pageSize);
            var danhMuc = _danhMucGameService.GetBySlug(slug);
            var game = _gameService.GetById(danhMuc.GameId);
            ViewData["DanhMuc"] = danhMuc;
            ViewData["Game"] = game;
            return PartialView("_TaiKhoanList", result);
        }

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
            var listTk = _gameService.GetListTaiKhoanDaXem(daXem);
            return PartialView("_TaiKhoanDaXemPartial", listTk);
        }

        #endregion Partial

    }
}