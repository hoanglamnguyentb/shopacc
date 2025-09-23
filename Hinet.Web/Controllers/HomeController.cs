using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.RoleService;
using Hinet.Web.Filters;
using log4net;
using System.Collections.Generic;
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

		public HomeController(
				IDM_DulieuDanhmucService dM_DulieuDanhmucService,
				IMapper mapper, ILog iLog,
				IDM_DulieuDanhmucService DM_DulieuDanhmucService,
				IAppUserService appUserService)
		{
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_mapper = mapper;
			_Ilog = iLog;
			_DM_DulieuDanhmucService = DM_DulieuDanhmucService;
			_appUserService = appUserService;
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult UnAuthor()
		{
			return View();
		}

		[AllowAnonymous]
		public PartialViewResult UnAuthorPartial()
		{
			return PartialView("UnAuthorPartial");
		}

		[AllowAnonymous]
		public PartialViewResult TimeOutSession()
		{
			return PartialView("TimeOutSession");
		}

		[AllowAnonymous]
		public ActionResult NotFound()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult License()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult LicensePartial()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult Error()
		{
			return View();
		}
	}
}