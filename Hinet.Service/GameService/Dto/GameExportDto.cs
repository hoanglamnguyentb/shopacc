using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Hinet.Service.GameService.Dto
{
    public class GameExportDto
    {
		[DisplayName("Tên")]
public string Name { get; set; }
		[DisplayName("Mô tả")]
public string MoTa { get; set; }
		[DisplayName("Trạng thái")]
public string TrangThai { get; set; }

    }
}