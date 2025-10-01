using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.BannerService;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DichVuService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.RoleService;
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
        private readonly IDanhMucGameService _danhMucGameService;

        public GameController(IGameService gameService, IDanhMucGameService danhMucGameService)
        {
            _gameService = gameService;
            _danhMucGameService = danhMucGameService;
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
        public ActionResult DanhMuc(string slug, TaiKhoanSearchDto search)
        {
            var danhMucGame = _danhMucGameService.FindBy(x => x.Slug == slug).FirstOrDefault();
            var game = _gameService.GetById(danhMucGame.GameId);
            var vm = new DanhMucGameVM
            {
                Game = game,
                DanhMucGame = danhMucGame,
                TaiKhoanPagedResult = _gameService.GetTaiKhoanPagedByDanhMucSlug(slug, search)
            };
            return View(vm);
        }

        [AllowAnonymous]
        public PartialViewResult DanhMucGameKhac(int id)
        {
            var danhMucGameKhac = _gameService.GetListDanhMucGameKhac(id, 10);
            return PartialView("_DanhMucGameKhacPartial", danhMucGameKhac);
        }
    }
}