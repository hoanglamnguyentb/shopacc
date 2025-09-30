using AutoMapper;
using CommonHelper;
using Hinet.Model.Entities;
using Hinet.Service.BannerService;
using Hinet.Service.BannerService.Dto;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Web.Areas.BannerArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.BannerArea.Controllers
{
	public class BannerController : BaseController
	{
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "Banner_index";
		public const string permissionCreate = "Banner_create";
		public const string permissionEdit = "Banner_edit";
		public const string permissionDelete = "Banner_delete";
		public const string permissionImport = "Banner_Inport";
		public const string permissionExport = "Banner_export";
		public const string searchKey = "BannerPageSearchModel";
		private readonly IBannerService _BannerService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;

		public BannerController(IBannerService BannerService, ILog Ilog,

		IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			IMapper mapper
			)
		{
			_BannerService = BannerService;
			_Ilog = Ilog;
			_mapper = mapper;
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
		}

		// GET: BannerArea/Banner
		//[PermissionAccess(Code = permissionIndex)]
		public ActionResult Index()
		{
			var listData = _BannerService.GetDaTaByPage(null);
			SessionManager.SetValue(searchKey, null);
			return View(listData);
		}

		[HttpPost]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(searchKey) as BannerSearchDto;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new BannerSearchDto();
				}
				searchModel.sortQuery = sortQuery;
				if (pageSize > 0)
				{
					searchModel.pageSize = pageSize;
				}
				SessionManager.SetValue(searchKey, searchModel);
			}
			var data = _BannerService.GetDaTaByPage(searchModel, indexPage, pageSize);
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
						model.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/Banner");
					}

					var EntityModel = _mapper.Map<Banner>(model);
					_BannerService.Create(EntityModel);
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

			var obj = _BannerService.GetById(id);
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
					var obj = _BannerService.GetById(model.Id);
					if (obj == null)
					{
						throw new Exception("Không tìm thấy thông tin");
					}
					obj = _mapper.Map(model, obj);

					if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
					{
						FileHelper.DeleteFile(model.DuongDanAnh);
						obj.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/Banner");
					}

					_BannerService.Update(obj);
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
		public JsonResult searchData(BannerSearchDto form)
		{
			var searchModel = SessionManager.GetValue(searchKey) as BannerSearchDto;

			if (searchModel == null)
			{
				searchModel = new BannerSearchDto();
				searchModel.pageSize = 20;
			}
			searchModel.NameFilter = form.NameFilter;
			searchModel.DuongDanAnhFilter = form.DuongDanAnhFilter;
			searchModel.LinkFilter = form.LinkFilter;
			searchModel.KichHoatFilter = form.KichHoatFilter;

			SessionManager.SetValue((searchKey), searchModel);

			var data = _BannerService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var result = new JsonResultBO(true, "Xóa thành công");
			try
			{
				var user = _BannerService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				_BannerService.Delete(user);
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
			model.objInfo = _BannerService.GetById(id);
			return View(model);
		}

        [HttpPost]
        public JsonResult UpdateKichHoat(int id)
        {
            var result = new JsonResultBO(true, "Cập nhật trạng thái thành công");
            try
            {
                var obj = _BannerService.GetById(id);
                if (obj == null)
                {
					result.MessageFail("Không tìm thấy thông tin");
                    return Json(result);
                }
                obj.KichHoat = !obj.KichHoat;
                _BannerService.Update(obj);
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