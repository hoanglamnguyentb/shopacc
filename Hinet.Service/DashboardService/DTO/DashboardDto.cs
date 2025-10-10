using System.Collections.Generic;

namespace Hinet.Service.Common
{
    //public class DashboardDto
    //{
    //    public SummaryData SummaryData { get; set; }
    //    public ChartData<int> RevenueData { get; set; }
    //    public ChartData<int> SoldAccountData { get; set; }
    //    public ChartData<int> NewAccountData { get; set; }
    //    public List<TopDeposit> LoadTopDeposits { get; set; }
    //}
    public class DashboardDto
    {
        public SummaryData SummaryData { get; set; }
        public string RevenueData { get; set; }
        public string SoldAccountData { get; set; }
        public string NewAccountData { get; set; }
        public List<TopDeposit> TopDeposits { get; set; }
    }

    public class SummaryData
    {
        public decimal TotalRevenue { get; set; }
        public int TotalAccountSold { get; set; }
        public int TotalNewAccount { get; set; }
        public decimal TotalDeposit { get; set; }
    }

    public class ChartData<T>
    {
        public List<string> Labels { get; set; }
        public List<T> Data { get; set; }
    }
    
    public class TopDeposit
    {
        public string UserName { get; set; }
        public double TotalDeposit { get; set; }
        public int NumOfDeposit { get; set; }
    }

    public class ChartPoint
    {
        public string Label { get; set; }
        public decimal Value { get; set; }
    }
}
