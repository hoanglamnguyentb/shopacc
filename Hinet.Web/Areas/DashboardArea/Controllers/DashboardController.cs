using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService;


//using Hinet.Service.Common;
using Hinet.Service.DashboardService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.GiaoDichService.Dto;
using Hinet.Service.NotificationService;
using Hinet.Service.VatPhamService;
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
        private readonly IDanhMucGameService _danhMucGameService;
        private readonly IVatPhamService _vatPhamService;

        public DashboardController(
            IMapper mapper,
            ILog log,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            INotificationService notificationService,
            IDM_DulieuDanhmucService IDM_DulieuDanhmucService,
            IAppUserService appUserService,
            IDashboardService dashboardService,
            IGiaoDichService giaoDichService,
            IDanhMucGameService danhMucGameService,
            IVatPhamService vatPhamService)
        {
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _log = log;
            _mapper = mapper;
            _notificationService = notificationService;
            _IDM_DulieuDanhmucService = IDM_DulieuDanhmucService;
            _appUserService = appUserService;
            _dashboardService = dashboardService;
            _giaoDichService = giaoDichService;
            _danhMucGameService = danhMucGameService;
            _vatPhamService = vatPhamService;
        }

        // GET: DashboardArea/Dashboard

        public ActionResult Index()
        {
            return View();
            //var now = DateTime.Now;
            //var today = now.Date;
            //var tomorrow = today.AddDays(1);
            //var search = new GiaoDichSearchDto()
            //{

            //};

            //var model = new DashboardDto
            //{
            //    SummaryData = new SummaryDataDto
            //    {
            //        RevenueToday = _giaoDichService
            //            .FindBy(o => o.CreatedDate >= today
            //                         && o.CreatedDate < tomorrow
            //                         && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
            //            .Sum(o => (decimal?)o.SoTien) ?? 0,

            //        OrderCount = _giaoDichService
            //            .FindBy(o => o.CreatedDate >= today
            //                         && o.CreatedDate < tomorrow
            //                         && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
            //            .Count(),

            //        MarginPercent = _giaoDichService
            //            .FindBy(o => o.CreatedDate >= today
            //                         && o.CreatedDate < tomorrow
            //                         && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN
            //                         && (o.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACC
            //                             || o.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACCRANDOM))
            //            .Count(),

            //        DisputesCount = _giaoDichService
            //            .FindBy(o => o.CreatedDate >= today
            //                         && o.CreatedDate < tomorrow
            //                         && o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN
            //                         && o.LoaiGiaoDich == LoaiGiaoDichConstant.NAPTOPUP)
            //            .Count(),
            //    },

            //    MonthlyChart = _giaoDichService
            //        .FindBy(o => o.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
            //        .GroupBy(o => new { o.CreatedDate.Year, o.CreatedDate.Month })
            //        .Select(g => new MonthlyChartDto
            //        {
            //            Year = g.Key.Year,
            //            Month = g.Key.Month,
            //            Revenue = g.Sum(x => x.SoTien),
            //            Profit = g.Sum(x => x.SoTien * 0.4m)
            //        })
            //        .OrderBy(g => g.Year).ThenBy(g => g.Month)
            //        .ToList(),

            //    TopGames = _giaoDichService
            //        .FindBy(x => x.LoaiDoiTuong == LoaiDoiTuongConstant.TAIKHOANGAME)
            //        .GroupBy(d => d.DoiTuongId)
            //        .Select(g => new TopItemDto { Label = g.Key.ToString(), Value = g.Count() })
            //        .OrderByDescending(g => g.Value)
            //        .Take(5)
            //        .ToList(),

            //    TopItems = _giaoDichService
            //        .FindBy(x => x.LoaiDoiTuong == LoaiDoiTuongConstant.NAPTOPUP)
            //        .GroupBy(d => d.DoiTuongId)
            //        .Select(g => new TopItemDto { Label = g.Key.ToString(), Value = g.Count() })
            //        .OrderByDescending(g => g.Value)
            //        .Take(5)
            //        .ToList(),

            //    Orders = _giaoDichService.GetDaTaByPage(new searchch)
            //        .FindBy(x => x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN)
            //        .OrderByDescending(o => o.CreatedDate)
            //        .Take(50)
            //        .Select(o => new OrderTableDto
            //        {
            //            //Id = o.Id,
            //            Customer = o.CreatedBy,
            //            Total = o.SoTien,
            //            Status = o.TrangThai,
            //            Date = o.CreatedDate
            //        })
            //        .ToList(),
            //    PendingOrders = _giaoDichService
            //        .FindBy(x => x.TrangThai == TrangThaiGiaoDichConstant.CHOXULY)
            //        .OrderByDescending(o => o.CreatedDate)
            //        .Take(50)
            //        .Select(o => new OrderTableDto
            //        {
            //            Customer = o.CreatedBy,
            //            Total = o.SoTien,
            //            Status = o.TrangThai,
            //            Date = o.CreatedDate
            //        })
            //        .ToList()
            //};
            //return View(model);
        }

        public ActionResult PendingOrders(int page = 1, string search = "")
        {
            var searchDto = new GiaoDichSearchDto()
            {
                TrangThaiFilter = TrangThaiGiaoDichConstant.CHOXULY,
                KeyWord = search
            };
            var result = _giaoDichService.GetDaTaByPage(searchDto, page, 20);
            return PartialView("_PendingOrdersPartial", result);
        }

        [ChildActionOnly]
        public ActionResult KpiCards()
        {
            // tính KPI ở đây
            var now = DateTime.Now.Date;
            var tomorrow = now.AddDays(1);
            var query = _giaoDichService
                    .FindBy(x => x.CreatedDate >= now && x.CreatedDate < tomorrow && x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN);

            var kpi = new SummaryDataDto
            {
                RevenueToday = query.Sum(x => (decimal?)x.SoTien) ?? 0,
                OrderCount = query.Count(),
                MarginPercent = query.Where(x => x.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACC || x.LoaiGiaoDich == LoaiGiaoDichConstant.MUAACCRANDOM).Count(),
                DisputesCount = query.Where(x => x.LoaiGiaoDich == LoaiGiaoDichConstant.NAPTOPUP).Count(),
            };

            return PartialView("_KpiCardsPartial", kpi);
        }

        public ActionResult RevenueChart(int months = 12)
        {
            var query = _giaoDichService.FindBy(x => x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN);

            if (months > 0)
            {
                var fromDate = DateTime.Now.AddMonths(-months);
                query = query.Where(x => x.CreatedDate >= fromDate);
            }

            var data = query
                .GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
                .Select(g => new MonthlyChartDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(x => x.SoTien),
                    Profit = g.Sum(x => x.SoTien * 0.4m)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();

            ViewBag.Months = months;
            return PartialView("_RevenueChartPartial", data);
        }

        public ActionResult TopCharts(int days = 30)
        {
            var fromDate = days > 0 ? DateTime.Now.AddDays(-days) : DateTime.MinValue;

            // Lấy Top Game ID + Số lượng bán
            var topGameData = _giaoDichService
                .FindBy(x => x.LoaiDoiTuong == LoaiDoiTuongConstant.TAIKHOANGAME && x.CreatedDate >= fromDate)
                .GroupBy(g => g.DoiTuongId)
                .Select(g => new
                {
                    Id = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            var topGameIds = topGameData.Select(x => x.Id.ToString()).ToList();

            var topDMGames = _danhMucGameService
                .FindBy(x => topGameIds.Contains(x.Id.ToString()))
                .ToList();

            // Ghép dữ liệu game + số lượng
            var topGames = topDMGames
                .Select(g => new TopItemDto
                {
                    Label = g.Name, // hoặc g.Ten nếu cột tên là Ten
                    Value = topGameData.First(d => d.Id.ToString() == g.Id.ToString()).Count
                })
                .OrderByDescending(x => x.Value)
                .ToList();


            // Lấy Top Vật phẩm ID + Số lượng bán
            var topVatPhamData = _giaoDichService
                .FindBy(x => x.LoaiDoiTuong == LoaiDoiTuongConstant.NAPTOPUP && x.CreatedDate >= fromDate)
                .GroupBy(g => g.DoiTuongId)
                .Select(g => new
                {
                    Id = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            var topVatPhamIds = topVatPhamData.Select(x => x.Id.ToString()).ToList();

            var topVatPhams = _vatPhamService
                .FindBy(x => topVatPhamIds.Contains(x.Id.ToString()))
                .ToList();

            var topItems = topVatPhams
                .Select(v => new TopItemDto
                {
                    Label = v.Name, // hoặc Ten tùy bảng
                    Value = topVatPhamData.First(d => d.Id.ToString() == v.Id.ToString()).Count
                })
                .OrderByDescending(x => x.Value)
                .ToList();


            var model = new TopChartsViewModel
            {
                TopDMGames = topGames,
                TopVatpPhams = topItems,
                Days = days
            };

            return PartialView("_TopChartsPartial", model);
        }
    }
};