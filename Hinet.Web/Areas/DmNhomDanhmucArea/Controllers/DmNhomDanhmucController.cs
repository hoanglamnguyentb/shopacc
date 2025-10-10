using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DM_NhomDanhmucService;
using Hinet.Service.DM_NhomDanhmucService.Dto;
using Hinet.Service.DM_NhomDanhmucService.DTO;
using Hinet.Web.Areas.DmNhomDanhmucArea.Models;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DmNhomDanhmucArea.Controllers
{
    public class DmNhomDanhmucController : BaseController
    {
        private IDM_NhomDanhmucService _dm_NhomDanhmucService;
        private IDM_DulieuDanhmucService _dm_DulieuDanhmucService;
        private ILog _ILog;
        private readonly IMapper _mapper;
        private const string SessionSearchString = "NhomDanhmucSearch";
        public const string permissionIndex = "DMNhomDanhMuc_index";
        public const string permissionCreate = "DMNhomDanhMuc_create";
        public const string permissionEdit = "DMNhomDanhMuc_edit";
        public const string permissionDelete = "DMNhomDanhMuc_delete";
        public const string permissionDetail = "DMNhomDanhMuc_detail";
        public const string permissionImport = "DMNhomDanhMuc_Inport";
        public const string permissionExport = "DMNhomDanhMuc_export";

        public DmNhomDanhmucController(IDM_NhomDanhmucService dm_NhomDanhmucService, IMapper mapper, IDM_DulieuDanhmucService dm_DulieuDanhmucService, ILog ILog)
        {
            _dm_NhomDanhmucService = dm_NhomDanhmucService;
            _ILog = ILog;
            _mapper = mapper;
            _dm_DulieuDanhmucService = dm_DulieuDanhmucService;
        }

        // GET: DmNhomDanhmucArea/DmNhomDanhmuc
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            var searchModel = new DM_NhomDanhmucSearchDTO();
            SessionManager.SetValue(SessionSearchString, null);
            var data = _dm_NhomDanhmucService.GetDataByPage(null);
            return View("Index", data);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult GetData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(SessionSearchString) as DM_NhomDanhmucSearchDTO;
            if (searchModel == null)
            {
                searchModel = new DM_NhomDanhmucSearchDTO();
            }
            if (!string.IsNullOrEmpty(sortQuery))
            {
                searchModel.sortQuery = sortQuery;
            }
            if (pageSize > 0)
            {
                searchModel.pageSize = pageSize;
            }
            SessionManager.SetValue(SessionSearchString, searchModel);
            var data = _dm_NhomDanhmucService.GetDataByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        [PermissionAccess(Code = permissionCreate)]
        public PartialViewResult Create()
        {
            var model = new CreateVM();
            return PartialView("_CreatePartial", model);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionCreate)]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(model.GroupCode) && _dm_NhomDanhmucService.CheckGroupCodeExisted(model.GroupCode.ToUpper()))
                    {
                        throw new Exception(String.Format("Mã nhóm {0} đã tồn tại!", model.GroupCode.ToUpper()));
                    }
                    var nhomDanhmuc = new DM_NhomDanhmuc();
                    nhomDanhmuc.GroupName = model.GroupName;
                    nhomDanhmuc.GroupCode = model.GroupCode.ToUpper();
                    _dm_NhomDanhmucService.Create(nhomDanhmuc);
                    _ILog.Info(String.Format("Thêm mới nhóm danh mục {0}", nhomDanhmuc.GroupName));
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
                _ILog.Error("Lỗi ở nhóm danh mục", ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id)
        {
            var model = new EditVM();
            var singleGroup = _dm_NhomDanhmucService.GetById(id);
            if (singleGroup == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }
            model = new EditVM()
            {
                Id = singleGroup.Id,
                GroupName = singleGroup.GroupName,
                GroupCode = singleGroup.GroupCode
            };
            return PartialView("_EditPartial", model);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionEdit)]
        public JsonResult Edit(EditVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    var singleGroup = _dm_NhomDanhmucService.GetById(model.Id);
                    if (singleGroup == null)
                    {
                        throw new Exception("Không tìm thấy nhóm danh mục");
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(model.GroupCode))
                        {
                            if (singleGroup.GroupCode.Equals(model.GroupCode))
                            {
                                singleGroup.GroupName = model.GroupName;
                                _dm_NhomDanhmucService.Update(singleGroup);
                            }
                            else if (_dm_NhomDanhmucService.CheckGroupCodeExisted(model.GroupCode.ToUpper()))
                            {
                                throw new Exception(String.Format("Mã nhóm {0} đã tồn tại", model.GroupCode.ToUpper()));
                            }
                            else
                            {
                                singleGroup.GroupCode = model.GroupCode.ToUpper();
                                singleGroup.GroupName = model.GroupName;
                                _dm_NhomDanhmucService.Update(singleGroup);
                            }
                        }
                        else
                        {
                            throw new Exception("Thiếu mã nhóm danh mục");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không cập nhật được!";
                _ILog.Error("Lỗi cập nhật chỉnh sửa nhóm danh mục", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult SearchData(FormCollection form)
        {
            var searchModel = SessionManager.GetValue(SessionSearchString) as DM_NhomDanhmucSearchDTO;

            if (searchModel == null)
            {
                searchModel = new DM_NhomDanhmucSearchDTO();
                searchModel.pageSize = 10;
            }

            searchModel.QueryName = form["QueryName"];
            searchModel.QueryCode = form["QueryCode"].ToUpper();
            SessionManager.SetValue(SessionSearchString, searchModel);
            var data = _dm_NhomDanhmucService.GetDataByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionDelete)]
        public JsonResult Delete(long id)
        {
            JsonResultBO result = new JsonResultBO(true);
            try
            {
                DM_NhomDanhmuc entity = _dm_NhomDanhmucService.GetById(id);
                if (entity != null)
                {
                    List<DM_DulieuDanhmuc> listDulieu = _dm_DulieuDanhmucService.GetListDataByGroupId(id);
                    if (listDulieu != null && listDulieu.Count > 0)
                    {
                        _dm_DulieuDanhmucService.DeleteRange(listDulieu);
                        _ILog.Info(String.Format("Xoá dữ liệu của danh mục {0} thành công", entity.GroupCode));
                    }
                    _dm_NhomDanhmucService.Delete(entity);
                    result.Message = "Xóa nhóm danh mục thành công";
                    _ILog.Info(result.Message);
                }
                else
                {
                    result.Status = false;
                    result.Message = "Nhóm danh mục không tồn tại";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Xóa nhóm danh mục không thành công";
                _ILog.Error("Xóa nhóm danh mục không thành công", ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionExport)]
        public ActionResult Import()
        {
            var model = new ImportVM();
            model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_DM_NhomDanhmuc"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
        {
            JsonResultImportBO<DM_NhomDanhmucImportDto> result = new JsonResultImportBO<DM_NhomDanhmucImportDto>(true);
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

                var importHelper = new ImportExcelHelper<DM_NhomDanhmucImportDto>();
                importHelper.PathTemplate = saveFileResult.fullPath;
                //importHelper.StartCol = 2;
                importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
                importHelper.ConfigColumn = new List<ConfigModule>();
                importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<DM_NhomDanhmucImportDto>(collection);

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
            ExportExcelHelper<DM_NhomDanhmucImportDto> exPro = new ExportExcelHelper<DM_NhomDanhmucImportDto>();
            exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
            exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_DM_NhomDanhmuc"]);
            exPro.StartRow = 5;
            exPro.StartCol = 2;
            exPro.FileName = "ErrorImportDM_NhomDanhmuc";
            var result = exPro.ExportText(lstData);
            if (result.Status)
            {
                result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveImportData(List<DM_NhomDanhmucImportDto> Data)
        {
            var result = new JsonResultBO(true);

            var lstObjSave = new List<DM_NhomDanhmuc>();
            try
            {
                foreach (var item in Data)
                {
                    var obj = _mapper.Map<DM_NhomDanhmuc>(item);
                    _dm_NhomDanhmucService.Create(obj);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Lỗi dữ liệu, không thể import";
                _ILog.Error("Lỗi Import", ex);
            }

            return Json(result);
        }
    }
}