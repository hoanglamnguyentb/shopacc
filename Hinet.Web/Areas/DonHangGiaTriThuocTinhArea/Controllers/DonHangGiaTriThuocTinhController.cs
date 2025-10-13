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
using Hinet.Web.Areas.DonHangGiaTriThuocTinhArea.Models;
using Hinet.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Hinet.Web.Filters;
using Hinet.Service.DonHangGiaTriThuocTinhService;
using Hinet.Service.DonHangGiaTriThuocTinhService.Dto;
using CommonHelper.Excel;
using CommonHelper.ObjectExtention;
using Hinet.Web.Common;
using System.IO;
using System.Web.Configuration;
using CommonHelper;
using Hinet.Service.DM_DulieuDanhmucService;



namespace Hinet.Web.Areas.DonHangGiaTriThuocTinhArea.Controllers
{
    public class DonHangGiaTriThuocTinhController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "DonHangGiaTriThuocTinh_index";
        public const string permissionCreate = "DonHangGiaTriThuocTinh_create";
        public const string permissionEdit = "DonHangGiaTriThuocTinh_edit";
        public const string permissionDelete = "DonHangGiaTriThuocTinh_delete";
        public const string permissionImport = "DonHangGiaTriThuocTinh_Inport";
        public const string permissionExport = "DonHangGiaTriThuocTinh_export";
        public const string searchKey = "DonHangGiaTriThuocTinhPageSearchModel";
        private readonly IDonHangGiaTriThuocTinhService _DonHangGiaTriThuocTinhService;
	private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;


        public DonHangGiaTriThuocTinhController(IDonHangGiaTriThuocTinhService DonHangGiaTriThuocTinhService, ILog Ilog,

		IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _DonHangGiaTriThuocTinhService = DonHangGiaTriThuocTinhService;
            _Ilog = Ilog;
            _mapper = mapper;
		_dM_DulieuDanhmucService = dM_DulieuDanhmucService;

        }
        // GET: DonHangGiaTriThuocTinhArea/DonHangGiaTriThuocTinh
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {

            var listData = _DonHangGiaTriThuocTinhService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as DonHangGiaTriThuocTinhSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new DonHangGiaTriThuocTinhSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _DonHangGiaTriThuocTinhService.GetDaTaByPage(searchModel, indexPage, pageSize);
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
                    var EntityModel = _mapper.Map<DonHangGiaTriThuocTinh>(model);
                    _DonHangGiaTriThuocTinhService.Create(EntityModel);

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

            var obj= _DonHangGiaTriThuocTinhService.GetById(id);
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

                    var obj = _DonHangGiaTriThuocTinhService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj= _mapper.Map(model, obj);
                    _DonHangGiaTriThuocTinhService.Update(obj);
                    
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
        public JsonResult searchData(DonHangGiaTriThuocTinhSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as DonHangGiaTriThuocTinhSearchDto;

            if (searchModel == null)
            {
                searchModel = new DonHangGiaTriThuocTinhSearchDto();
                searchModel.pageSize = 20;
            }
			searchModel.DonHangIdFilter = form.DonHangIdFilter;
			searchModel.ThuocTinhIdFilter = form.ThuocTinhIdFilter;
			searchModel.ThuocTinhTxtFilter = form.ThuocTinhTxtFilter;
			searchModel.GiaTriFilter = form.GiaTriFilter;
			searchModel.GiaTriTxtFilter = form.GiaTriTxtFilter;
			searchModel.KieuDuLieuFilter = form.KieuDuLieuFilter;

            SessionManager.SetValue((searchKey) , searchModel);

            var data = _DonHangGiaTriThuocTinhService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _DonHangGiaTriThuocTinhService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _DonHangGiaTriThuocTinhService.Delete(user);
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
            model.objInfo = _DonHangGiaTriThuocTinhService.GetById(id);
            return View(model);
        }


        
    }
}