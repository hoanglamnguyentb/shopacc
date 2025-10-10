using Hinet.Service.Common;
using System;

namespace Hinet.Service.DashboardService.DTO
{
    public class DashboardSearchDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Period { get; set; }
    }
}