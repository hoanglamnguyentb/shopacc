using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GiaoDichService;
using Hinet.Service.GiaoDichService.Dto;
using Hinet.Web.Areas.GiaoDichArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.GiaoDichArea.Controllers
{
	public class GiaoDichController : BaseController
	{
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "GiaoDich_index";
		public const string permissionCreate = "GiaoDich_create";
		public const string permissionEdit = "GiaoDich_edit";
		public const string permissionDelete = "GiaoDich_delete";
		public const string permissionImport = "GiaoDich_Inport";
		public const string permissionExport = "GiaoDich_export";
		public const string searchKey = "GiaoDichPageSearchModel";
		private readonly IGiaoDichService _GiaoDichService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IAppUserService _appUserService;

		public GiaoDichController(IGiaoDichService GiaoDichService, ILog Ilog,
			IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			IMapper mapper, IAppUserService appUserService)
		{
			_GiaoDichService = GiaoDichService;
			_Ilog = Ilog;
			_mapper = mapper;
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_appUserService = appUserService;
		}

		// GET: GiaoDichArea/GiaoDich
		//[PermissionAccess(Code = permissionIndex)]
		public ActionResult Index()
		{
			 var listData = _GiaoDichService.GetDaTaByPage(null);
			SessionManager.SetValue(searchKey, null);
			ViewBag.dropdownListNguoiGiaoDich = _appUserService.GetDropDownMultiple("UserName", "Id");
			ViewBag.dropdownListLoaiDoiTuong = ConstantExtension.GetDropdownData<LoaiDoiTuongConstant>();
			ViewBag.dropdownListLoaiGiaoDich = ConstantExtension.GetDropdownData<LoaiGiaoDichConstant>();
			ViewBag.dropdownListTrangThai = ConstantExtension.GetDropdownData<TrangThaiGiaoDichConstant>();
			ViewBag.dropdownListPhuongThucThanhToan = ConstantExtension.GetDropdownData<PhuongThucThanhToanConstant>();
			return View(listData);
		}

		[HttpPost]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(searchKey) as GiaoDichSearchDto;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new GiaoDichSearchDto();
				}
				searchModel.sortQuery = sortQuery;
				if (pageSize > 0)
				{
					searchModel.pageSize = pageSize;
				}
				SessionManager.SetValue(searchKey, searchModel);
			}
			var data = _GiaoDichService.GetDaTaByPage(searchModel, indexPage, pageSize);
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
					var EntityModel = _mapper.Map<GiaoDich>(model);
					_GiaoDichService.Create(EntityModel);
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

			var obj = _GiaoDichService.GetById(id);
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
					var obj = _GiaoDichService.GetById(model.Id);
					if (obj == null)
					{
						throw new Exception("Không tìm thấy thông tin");
					}

					obj = _mapper.Map(model, obj);
					_GiaoDichService.Update(obj);
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
		public JsonResult searchData(GiaoDichSearchDto form)
		{
			var searchModel = SessionManager.GetValue(searchKey) as GiaoDichSearchDto;

			if (searchModel == null)
			{
				searchModel = new GiaoDichSearchDto();
				searchModel.pageSize = 20;
			}
			searchModel.UserIdFilter = form.UserIdFilter;
			searchModel.DoiTuongIdFilter = form.DoiTuongIdFilter;
			searchModel.LoaiDoiTuongFilter = form.LoaiDoiTuongFilter;
			searchModel.LoaiGiaoDichFilter = form.LoaiGiaoDichFilter;
			searchModel.TrangThaiFilter = form.TrangThaiFilter;
			searchModel.PhuongThucThanhToanFilter = form.PhuongThucThanhToanFilter;
			searchModel.NgayGiaoDichFilter = form.NgayGiaoDichFilter;
			searchModel.NgayThanhToanFilter = form.NgayThanhToanFilter;

			SessionManager.SetValue((searchKey), searchModel);

			var data = _GiaoDichService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		public JsonResult Delete(long id)
		{
			var result = new JsonResultBO(true, "Xóa  thành công");
			try
			{
				var user = _GiaoDichService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				_GiaoDichService.Delete(user);
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
			model.objInfo = _GiaoDichService.GetById(id);
			return View(model);
		}
	}
}