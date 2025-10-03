using AutoMapper;
using CommonHelper.String;
using CommonHelper.Upload;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Web.Areas.GiaTriThuocTinhArea.Models;
using Hinet.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Hinet.Web.Filters;
using Hinet.Service.GiaTriThuocTinhService;
using Hinet.Service.GiaTriThuocTinhService.Dto;
using CommonHelper.Excel;
using CommonHelper.ObjectExtention;
using Hinet.Web.Common;
using System.IO;
using System.Web.Configuration;
using CommonHelper;
using Hinet.Service.DM_DulieuDanhmucService;



namespace Hinet.Web.Areas.GiaTriThuocTinhArea.Controllers
{
    public class GiaTriThuocTinhController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "GiaTriThuocTinh_index";
        public const string permissionCreate = "GiaTriThuocTinh_create";
        public const string permissionEdit = "GiaTriThuocTinh_edit";
        public const string permissionDelete = "GiaTriThuocTinh_delete";
        public const string permissionImport = "GiaTriThuocTinh_Inport";
        public const string permissionExport = "GiaTriThuocTinh_export";
        public const string searchKey = "GiaTriThuocTinhPageSearchModel";
        private readonly IGiaTriThuocTinhService _GiaTriThuocTinhService;
	private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;


        public GiaTriThuocTinhController(IGiaTriThuocTinhService GiaTriThuocTinhService, ILog Ilog,

		IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _GiaTriThuocTinhService = GiaTriThuocTinhService;
            _Ilog = Ilog;
            _mapper = mapper;
		_dM_DulieuDanhmucService = dM_DulieuDanhmucService;

        }
        // GET: GiaTriThuocTinhArea/GiaTriThuocTinh
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {

            var listData = _GiaTriThuocTinhService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as GiaTriThuocTinhSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new GiaTriThuocTinhSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _GiaTriThuocTinhService.GetDaTaByPage(searchModel, indexPage, pageSize);
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
                    var EntityModel = _mapper.Map<GiaTriThuocTinh>(model);
                    _GiaTriThuocTinhService.Create(EntityModel);

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

            var obj= _GiaTriThuocTinhService.GetById(id);
            if (obj== null)
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

                    var obj = _GiaTriThuocTinhService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj= _mapper.Map(model, obj);
                    _GiaTriThuocTinhService.Update(obj);
                    
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
        public JsonResult searchData(GiaTriThuocTinhSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as GiaTriThuocTinhSearchDto;

            if (searchModel == null)
            {
                searchModel = new GiaTriThuocTinhSearchDto();
                searchModel.pageSize = 20;
            }
			searchModel.TaiKhoanIdFilter = form.TaiKhoanIdFilter;
			searchModel.ThuocTinhIdFilter = form.ThuocTinhIdFilter;
			searchModel.ThuocTinhTxtFilter = form.ThuocTinhTxtFilter;
			searchModel.GiaTriFilter = form.GiaTriFilter;
			searchModel.GiaTriTextFilter = form.GiaTriTextFilter;

            SessionManager.SetValue((searchKey) , searchModel);

            var data = _GiaTriThuocTinhService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _GiaTriThuocTinhService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _GiaTriThuocTinhService.Delete(user);
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
            model.objInfo = _GiaTriThuocTinhService.GetById(id);
            return View(model);
        }


        
    }
}