using Hinet.Model.Entities;
using Hinet.Service.Common;
using System;

namespace Hinet.Service.DashboardService
{
    public interface IDashboardService : IEntityService<Deposit>
    {
        DashboardDto GetDashboardData(DateTime? startDate = null, DateTime? endDate = null, string period = "day");
    }
}