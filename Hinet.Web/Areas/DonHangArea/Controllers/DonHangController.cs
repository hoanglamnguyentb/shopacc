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
using Hinet.Web.Areas.DonHangArea.Models;
using Hinet.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Hinet.Web.Filters;
using Hinet.Service.DonHangService;
using Hinet.Service.DonHangService.Dto;
using CommonHelper.Excel;
using CommonHelper.ObjectExtention;
using Hinet.Web.Common;
using System.IO;
using System.Web.Configuration;
using CommonHelper;
using Hinet.Service.DM_DulieuDanhmucService;



namespace Hinet.Web.Areas.DonHangArea.Controllers
{
    public class DonHangController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "DonHang_index";
        public const string permissionCreate = "DonHang_create";
        public const string permissionEdit = "DonHang_edit";
        public const string permissionDelete = "DonHang_delete";
        public const string permissionImport = "DonHang_Inport";
        public const string permissionExport = "DonHang_export";
        public const string searchKey = "DonHangPageSearchModel";
        private readonly IDonHangService _DonHangService;
	private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;


        public DonHangController(IDonHangService DonHangService, ILog Ilog,

		IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _DonHangService = DonHangService;
            _Ilog = Ilog;
            _mapper = mapper;
		_dM_DulieuDanhmucService = dM_DulieuDanhmucService;

        }
        // GET: DonHangArea/DonHang
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {

            var listData = _DonHangService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as DonHangSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new DonHangSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _DonHangService.GetDaTaByPage(searchModel, indexPage, pageSize);
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
                    var EntityModel = _mapper.Map<DonHang>(model);
                    _DonHangService.Create(EntityModel);

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

            var obj= _DonHangService.GetById(id);
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

                    var obj = _DonHangService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj= _mapper.Map(model, obj);
                    _DonHangService.Update(obj);
                    
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
        public JsonResult searchData(DonHangSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as DonHangSearchDto;

            if (searchModel == null)
            {
                searchModel = new DonHangSearchDto();
                searchModel.pageSize = 20;
            }
			searchModel.DonHangIdFilter = form.DonHangIdFilter;
			searchModel.VatPhamIdFilter = form.VatPhamIdFilter;
			searchModel.MaGiamGiaIdFilter = form.MaGiamGiaIdFilter;
			searchModel.GiaGocFilter = form.GiaGocFilter;
			searchModel.GiaKhuyenMaiFilter = form.GiaKhuyenMaiFilter;
			searchModel.TrangThaiFilter = form.TrangThaiFilter;
			searchModel.QrUrlFilter = form.QrUrlFilter;

            SessionManager.SetValue((searchKey) , searchModel);

            var data = _DonHangService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _DonHangService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _DonHangService.Delete(user);
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
            model.objInfo = _DonHangService.GetById(id);
            return View(model);
        }


        
    }
}