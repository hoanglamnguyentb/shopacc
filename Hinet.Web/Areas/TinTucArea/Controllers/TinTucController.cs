using AutoMapper;
using CommonHelper.String;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.TinTucService;
using Hinet.Service.TinTucService.Dto;
using Hinet.Web.Areas.TinTucArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Web;
using System.Web.Mvc;



namespace Hinet.Web.Areas.TinTucArea.Controllers
{
	public class TinTucController : BaseController
	{
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "TinTuc_index";
		public const string permissionCreate = "TinTuc_create";
		public const string permissionEdit = "TinTuc_edit";
		public const string permissionDelete = "TinTuc_delete";
		public const string permissionImport = "TinTuc_Inport";
		public const string permissionExport = "TinTuc_export";
		public const string searchKey = "TinTucPageSearchModel";
		private readonly ITinTucService _TinTucService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;


		public TinTucController(ITinTucService TinTucService, ILog Ilog,

		IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			IMapper mapper
			)
		{
			_TinTucService = TinTucService;
			_Ilog = Ilog;
			_mapper = mapper;
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;

		}
		// GET: TinTucArea/TinTuc
		//[PermissionAccess(Code = permissionIndex)]
		public ActionResult Index()
		{

			var listData = _TinTucService.GetDaTaByPage(null);
			SessionManager.SetValue(searchKey, null);
			return View(listData);
		}

		[HttpPost]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(searchKey) as TinTucSearchDto;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new TinTucSearchDto();
				}
				searchModel.sortQuery = sortQuery;
				if (pageSize > 0)
				{
					searchModel.pageSize = pageSize;
				}
				SessionManager.SetValue(searchKey, searchModel);
			}
			var data = _TinTucService.GetDaTaByPage(searchModel, indexPage, pageSize);
			return Json(data);
		}
		public PartialViewResult Create()
		{
			var myModel = new CreateVM();

			return PartialView("_CreatePartial", myModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public JsonResult Create(CreateVM model)
		{
			var result = new JsonResultBO(true, "Tạo  thành công");
			try
			{
				if (ModelState.IsValid)
				{
					var EntityModel = _mapper.Map<TinTuc>(model);
					EntityModel.TacGia = CurrentUserInfo.FullName;
					EntityModel.ThoiGianXuatBan = DateTime.Now;
					EntityModel.Slug = "HELLO";
					//EntityModel.Slug.SlugTitleName();
					_TinTucService.Create(EntityModel);
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

			var obj = _TinTucService.GetById(id);
			if (obj == null)
			{
				throw new HttpException(404, "Không tìm thấy thông tin");
			}

			myModel = _mapper.Map(obj, myModel);
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

					var obj = _TinTucService.GetById(model.Id);
					if (obj == null)
					{
						throw new Exception("Không tìm thấy thông tin");
					}

					obj = _mapper.Map(model, obj);
					_TinTucService.Update(obj);

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
		public JsonResult searchData(TinTucSearchDto form)
		{
			var searchModel = SessionManager.GetValue(searchKey) as TinTucSearchDto;

			if (searchModel == null)
			{
				searchModel = new TinTucSearchDto();
				searchModel.pageSize = 20;
			}
			searchModel.SlugFilter = form.SlugFilter;
			searchModel.TieuDeFilter = form.TieuDeFilter;
			searchModel.NoiDungFilter = form.NoiDungFilter;
			searchModel.AnhBiaFilter = form.AnhBiaFilter;
			searchModel.TacGiaFilter = form.TacGiaFilter;
			searchModel.TrangThaiFilter = form.TrangThaiFilter;
			searchModel.ThoiGianXuatBanFilter = form.ThoiGianXuatBanFilter;

			SessionManager.SetValue((searchKey), searchModel);

			var data = _TinTucService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		public JsonResult Delete(long id)
		{
			var result = new JsonResultBO(true, "Xóa  thành công");
			try
			{
				var user = _TinTucService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				_TinTucService.Delete(user);
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
			model.objInfo = _TinTucService.GetById(id);
			return View(model);
		}



	}
}