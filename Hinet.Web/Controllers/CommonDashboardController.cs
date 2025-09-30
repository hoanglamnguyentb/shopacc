using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DM_DulieuDanhmucService.DTO;
using Hinet.Service.NotificationService;
using Hinet.Web.Filters;
using Hinet.Web.Modules;
using log4net;
using Newtonsoft.Json;
using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
	public class CommonDashboardController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly ILog _log;
		private readonly INotificationService _notificationService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IDM_DulieuDanhmucService _IDM_DulieuDanhmucService;
		private readonly IAppUserService _appUserService;

		public CommonDashboardController(
			IMapper mapper,
			ILog log,
			IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			INotificationService notificationService,
			IAppUserService appUserService
			)
		{
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_log = log;
			_mapper = mapper;
			_notificationService = notificationService;
			_appUserService = appUserService;
		}

		// GET: CommonDashboard
		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult Map()
		{
			var sysConfig = _dM_DulieuDanhmucService.GetCauHinhHeThong();
			ViewBag.MaTinh = sysConfig.MaTinh;
			ViewBag.TenTinh = sysConfig.TenTinh;

			var filePath = Server.MapPath("~/Uploads/Geojson/ProvinceVietNam.json");
			var jsonData = System.IO.File.ReadAllText(filePath);
			ViewBag.JsonData = jsonData;

			//danh sách huyện theo tỉnh
			//ViewBag.DataHuyen = _qLTramQuanTracService.GetTramQuanTracTheoHuyen();
			return View();
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult GoToURL(string url)
		{
			// Điều hướng đến action trong controller khác và truyền tham số
			return RedirectToAction("Login", "AccountAdmin", new { returnUrl = url });
		}

		[HttpGet]
		public PartialViewResult GetMapFooter()
		{
			CauHinhHeThong sysConfig = new CauHinhHeThong();
			var key = "SysConfig";
			var redisEnabled = WebConfigurationManager.AppSettings["RedisEnabled"] == "1";
			try
			{
				if (redisEnabled)
				{
					var cacheHelper = new CacheStack();
					if (!cacheHelper.IsKeyExists(key))
					{
						sysConfig = _dM_DulieuDanhmucService.GetCauHinhHeThong();
						var configType = typeof(CauHinhHeThong);
						var jsonConfig = JsonConvert.SerializeObject(sysConfig);
						cacheHelper.SetStrings(key, jsonConfig);
					}
					else
					{
						sysConfig = JsonConvert.DeserializeObject<CauHinhHeThong>(cacheHelper.GetStrings(key));
					}
				}
			}
			catch (Exception ex)
			{
				sysConfig = new CauHinhHeThong();
			}
			return PartialView("_MapFooter", sysConfig);
		}
	}
}