using AutoMapper;
using CommonHelper;
using Hinet.Model;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.TaiKhoanService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.TaiLieuDinhKemService;
using Hinet.Web.Areas.TaiKhoanArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.TaiKhoanArea.Controllers
{
	public class TaiKhoanController : BaseController
	{
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "TaiKhoan_index";
		public const string permissionCreate = "TaiKhoan_create";
		public const string permissionEdit = "TaiKhoan_edit";
		public const string permissionDelete = "TaiKhoan_delete";
		public const string permissionImport = "TaiKhoan_Inport";
		public const string permissionExport = "TaiKhoan_export";
		public const string searchKey = "TaiKhoanPageSearchModel";
		private readonly ITaiKhoanService _TaiKhoanService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IGameService _gameService;
		private readonly ITaiLieuDinhKemService _taiLieuDinhKemService;

		public TaiKhoanController(ITaiKhoanService TaiKhoanService, ILog Ilog,
			IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			IMapper mapper,
			IGameService gameService, ITaiLieuDinhKemService taiLieuDinhKemService)
		{
			_TaiKhoanService = TaiKhoanService;
			_Ilog = Ilog;
			_mapper = mapper;
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_gameService = gameService;
			_taiLieuDinhKemService = taiLieuDinhKemService;
		}

		// GET: TaiKhoanArea/TaiKhoan
		//[PermissionAccess(Code = permissionIndex)]
		public ActionResult Index(int id)
		{
			var searchModel = new TaiKhoanSearchDto
			{
				GameIdFilter = id
			};
			ViewBag.Game = _gameService.GetById(id) as Game;
			var listData = _TaiKhoanService.GetDaTaByPage(searchModel);
			SessionManager.SetValue(searchKey, searchModel);
			return View(listData);
		}

		[HttpPost]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(searchKey) as TaiKhoanSearchDto;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new TaiKhoanSearchDto();
				}
				searchModel.sortQuery = sortQuery;
				if (pageSize > 0)
				{
					searchModel.pageSize = pageSize;
				}
				SessionManager.SetValue(searchKey, searchModel);
			}
			var data = _TaiKhoanService.GetDaTaByPage(searchModel, indexPage, pageSize);
			return Json(data);
		}

		public PartialViewResult Create()
		{
			var myModel = new CreateVM();

			return PartialView("_CreatePartial", myModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Create(CreateVM model)
		{
			var result = new JsonResultBO(true, "Tạo  thành công");
			try
			{
				if (ModelState.IsValid)
				{
					var entity = _mapper.Map<TaiKhoan>(model);
					_TaiKhoanService.Create(entity);

					//Tạo các tài liệu đính kèm liên quan đến file này

					var listTaiLieu = new List<TaiLieuDinhKem>();
					var files = Request.Files;

					for (int i = 0; i < files.Count; i++)
					{
						var file = files[i];
						if (file != null && file.ContentLength > 0)
						{
							var relativeFolder = $"~/Uploads/TaiKhoan/{entity.Id}";
							var filePath = FileHelper.SaveUploadedFile(file, relativeFolder);

							listTaiLieu.Add(new TaiLieuDinhKem
							{
								TenTaiLieu = file.FileName,
								LoaiTaiLieu = nameof(TaiKhoan),
								Item_ID = entity.Id,
								MoTa = string.Empty,
								DuongDanFile  = filePath

							});
						}
					}
					_taiLieuDinhKemService.InsertRange(listTaiLieu);
				}
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
				_Ilog.Error("Lỗi tạo mới ", ex);
			}
			return Json(result);
		}

		public PartialViewResult Edit(long id)
		{
			var myModel = new EditVM();

			var obj = _TaiKhoanService.GetById(id);
			if (obj == null)
			{
				throw new HttpException(404, "Không tìm thấy thông tin");
			}

			myModel = _mapper.Map(obj, myModel);
			myModel.TaiLieuDinhKemList = _taiLieuDinhKemService.GetListTaiLieuAllByType(nameof(TaiKhoan), id);

			return PartialView("_EditPartial", myModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Edit(EditVM model)
		{
			var result = new JsonResultBO(true);
			try
			{
				if (ModelState.IsValid)
				{
					var obj = _TaiKhoanService.GetById(model.Id);
					if (obj == null)
					{
						throw new Exception("Không tìm thấy thông tin");
					}

					obj = _mapper.Map(model, obj);
					_TaiKhoanService.Update(obj);
				}
			}
			catch (Exception ex)
			{
				result.Status = false;
				result.Message = "Không cập nhật được";
				_Ilog.Error("Lỗi cập nhật thông tin ", ex);
			}
			return Json(result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult searchData(TaiKhoanSearchDto form)
		{
			var searchModel = SessionManager.GetValue(searchKey) as TaiKhoanSearchDto;

			if (searchModel == null)
			{
				searchModel = new TaiKhoanSearchDto();
				searchModel.pageSize = 20;
			}
			searchModel.CodeFilter = form.CodeFilter;
			searchModel.GameIdFilter = form.GameIdFilter;
			searchModel.TrangThaiFilter = form.TrangThaiFilter;
			searchModel.UserNameFilter = form.UserNameFilter;
			searchModel.PasswordFilter = form.PasswordFilter;
			searchModel.GiaGocFilter = form.GiaGocFilter;
			searchModel.GiaKhuyenMaiFilter = form.GiaKhuyenMaiFilter;
			searchModel.MotaFilter = form.MotaFilter;
			searchModel.ViTriFilter = form.ViTriFilter;

			SessionManager.SetValue((searchKey), searchModel);

			var data = _TaiKhoanService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		public JsonResult Delete(long id)
		{
			var result = new JsonResultBO(true, "Xóa  thành công");
			try
			{
				var user = _TaiKhoanService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				_TaiKhoanService.Delete(user);
			}
			catch (Exception ex)
			{
				result.MessageFail("Không thực hiện được");
				_Ilog.Error("Lỗi khi xóa tài khoản id=" + id, ex);
			}
			return Json(result);
		}

		public ActionResult Detail(long id)
		{
			var model = new DetailVM();
			model.objInfo = _TaiKhoanService.GetById(id);
			return View(model);
		}
	}
}