using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.NotificationService.Dto;
using System.Collections.Generic;

namespace Hinet.Web.Areas.DashboardArea.Models
{
    public class DetailVM
    {
        public PageListResultBO<NotificationDto> ObjNoti { get; set; }

        public DashboardCountNhanSu CountDataNhanSu { get; set; }

        public List<UserDto> lstinfoNhanSu { get; set; }
        public List<long?> lstUser { get; set; }
    }

    public class DashboardCountNhanSu
    {
        public int NhanSuChuaChinhThuc { get; set; }
        public int NhanSuDangLamViec { get; set; }
        public int NhanSuDaNghiViec { get; set; }
        public int TongSo { get; set; }
    }
}