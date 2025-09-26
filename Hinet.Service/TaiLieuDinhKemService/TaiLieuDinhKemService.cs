using AutoMapper;
using CommonHelper.String.HTML;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.TaiLieuDinhKemRepository;
using Hinet.Service.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace Hinet.Service.TaiLieuDinhKemService
{
	public class TaiLieuDinhKemService : EntityService<TaiLieuDinhKem>, ITaiLieuDinhKemService
	{
		private ITaiLieuDinhKemRepository _taiLieuDinhKemRepository;
		private ILog _logger;
		private IMapper _mapper;

		public TaiLieuDinhKemService(IUnitOfWork unitOfWork,
			ITaiLieuDinhKemRepository taiLieuDinhKemRepository,
			ILog logger,
			IMapper mapper
			) : base(unitOfWork, taiLieuDinhKemRepository)
		{
			_taiLieuDinhKemRepository = taiLieuDinhKemRepository;
			_logger = logger;
			_mapper = mapper;
		}

		public List<TaiLieuDinhKem> GetListTaiLieuAllByType(string LoaiTaiLieu, long itemId)
		{
			var query = _taiLieuDinhKemRepository.GetAllAsQueryable()
				.Where(x => x.Item_ID == itemId && x.LoaiTaiLieu == LoaiTaiLieu)
				.OrderByDescending(x => x.Id)
				.ToList();
			return query;
		}

		public TaiLieuDinhKem GetTaiLieuFirstByType(string LoaiTaiLieu, long itemId)
		{
			var query = _taiLieuDinhKemRepository.GetAllAsQueryable().Where(x => x.Item_ID == itemId && x.LoaiTaiLieu == LoaiTaiLieu).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
			return query;
		}

		public JsonResultBO SaveMultiFile(string ITEM_TYPE, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<string> NameFile, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID)
		{
			var result = new JsonResultBO(true);
			if (lstFile != null && lstFile.Any())
			{
				for (int i = 0; i < lstFile.Count; i++)
				{
					if (lstFile[i] != null)
					{
						var resultSave = UploadProvider.SaveFile(lstFile[i], lstFile[i].FileName, AllowExtention, maxSize, folder, pathSave);
						if (resultSave.status)
						{
							var obj = new TaiLieuDinhKem();
							obj.TenTaiLieu = lstFile[i].FileName;
							var arrName = lstFile[i].FileName.Split('.');
							var extention = '.' + arrName[arrName.Length - 1];
							obj.DinhDangFile = extention;
							obj.DuongDanFile = resultSave.path;
							obj.Item_ID = ITEM_ID;
							obj.LoaiTaiLieu = ITEM_TYPE;
							_taiLieuDinhKemRepository.Add(obj);
							_taiLieuDinhKemRepository.Save();
						}
						else
						{
							result.Status = false;
							result.Message += "Tệp " + (i + 1) + " " + resultSave.message + "<br/>";
						}
					}
				}
			}

			return result;
		}

		public JsonResultBO SaveMultiFile2(string ITEM_TYPE, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<DateTime?> NgayKiemDinhs, List<DateTime?> NgayHetHans, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID)
		{
			var result = new JsonResultBO(true);
			if (lstFile != null && lstFile.Any())
			{
				for (int i = 0; i < lstFile.Count; i++)
				{
					if (lstFile[i] != null)
					{
						var resultSave = UploadProvider.SaveFile(lstFile[i], "", AllowExtention, maxSize, folder, pathSave);
						if (resultSave.status)
						{
							var obj = new TaiLieuDinhKem();
							obj.TenTaiLieu = lstFile[i].FileName;
							var arrName = lstFile[i].FileName.Split('.');
							var extention = '.' + arrName[arrName.Length - 1];
							obj.DinhDangFile = extention;
							obj.DuongDanFile = resultSave.path;
							obj.MoTa = extention;
							obj.Item_ID = ITEM_ID;
							obj.LoaiTaiLieu = ITEM_TYPE;
							_taiLieuDinhKemRepository.Add(obj);
							_taiLieuDinhKemRepository.Save();
						}
						else
						{
							result.Status = false;
							result.Message = resultSave.message;
						}
					}
				}
			}

			return result;
		}

		public JsonResultBO SaveMultiFileV3(string ITEM_TYPE, List<string> soKyHieus, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<DateTime?> NgayKiemDinhs, List<DateTime?> NgayHetHans, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID)
		{
			var result = new JsonResultBO(true);
			try
			{
				if (lstFile != null && lstFile.Any())
				{
					for (int i = 0; i < lstFile.Count; i++)
					{
						if (lstFile[i] != null)
						{
							var resultSave = UploadProvider.SaveFile(lstFile[i], "", AllowExtention, maxSize, folder, pathSave);
							if (resultSave.status)
							{
								var obj = new TaiLieuDinhKem();
								obj.TenTaiLieu = lstFile[i].FileName;
								var arrName = lstFile[i].FileName.Split('.');
								var extention = '.' + arrName[arrName.Length - 1];
								obj.DinhDangFile = extention;
								obj.DuongDanFile = resultSave.path;
								obj.Item_ID = ITEM_ID;
								obj.LoaiTaiLieu = ITEM_TYPE;
								_taiLieuDinhKemRepository.Add(obj);
								_taiLieuDinhKemRepository.Save();
							}
							else
							{
								result.Status = false;
								result.Message = resultSave.message;
							}
						}
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				result.Status = false;
				return result;
			}
		}

		public JsonResultBO SaveMultiFile4(string ITEM_TYPE, long ITEM_ID, List<HttpPostedFileBase> lstFile, List<String> LoaiKiemDinhs, List<DateTime?> NgayKiemDinhs, List<DateTime?> NgayHetHans, string AllowExtention, long? maxSize, string folder, string pathSave, long? userID)
		{
			var result = new JsonResultBO(true);
			if (lstFile != null && lstFile.Any())
			{
				for (int i = 0; i < lstFile.Count; i++)
				{
					if (lstFile[i] != null)
					{
						var resultSave = UploadProvider.SaveFile(lstFile[i], "", AllowExtention, maxSize, folder, pathSave);
						if (resultSave.status)
						{
							var obj = new TaiLieuDinhKem();
							obj.TenTaiLieu = lstFile[i].FileName;
							var arrName = lstFile[i].FileName.Split('.');
							var extention = '.' + arrName[arrName.Length - 1];
							obj.DinhDangFile = extention;
							obj.DuongDanFile = resultSave.path;
							obj.MoTa = extention;
							obj.Item_ID = ITEM_ID;
							obj.LoaiTaiLieu = ITEM_TYPE;
							_taiLieuDinhKemRepository.Add(obj);
							_taiLieuDinhKemRepository.Save();
						}
						else
						{
							result.Status = false;
							result.Message = resultSave.message;
						}
					}
				}
			}

			return result;
		}


	}
}