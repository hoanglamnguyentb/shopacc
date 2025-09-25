using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hinet.Service.Common;
using Hinet.Service.Constant;

namespace Hinet.Service.TinTucService.Dto
{
	public class TinTucDto : TinTuc
	{
		public string TrangThaiTxt
		{
			get
			{
				return ConstantExtension.GetName<TrangThaiTinTucConstant>(TrangThai);
			}
		}
	}
}