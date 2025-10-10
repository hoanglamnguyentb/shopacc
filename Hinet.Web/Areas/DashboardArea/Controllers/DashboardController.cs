using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.DashboardService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.NotificationService;
using Hinet.Web.Areas.UserArea.Models;
using Hinet.Web.Filters;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DashboardArea.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly ILog _log;
		public const string permissionIndexSoTTTT = "Dashboard_indexSoTTTT";
		public const string permissionIndexDN = "Dashboard_indexDN";
		private readonly INotificationService _notificationService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IDM_DulieuDanhmucService _IDM_DulieuDanhmucService;
		private readonly IAppUserService _appUserService;
		private string searchGiamSat = "searchGiamSat";
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IMapper mapper,
            ILog log,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            INotificationService notificationService,
            IDM_DulieuDanhmucService IDM_DulieuDanhmucService,
            IAppUserService appUserService,
            IDashboardService dashboardService)
        {
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _log = log;
            _mapper = mapper;
            _notificationService = notificationService;
            _IDM_DulieuDanhmucService = IDM_DulieuDanhmucService;
            _appUserService = appUserService;
            _dashboardService = dashboardService;
        }

        // GET: DashboardArea/Dashboard

        public ActionResult Index(DateTime? startDate = null, DateTime? endDate = null, string period = "day")
		{
            var viewData = _dashboardService.GetDashboardData(startDate, endDate, period);
            return View(viewData);
		}
        

	}
};