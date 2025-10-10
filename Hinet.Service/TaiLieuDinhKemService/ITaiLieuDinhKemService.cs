using Hinet.Model.Entities;
using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.Web;

namespace Hinet.Service.TaiLieuDinhKemService
{
	public interface ITaiLieuDinhKemService : IEntityService<TaiLieuDinhKem>
	{
		List<TaiLieuDinhKem> GetListTaiLieuAllByType(string LoaiTaiLieu, long itemId);

		TaiLieuDinhKem GetTaiLieuFirstByType(string LoaiTaiLieu, long itemId);

		JsonResultBO SaveMultiFile(string ITEM_TYPE, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<string> NameFile, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID);

		JsonResultBO SaveMultiFile2(string ITEM_TYPE, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<DateTime?> NgayKiemDinhs, List<DateTime?> NgayHetHans, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID);

		JsonResultBO SaveMultiFileV3(string ITEM_TYPE, List<string> soKyHieus, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<DateTime?> NgayKiemDinhs, List<DateTime?> NgayHetHans, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID);

		JsonResultBO SaveMultiFile4(string ITEM_TYPE, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<String> LoaiKiemDinhs, List<DateTime?> NgayKiemDinhs, List<DateTime?> NgayHetHans, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID);
	}
}