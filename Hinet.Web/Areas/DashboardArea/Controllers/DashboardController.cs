using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;

//using Hinet.Service.Common;
using Hinet.Service.DashboardService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.NotificationService;
using Hinet.Web.Areas.DashboardArea.Models;
using Hinet.Web.Areas.UserArea.Models;
using Hinet.Web.Filters;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
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
        private readonly IGiaoDichService _giaoDichService;

        public DashboardController(
            IMapper mapper,
            ILog log,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            INotificationService notificationService,
            IDM_DulieuDanhmucService IDM_DulieuDanhmucService,
            IAppUserService appUserService,
            IDashboardService dashboardService,
            IGiaoDichService giaoDichService)
        {
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _log = log;
            _mapper = mapper;
            _notificationService = notificationService;
            _IDM_DulieuDanhmucService = IDM_DulieuDanhmucService;
            _appUserService = appUserService;
            _dashboardService = dashboardService;
            _giaoDichService = giaoDichService;
        }

        // GET: DashboardArea/Dashboard

        public ActionResult Index()
        {
            var now = DateTime.Now;
            var today = now.Date;
            var tomorrow = today.AddDays(1);

            var model = new DashboardDto
            {
                SummaryData = new SummaryDataDto
                {
                    RevenueToday = _giaoDichService
                        .FindBy(o => o.CreatedDate >= today
                                     && o.CreatedDate < tomorrow
                                     && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                        .Sum(o => (decimal?)o.SoTien) ?? 0,

                    OrderCount = _giaoDichService
                        .FindBy(o => o.CreatedDate >= today
                                     && o.CreatedDate < tomorrow
                                     && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                        .Count(),

                    MarginPercent = _giaoDichService
                        .FindBy(o => o.CreatedDate >= today
                                     && o.CreatedDate < tomorrow
                                     && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN
                                     && (o.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACC
                                         || o.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACCRANDOM))
                        .Count(),

                    DisputesCount = _giaoDichService
                        .FindBy(o => o.CreatedDate >= today
                                     && o.CreatedDate < tomorrow
                                     && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN
                                     && o.LoaiGiaoDich == LoaiGiaoDichConstant.NAPTOPUP)
                        .Count(),
                },

                MonthlyChart = _giaoDichService
                    .FindBy(o => o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                    .GroupBy(o => new { o.CreatedDate.Year, o.CreatedDate.Month })
                    .Select(g => new MonthlyChartDto
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        Revenue = g.Sum(x => x.SoTien),
                        Profit = g.Sum(x => x.SoTien * 0.4m)
                    })
                    .OrderBy(g => g.Year).ThenBy(g => g.Month)
                    .ToList(),

                TopGames = _giaoDichService
                    .FindBy(x => x.LoaiDoiTuong == LoaiDoiTuongConstant.TAIKHOANGAME)
                    .GroupBy(d => d.DoiTuongId)
                    .Select(g => new TopItemDto { Label = g.Key.ToString(), Value = g.Count() })
                    .OrderByDescending(g => g.Value)
                    .Take(5)
                    .ToList(),

                TopItems = _giaoDichService
                    .FindBy(x => x.LoaiDoiTuong == LoaiDoiTuongConstant.NAPTOPUP)
                    .GroupBy(d => d.DoiTuongId)
                    .Select(g => new TopItemDto { Label = g.Key.ToString(), Value = g.Count() })
                    .OrderByDescending(g => g.Value)
                    .Take(5)
                    .ToList(),

                Orders = _giaoDichService
                    .FindBy(x => x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
                    .OrderByDescending(o => o.CreatedDate)
                    .Take(50)
                    .Select(o => new OrderTableDto
                    {
                        //Id = o.Id,
                        Customer = o.CreatedBy,
                        Total = o.SoTien,
                        Status = o.TrangThai,
                        Date = o.CreatedDate
                    })
                    .ToList(),
                PendingOrders = _giaoDichService
                    .FindBy(x => x.TrangThai == TrangThaiGiaoDichConstant.CHOXULY)
                    .OrderByDescending(o => o.CreatedDate)
                    .Take(50)
                    .Select(o => new OrderTableDto
                    {
                        Customer = o.CreatedBy,
                        Total = o.SoTien,
                        Status = o.TrangThai,
                        Date = o.CreatedDate
                    })
                    .ToList()
            };
            return View(model);
        }
    }
};