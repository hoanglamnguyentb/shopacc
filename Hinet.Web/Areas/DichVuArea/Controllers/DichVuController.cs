using AutoMapper;
using CommonHelper;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DichVuService;
using Hinet.Service.DichVuService.Dto;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Web.Areas.DichVuArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DichVuArea.Controllers
{
	public class DichVuController : BaseController
	{
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "DichVu_index";
		public const string permissionCreate = "DichVu_create";
		public const string permissionEdit = "DichVu_edit";
		public const string permissionDelete = "DichVu_delete";
		public const string permissionImport = "DichVu_Inport";
		public const string permissionExport = "DichVu_export";
		public const string searchKey = "DichVuPageSearchModel";
		private readonly IDichVuService _DichVuService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;

		public DichVuController(IDichVuService DichVuService, ILog Ilog,

		IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			IMapper mapper
			)
		{
			_DichVuService = DichVuService;
			_Ilog = Ilog;
			_mapper = mapper;
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
		}

		// GET: DichVuArea/DichVu
		//[PermissionAccess(Code = permissionIndex)]
		public ActionResult Index()
		{
			var listData = _DichVuService.GetDaTaByPage(null);
			SessionManager.SetValue(searchKey, null);
			return View(listData);
		}

		[HttpPost]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(searchKey) as DichVuSearchDto;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new DichVuSearchDto();
				}
				searchModel.sortQuery = sortQuery;
				if (pageSize > 0)
				{
					searchModel.pageSize = pageSize;
				}
				SessionManager.SetValue(searchKey, searchModel);
			}
			var data = _DichVuService.GetDaTaByPage(searchModel, indexPage, pageSize);
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
					if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
					{
						model.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/DichVu");
					}

					var EntityModel = _mapper.Map<DichVu>(model);
					_DichVuService.Create(EntityModel);
				}
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
				_Ilog.Error("Lỗi tạo mới ", ex);
			}
			return Json(result);
		}

		public PartialViewResult Edit(int id)
		{
			var myModel = new EditVM();

			var obj = _DichVuService.GetById(id);
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
					var obj = _DichVuService.GetById(model.Id);
					if (obj == null)
					{
						throw new Exception("Không tìm thấy thông tin");
					}
					obj = _mapper.Map(model, obj);

					if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
					{
						FileHelper.DeleteFile(model.DuongDanAnh);
						obj.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/DichVu");
					}

					_DichVuService.Update(obj);
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
		public JsonResult searchData(DichVuSearchDto form)
		{
			var searchModel = SessionManager.GetValue(searchKey) as DichVuSearchDto;

			if (searchModel == null)
			{
				searchModel = new DichVuSearchDto();
				searchModel.pageSize = 20;
			}
			searchModel.NameFilter = form.NameFilter;
			searchModel.DuongDanAnhFilter = form.DuongDanAnhFilter;
			searchModel.LinkFilter = form.LinkFilter;
			searchModel.KichHoatFilter = form.KichHoatFilter;

			SessionManager.SetValue((searchKey), searchModel);

			var data = _DichVuService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var result = new JsonResultBO(true, "Xóa  thành công");
			try
			{
				var user = _DichVuService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				_DichVuService.Delete(user);
			}
			catch (Exception ex)
			{
				result.MessageFail("Không thực hiện được");
				_Ilog.Error("Lỗi khi xóa tài khoản id=" + id, ex);
			}
			return Json(result);
		}

		public ActionResult Detail(int id)
		{
			var model = new DetailVM();
			model.objInfo = _DichVuService.GetById(id);
			return View(model);
		}

        [HttpPost]
        public JsonResult UpdateKichHoat(int id)
        {
            var result = new JsonResultBO(true, "Cập nhật trạng thái thành công");
            try
            {
                var obj = _DichVuService.GetById(id);
                if (obj == null)
                {
                    result.MessageFail("Không tìm thấy thông tin");
                    return Json(result);
                }
                obj.KichHoat = !obj.KichHoat;
                _DichVuService.Update(obj);
            }
            catch (Exception ex)
            {
                result.MessageFail("Lỗi: " + ex.Message);
                return Json(result);
            }
            return Json(result);
        }
    }
}