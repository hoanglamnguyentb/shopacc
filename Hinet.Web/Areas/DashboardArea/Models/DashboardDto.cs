using Hinet.Service.GiaoDichService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.DashboardArea.Models
{
    public class DashboardDto
    {
        public SummaryDataDto SummaryData { get; set; }
        public List<MonthlyChartDto> MonthlyChart { get; set; }
        public List<TopItemDto> TopGames { get; set; }
        public List<TopItemDto> TopItems { get; set; }
        public List<GiaoDichDto> Orders { get; set; }
        public List<GiaoDichDto> PendingOrders { get; set; }
    }

    public class SummaryDataDto
    {
        public decimal RevenueToday { get; set; }
        public int OrderCount { get; set; }
        public int MarginPercent { get; set; }
        public int DisputesCount { get; set; }
    }

    public class MonthlyChartDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
    }

    public class TopItemDto
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }

    public class OrderTableDto
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }

}