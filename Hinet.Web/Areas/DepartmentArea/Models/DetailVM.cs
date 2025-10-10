using Hinet.Service.DepartmentService.DTO;
using Hinet.Service.HistoryService.Dto;
using System.Collections.Generic;

namespace Hinet.Web.Areas.DepartmentArea.Models
{
    public class DetailVM
    {
        public DepartmentInfoDto ObjInfo { get; set; }
        public List<HistoryDto> historyDtos { get; set; }
    }
}