using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DM_DulieuDanhmucService.DTO;
using Hinet.Service.NotificationService;
using Hinet.Web.Filters;
using log4net;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DashboardArea.Controllers
{
	public class CommonShowController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly ILog _log;

		private readonly INotificationService _notificationService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IDM_DulieuDanhmucService _IDM_DulieuDanhmucService;
		private readonly IAppUserService _appUserService;
		private const string KeySessionTableShowData = "KeySessionTableShowData";

		//IConnectionMultiplexer _connectionMultiplexer;
		//IDatabase _cacheDatabase;
		public CommonShowController(
			IMapper mapper,
			ILog log,
			IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			INotificationService notificationService,
			IDM_DulieuDanhmucService IDM_DulieuDanhmucService,
			IAppUserService appUserService)
		{
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_log = log;
			_mapper = mapper;
			_notificationService = notificationService;
			_IDM_DulieuDanhmucService = IDM_DulieuDanhmucService;
			_appUserService = appUserService;
		}

		// GET: DashboardArea/Common
		public ActionResult Index()
		{
			return View();
		}

		public PartialViewResult ShowValueTable(string tableName, string key, string text)
		{
			var searchModel = new ShowValueTableSVM();
			searchModel.TableName = tableName;
			searchModel.Value = key;
			searchModel.Text = text;
			searchModel.pageIndex = 1;
			searchModel.pageSize = 20;
			var result = new PageListResultBO<SelectListItem>();
			SessionManager.SetValue(KeySessionTableShowData, searchModel);
			result = _dM_DulieuDanhmucService.GetDataToShowImportCategory(searchModel);
			return PartialView(result);
		}

		public PartialViewResult ShowValueDanhMuc(string danhmuc)
		{
			var data = _dM_DulieuDanhmucService.GetDropdownlistCode(danhmuc, null);
			return PartialView(data);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult SearchShowValueTable(ShowValueTableSVM form)
		{
			var searchModel = SessionManager.GetValue(KeySessionTableShowData) as ShowValueTableSVM;

			if (searchModel == null)
			{
				searchModel = new ShowValueTableSVM();
				searchModel.pageSize = 20;
			}
			searchModel.GiaTriHienThiFilter = form.GiaTriHienThiFilter;
			searchModel.GiaTriNhapFilter = form.GiaTriNhapFilter;

			SessionManager.SetValue(KeySessionTableShowData, searchModel);

			var result = _dM_DulieuDanhmucService.GetDataToShowImportCategory(searchModel);
			return Json(result);
		}

		[HttpPost]
		public JsonResult getDataShowValueTable(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(KeySessionTableShowData) as ShowValueTableSVM;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new ShowValueTableSVM();
				}
				searchModel.sortQuery = sortQuery;
			}
			if (pageSize > 0)
			{
				searchModel.pageSize = pageSize;
			}
			searchModel.pageIndex = indexPage;
			SessionManager.SetValue(KeySessionTableShowData, searchModel);
			var data = _dM_DulieuDanhmucService.GetDataToShowImportCategory(searchModel);
			return Json(data);
		}
	}
}