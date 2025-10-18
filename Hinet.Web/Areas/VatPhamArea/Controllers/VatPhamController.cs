using AutoMapper;
using CommonHelper;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.VatPhamService;
using Hinet.Service.VatPhamService.Dto;
using Hinet.Web.Areas.VatPhamArea.Models;
using Hinet.Web.Filters;
using log4net;
using MassTransit.Configuration;
using System;
using System.Web;
using System.Web.Mvc;



namespace Hinet.Web.Areas.VatPhamArea.Controllers
{
    public class VatPhamController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "VatPham_index";
        public const string permissionCreate = "VatPham_create";
        public const string permissionEdit = "VatPham_edit";
        public const string permissionDelete = "VatPham_delete";
        public const string permissionImport = "VatPham_Inport";
        public const string permissionExport = "VatPham_export";
        public const string searchKey = "VatPhamPageSearchModel";
        private readonly IVatPhamService _VatPhamService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;


        public VatPhamController(IVatPhamService VatPhamService, ILog Ilog,
        IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _VatPhamService = VatPhamService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;

        }
        // GET: VatPhamArea/VatPham
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index(int id)
        {
            var searchModel = new VatPhamSearchDto
            {
                GianHangIdFilter = id
            };
            var listData = _VatPhamService.GetDaTaByPage(searchModel);
            ViewBag.GianHangId = id;
            SessionManager.SetValue(searchKey, searchModel);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as VatPhamSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new VatPhamSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _VatPhamService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }
        public PartialViewResult Create(int id)
        {
            var myModel = new CreateVM()
            {
                GianHangId = id,
            };

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
                        model.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/VatPham");
                    }
                    var EntityModel = _mapper.Map<VatPham>(model);
                    EntityModel.Slug = SlugHelper.GenerateSlug(model.Name, 50);
                    _VatPhamService.Create(EntityModel);

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

            var obj = _VatPhamService.GetById(id);
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

                    var obj = _VatPhamService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }
                    obj = _mapper.Map(model, obj);
                    if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
                    {
                        FileHelper.DeleteFile(model.DuongDanAnh);
                        obj.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/VatPham");
                    }
                    obj.Slug = SlugHelper.GenerateSlug(model.Name, 50);
                    _VatPhamService.Update(obj);

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
        public JsonResult searchData(VatPhamSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as VatPhamSearchDto;

            if (searchModel == null)
            {
                searchModel = new VatPhamSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.GiaGocFilter = form.GiaGocFilter;
            searchModel.NameFilter = form.NameFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _VatPhamService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _VatPhamService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _VatPhamService.Delete(user);
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
            model.objInfo = _VatPhamService.GetById(id);
            return View(model);
        }



    }
}