using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.BannerService;
using Hinet.Service.DichVuService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.RoleService;
using Hinet.Service.TinTucService;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using log4net;
using System.Collections.Generic;
using System.Linq;
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

        public HomeController(
                IDM_DulieuDanhmucService dM_DulieuDanhmucService,
                IMapper mapper, ILog iLog,
                IDM_DulieuDanhmucService DM_DulieuDanhmucService,
                IAppUserService appUserService, IDichVuService dichVuService, IBannerService bannerService, IGameService gameService, ITinTucService tinTucService)
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
        }

        [AllowAnonymous]
		public ActionResult Index()
		{
			var homeVM = new HomeVM();
			homeVM.ListDichVu = _dichVuService.GetAll().OrderBy(x => x.STT).ToList();
			homeVM.ListBanner = _bannerService.GetAll().OrderBy(x => x.STT).ToList();
			homeVM.ListGame = _gameService.GetListGame();
			homeVM.ListTinTuc = _tinTucService.GetAll().OrderByDescending(x => x.CreatedDate).Take(3).ToList();
            return View(homeVM);
		}

		//nạp thẻ
		[AllowAnonymous]
		public ActionResult Recharge()
		{
			return View();
		}
		//tài khoản
		[AllowAnonymous]
		public ActionResult Accounts()
		{
			return View();
		}
		//dịch vụ
		[AllowAnonymous]
		public ActionResult Services()
		{
			return View();
		}
		//tin tức
		[AllowAnonymous]
		public ActionResult News()
		{
			return View();
		}
		
		//tin tức
		[AllowAnonymous]
		public ActionResult AccountDetail()
		{
			return View();
		}
        
    }
}