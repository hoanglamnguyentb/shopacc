using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.AppUserRepository;
using Hinet.Repository.DepositRepository;
using Hinet.Repository.GiaoDichRepository;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using log4net;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;      
using System.Linq.Dynamic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace Hinet.Service.DashboardService
{
    public class DashboardService : EntityService<Deposit>, IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepositRepository _DepositRepository;
        private readonly ILog _loger;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ITaiKhoanRepository _gameAccountRepository;
        private readonly IGiaoDichRepository _transactionRepository;
        public DashboardService(
            IUnitOfWork unitOfWork,
            IDepositRepository DepositRepository,
            ILog loger,
            IMapper mapper,
            IAppUserRepository appUserRepository,
            ITaiKhoanRepository gameAccountRepository,
            IGiaoDichRepository transactionRepository)
        : base(unitOfWork, DepositRepository)
        {
            _unitOfWork = unitOfWork;
            _DepositRepository = DepositRepository;
            _loger = loger;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _gameAccountRepository = gameAccountRepository;
            _transactionRepository = transactionRepository;
        }

        public DashboardDto GetDashboardData(DateTime? startDate = null, DateTime? endDate = null, string period = "day")
        {
            startDate = startDate ?? DateTime.Now.AddDays(-30).Date;
            endDate = endDate ?? DateTime.Now;
            var summaryData = GetSummaryData(startDate.Value, endDate.Value);
            var chartData = LoadChartData(startDate.Value, endDate.Value, period);
            var topDeposit = GetTopDeposits(startDate.Value, endDate.Value);
            return new DashboardDto
            {
                SummaryData = summaryData,
                RevenueData = chartData.Item1,
                SoldAccountData = chartData.Item2,
                NewAccountData = chartData.Item3,
                TopDeposits = topDeposit
            };
        }

        //thống kê chung
        private SummaryData GetSummaryData(DateTime startDate, DateTime endDate)
        {
            // Tính tổng doanh thu
            var transQuery = _transactionRepository
                .GetAllAsQueryable()
                .Where(t => t.CreatedDate >= startDate && t.CreatedDate <= endDate);

            var totalRevenue = transQuery
                .Where(t => t.LoaiGiaoDich == LoaiGiaoDichConstant.MUA)
                .Sum(t => (int?)Math.Abs(t.SoTien)) ?? 0;

            //// Đếm số tài khoản bán ra
            var totalAccountSold = transQuery
                .Count(t => t.LoaiDoiTuong == "TaiKhoan");

            //// Đếm số tài khoản thêm mới
            var totalNewAccount = _gameAccountRepository .GetAllAsQueryable()
                .Count(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate);

            //// Tính tổng nạp tiền 
            var totalDeposit = transQuery
                .Where(t => t.LoaiGiaoDich == LoaiGiaoDichConstant.NAP)
                .Sum(t => (int?)Math.Abs(t.SoTien)) ?? 0;

            return new SummaryData
            {
                TotalRevenue = (decimal)totalRevenue,
                TotalAccountSold = totalAccountSold,
                TotalNewAccount = totalNewAccount,
                TotalDeposit = (decimal)totalDeposit
            };
        }

        //biểu đồ
        private Tuple<string, string, string> LoadChartData(DateTime startDate, DateTime endDate, string period)
        {
            var transQuery = _transactionRepository
                .GetAllAsQueryable()
                .Where(t => t.CreatedDate >= startDate && t.CreatedDate <= endDate)
                .AsEnumerable(); 

            var soldAccountQuery = _transactionRepository
                .GetAllAsQueryable()
                .Where(t => t.CreatedDate >= startDate && t.CreatedDate <= endDate && t.LoaiDoiTuong == "TaiKhoan")
                .AsEnumerable(); 

            var gameAccountQuery = _gameAccountRepository
                .GetAllAsQueryable()
                .Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate )
                .AsEnumerable();

            // Lấy dữ liệu doanh thu theo thời gian
            //var revenueQuery = _context.Orders
            //    .Where(o => o.CreatedDate >= StartDate && o.CreatedDate <= EndDate && o.Status == "Completed")
            //    .AsEnumerable();

            //// Lấy dữ liệu số acc bán ra theo thời gian
            //var soldAccountQuery = _context.Orders
            //    .Where(o => o.CreatedDate >= StartDate && o.CreatedDate <= EndDate && o.Status == "Completed")
            //    .AsEnumerable();

            //// Lấy dữ liệu số acc thêm mới theo thời gian
            //var newAccountQuery = _context.Accounts
            //    .Where(a => a.CreatedDate >= StartDate && a.CreatedDate <= EndDate)
            //    .AsEnumerable();

            // Nhóm dữ liệu theo chu kỳ
            var revenueGrouped = GroupByPeriod(transQuery.Select(o => new { o.CreatedDate, Amount = o.SoTien }), period);
            var soldAccountGrouped = GroupByPeriod(soldAccountQuery.Select(o => new { o.CreatedDate, Amount = 1m }), period);
            var newAccountGrouped = GroupByPeriod(gameAccountQuery.Select(a => new { a.CreatedDate, Amount = 1m }), period);
            //var revenueGrouped = GroupByPeriod(revenueQuery.Select(o => new { o.CreatedDate, Amount = o.TotalAmount }));
            //var soldAccountGrouped = GroupByPeriod(soldAccountQuery.Select(o => new { o.CreatedDate, Amount = 1m }));
            //var newAccountGrouped = GroupByPeriod(newAccountQuery.Select(a => new { a.CreatedDate, Amount = 1m }));

            // Chuyển đổi sang JSON cho Chart.js
            var revenueChartData = JsonConvert.SerializeObject(new
            {
                labels = revenueGrouped.Select(x => x.Label).ToList(),
                data = revenueGrouped.Select(x => x.Value).ToList()
            });

            var soldAccountChartData = JsonConvert.SerializeObject(new
            {
                labels = soldAccountGrouped.Select(x => x.Label).ToList(),
                data = soldAccountGrouped.Select(x => x.Value).ToList()
            });

            var newAccountChartData = JsonConvert.SerializeObject(new
            {
                labels = newAccountGrouped.Select(x => x.Label).ToList(),
                data = newAccountGrouped.Select(x => x.Value).ToList()
            });
            return Tuple.Create(revenueChartData, soldAccountChartData, newAccountChartData);
        }


        //top nạp tiền
        private List<TopDeposit> GetTopDeposits(DateTime startDate, DateTime endDate)
        {
            var userQuery = _appUserRepository
                .GetAllAsQueryable()
                .AsNoTracking();

            var depositQuery = _DepositRepository
                .GetAllAsQueryable()
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate && x.Status == DepositConstant.SUCCESS)
                .AsNoTracking();

            var query = (from d in depositQuery
                        join u in userQuery on d.UserId equals u.Id
                        group d by new { d.UserId, u.UserName, u.Email } into g
                        select new TopDeposit
                        {
                            UserName = g.Key.UserName,
                            TotalDeposit = (double)g.Sum(x => x.Amount),
                            NumOfDeposit = g.Count()
                        })
                        .OrderByDescending(x => x.TotalDeposit)
                        .Take(10);
            return query.ToList();
        }

        private int GetWeekNumber(DateTime date)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var calendar = culture.Calendar;
            var calendarWeekRule = culture.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            return calendar.GetWeekOfYear(date, calendarWeekRule, firstDayOfWeek);
        }

        private List<ChartPoint> GroupByPeriod(IEnumerable<dynamic> data, string period)
        {
            var result = new List<ChartPoint>();

            switch (period.ToLower())
            {
                case "week":
                    result = data
                        .GroupBy(x => new
                        {
                            Year = ((DateTime)x.CreatedDate).Year,
                            Week = GetWeekNumber((DateTime)x.CreatedDate)
                        })
                        .Select(g => new ChartPoint
                        {
                            Label = $"Tuần {g.Key.Week}/{g.Key.Year}",
                            Value = g.Sum(x => (decimal)x.Amount)
                        })
                        .OrderBy(x => x.Label)
                        .ToList();
                    break;

                case "month":
                    result = data
                        .GroupBy(x => new
                        {
                            Year = ((DateTime)x.CreatedDate).Year,
                            Month = ((DateTime)x.CreatedDate).Month
                        })
                        .Select(g => new ChartPoint
                        {
                            Label = $"{g.Key.Month:00}/{g.Key.Year}",
                            Value = g.Sum(x => (decimal)x.Amount)
                        })
                        .OrderBy(x => x.Label)
                        .ToList();
                    break;

                default: // day
                    result = data
                        .GroupBy(x => ((DateTime)x.CreatedDate).Date)
                        .Select(g => new ChartPoint
                        {
                            Label = g.Key.ToString("dd/MM/yyyy"),
                            Value = g.Sum(x => (decimal)x.Amount)
                        })
                        .OrderBy(x => x.Label)
                        .ToList();
                    break;
            }

            return result;
        }
    }
}