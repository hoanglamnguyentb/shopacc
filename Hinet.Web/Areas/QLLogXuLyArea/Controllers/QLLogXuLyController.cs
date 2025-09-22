using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.QLLogXuLyService;
using Hinet.Service.QLLogXuLyService.Dto;
using Hinet.Web.Areas.QLLogXuLyArea.Models;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Hinet.Web.Areas.QLLogXuLyArea.Controllers
{
    public class QLLogXuLyController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionHome = "QLLogXuLy_home";
        public const string permissionCreate = "QLLogXuLy_create";
        public const string permissionEdit = "QLLogXuLy_edit";
        public const string permissionDelete = "QLLogXuLy_delete";
        public const string permissionDetail = "QLLogXuLy_detail";
        public const string permissionImport = "QLLogXuLy_import";
        public const string permissionExport = "QLLogXuLy_export";
        public const string searchKey = "QLLogXuLyPageSearchModel";
        private readonly IQLLogXuLyService _QLLogXuLyService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IAppUserService _appUserService;

        public QLLogXuLyController(IQLLogXuLyService QLLogXuLyService, ILog Ilog,

        IDM_DulieuDanhmucService dM_DulieuDanhmucService,
        IAppUserService appUserService,
            IMapper mapper
            )
        {
            _QLLogXuLyService = QLLogXuLyService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            this._appUserService = appUserService;
        }

        // GET: QLLogXuLyArea/QLLogXuLy
        [PermissionAccess(Code = permissionHome)]
        public ActionResult Home()
        {
            var searchModel = new QLLogXuLySearchDto();
            var listData = _QLLogXuLyService.GetDaTaByPage(null);
            //ViewBag.NguoiTaoFilter = _appUserService.GetDropdown("UserName", "Id");
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as QLLogXuLySearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new QLLogXuLySearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _QLLogXuLyService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        [PermissionAccess(Code = permissionCreate)]
        public PartialViewResult Create()
        {
            var myModel = new CreateVM();

            return PartialView("_CreatePartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionCreate)]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true, "Tạo Quản lý log xử lý thành công");
            try
            {
                if (ModelState.IsValid)
                {
                    var EntityModel = _mapper.Map<QLLogXuLy>(model);

                    _QLLogXuLyService.Create(EntityModel);
                }
            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
                _Ilog.Error("Lỗi tạo mới Quản lý log xử lý", ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id)
        {
            var myModel = new EditVM();

            var obj = _QLLogXuLyService.GetById(id);
            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }

            myModel = _mapper.Map(obj, myModel);
            return PartialView("_EditPartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionEdit)]
        public JsonResult Edit(EditVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = _QLLogXuLyService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj = _mapper.Map(model, obj);

                    _QLLogXuLyService.Update(obj);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không cập nhật được";
                _Ilog.Error("Lỗi cập nhật thông tin Quản lý log xử lý", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult searchData(QLLogXuLySearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as QLLogXuLySearchDto;

            if (searchModel == null)
            {
                searchModel = new QLLogXuLySearchDto();
                searchModel.pageSize = 20;
            }

            searchModel.NguoiTaoFilter = form.NguoiTaoFilter;
            searchModel.NgayFormFilter = form.NgayFormFilter;
            searchModel.NgayEndFilter = form.NgayEndFilter;
            searchModel.NoiDungFilter = form.NoiDungFilter;
            SessionManager.SetValue((searchKey), searchModel);

            var data = _QLLogXuLyService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [PermissionAccess(Code = permissionDelete)]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultBO(true, "Xóa Quản lý log xử lý thành công");
            try
            {
                var user = _QLLogXuLyService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _QLLogXuLyService.Delete(user);
            }
            catch (Exception ex)
            {
                result.MessageFail("Không thực hiện được");
                _Ilog.Error("Lỗi khi xóa tài khoản id=" + id, ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionDetail)]
        public ActionResult Detail(long id)
        {
            var model = new DetailVM();
            model.objInfo = _QLLogXuLyService.GetDtoById(id);
            return View(model);
        }

        [PermissionAccess(Code = permissionExport)]
        public FileResult ExportExcel()
        {
            var searchModel = SessionManager.GetValue(searchKey) as QLLogXuLySearchDto;
            var data = _QLLogXuLyService.GetDaTaByPage(searchModel).ListItem;
            var dataExport = new List<QLLogXuLyExportDto>();
            foreach (var item in data)
            {
                var logExport = new QLLogXuLyExportDto();
                logExport.TypeItem = item.TypeItem;
                logExport.NoiDung = item.NoiDung;
                logExport.NgayTao = item.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss");
                logExport.TaiKhoan = item.CreatedBy;
                dataExport.Add(logExport);
            }
            var fileExcel = ExportExcelV2Helper.Export<QLLogXuLyExportDto>(dataExport);
            return File(fileExcel, "application/octet-stream", "QLLogXuLy.xlsx");
        }

        [PermissionAccess(Code = permissionExport)]
        public ActionResult Import()
        {
            var model = new ImportVM();
            model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_QLLogXuLy"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
        {
            JsonResultImportBO<QLLogXuLyImportDto> result = new JsonResultImportBO<QLLogXuLyImportDto>(true);
            //Kiểm tra file có tồn tại k?
            if (fileImport == null)
            {
                result.Status = false;
                result.Message = "Không có file đọc dữ liệu";
                return View(result);
            }

            //Lưu file upload để đọc
            var saveFileResult = UploadProvider.SaveFile(fileImport, null, ".xls,.xlsx", null, "TempImportFile", HostingEnvironment.MapPath("/Uploads"));
            if (!saveFileResult.status)
            {
                result.Status = false;
                result.Message = saveFileResult.message;
                return View(result);
            }
            else
            {
                #region Config để import dữ liệu

                var importHelper = new ImportExcelHelper<QLLogXuLyImportDto>();
                importHelper.PathTemplate = saveFileResult.fullPath;
                //importHelper.StartCol = 2;
                importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
                importHelper.ConfigColumn = new List<ConfigModule>();
                importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<QLLogXuLyImportDto>(collection);

                #endregion Config để import dữ liệu

                var rsl = importHelper.ImportCustomRow();
                if (rsl.Status)
                {
                    result.Status = true;
                    result.Message = rsl.Message;

                    result.ListData = rsl.ListTrue;
                    result.ListFalse = rsl.lstFalse;
                }
                else
                {
                    result.Status = false;
                    result.Message = rsl.Message;
                }
            }
            return View(result);
        }

        [HttpPost]
        public JsonResult GetExportError(List<List<string>> lstData)
        {
            ExportExcelHelper<QLLogXuLyImportDto> exPro = new ExportExcelHelper<QLLogXuLyImportDto>();
            exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
            exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_QLLogXuLy"]);
            exPro.StartRow = 5;
            exPro.StartCol = 2;
            exPro.FileName = "ErrorImportQLLogXuLy";
            var result = exPro.ExportText(lstData);
            if (result.Status)
            {
                result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveImportData(List<QLLogXuLyImportDto> Data)
        {
            var result = new JsonResultBO(true);

            var lstObjSave = new List<QLLogXuLy>();
            try
            {
                foreach (var item in Data)
                {
                    var obj = _mapper.Map<QLLogXuLy>(item);
                    _QLLogXuLyService.Create(obj);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Lỗi dữ liệu, không thể import";
                _Ilog.Error("Lỗi Import", ex);
            }

            return Json(result);
        }
    }
}