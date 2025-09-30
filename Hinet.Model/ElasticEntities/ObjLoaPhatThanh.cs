using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinet.Model.ElasticEntities
{
	public class ObjLoaPhatThanh : ElasticEntity
	{
		public Guid GuidId { get; set; }
		public long ItemId { get; set; }
		public string Type { get; set; }
		public string TenDoiTuong { get; set; }
		public string DiaChi { get; set; }
		public string TrangThai { get; set; }
		public string AnhDaiDien { get; set; }
		public string Lng { get; set; }
		public string Lat { get; set; }
		public string PathAvata { get; set; }
		public string NoiDung { get; set; }
		public string PathMarker { get; set; }

    }
}
