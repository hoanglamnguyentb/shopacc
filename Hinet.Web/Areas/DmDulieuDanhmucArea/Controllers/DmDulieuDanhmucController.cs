using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DM_DulieuDanhmucService.Dto;
using Hinet.Service.DM_DulieuDanhmucService.DTO;
using Hinet.Service.DM_NhomDanhmucService;
using Hinet.Web.Areas.DmDulieuDanhmucArea.Models;
using Hinet.Web.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DmDulieuDanhmucArea.Controllers
{
    public class DmDulieuDanhmucController : Controller
    {
        // GET: DmDulieuDanhmucArea/DmDulieuDanhmuc
        private readonly ILog _Ilog;

        private readonly IMapper _mapper;
        private IDM_DulieuDanhmucService _dm_DulieuDanhmucService;
        private IDM_NhomDanhmucService _dm_NhomDanhmucService;
        public const string permissionIndex = "DM_DuLieuDanhMuc_index";
        private ILog _ILog;
        private const string SessionSearchString = "DulieuDanhmucSearch";
        private const string SessionSearch_QuanTri = "SessionSearch_QuanTri";
        private const string SessionSearch_QuanTriDM = "SessionSearch_QuanTriDM";

        public DmDulieuDanhmucController(IDM_DulieuDanhmucService dm_DulieuDanhmucService, IDM_NhomDanhmucService dm_NhomdanhmucService, ILog ILog)
        {
            _dm_DulieuDanhmucService = dm_DulieuDanhmucService;
            _dm_NhomDanhmucService = dm_NhomdanhmucService;
            _ILog = ILog;
        }

        public ActionResult Index(long id)
        {
            var searchModel = new DM_DulieuDanhmucSearchDTO();

            var groupName = _dm_NhomDanhmucService.GetById(id);

            if (groupName == null)
            {
                return HttpNotFound();
            }
            var model = new IndexVM();
            model.GroupId = id;
            searchModel.GroupId = id;
            model.Data = _dm_DulieuDanhmucService.GetDataByPage(id, null);
            SessionManager.SetValue(SessionSearchString, null);
            SessionManager.SetValue("GroupId", model.GroupId);
            ViewBag.PageName = "Dữ liệu " + groupName.GroupName;
            model.Code = groupName.GroupCode;
            return View("Index", model);
        }

        [HttpPost]
        public JsonResult GetData(long groupid, int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(SessionSearchString) as DM_DulieuDanhmucSearchDTO;
            if (searchModel == null)
            {
                searchModel = new DM_DulieuDanhmucSearchDTO();
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
            var data = _dm_DulieuDanhmucService.GetDataByPage(groupid, searchModel, indexPage, pageSize);
            return Json(data);
        }

        public PartialViewResult Create(long id)
        {
            var model = new CreateVM();
            model.GroupId = id;
            return PartialView("_CreatePartial", model);
        }

        [HttpPost]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(model.Code) && _dm_DulieuDanhmucService.CheckCodeExisted(model.GroupId, model.Code.ToUpper()))
                    {
                        throw new Exception(String.Format("Mã nhóm {0} đã tồn tại!", model.Code.ToUpper()));
                    }

                    var dulieuDanhmuc = new DM_DulieuDanhmuc();
                    dulieuDanhmuc.GroupId = model.GroupId;
                    dulieuDanhmuc.Name = model.Name;
                    dulieuDanhmuc.Code = model.Code.ToUpper();
                    dulieuDanhmuc.Priority = model.Priority;
                    dulieuDanhmuc.Note = model.Note;

                    if (model.IconFile != null)
                    {
                        var resultUpload_FileAnhIcon = UploadProvider.SaveFile(model.IconFile, null, UploadProvider.ListExtensionCommon, UploadProvider.MaxSizeCommon, "IconMarker", Server.MapPath("/Uploads"));
                        if (resultUpload_FileAnhIcon.status)
                        {
                            dulieuDanhmuc.Icon = resultUpload_FileAnhIcon.path;
                        }
                    }

                    _dm_DulieuDanhmucService.Create(dulieuDanhmuc);
                    _ILog.Info(String.Format("Thêm mới nhóm danh mục {0}", dulieuDanhmuc.Name));
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

        public PartialViewResult Edit(long id)
        {
            var model = new EditVM();
            var singleGroup = _dm_DulieuDanhmucService.GetById(id);
            if (singleGroup == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }
            model = new EditVM()
            {
                Id = singleGroup.Id,
                GroupId = singleGroup.GroupId,
                Name = singleGroup.Name,
                Code = singleGroup.Code,
                Priority = singleGroup.Priority,
                Note = singleGroup.Note
            };
            return PartialView("_EditPartial", model);
        }

        public PartialViewResult Detail(long id)
        {
            var singleGroup = _dm_DulieuDanhmucService.GetById(id);
            if (singleGroup == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }

            return PartialView("_DetailPartial", singleGroup);
        }

        [HttpPost]
        public JsonResult Edit(EditVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    var singleGroup = _dm_DulieuDanhmucService.GetById(model.Id);
                    if (singleGroup == null)
                    {
                        throw new Exception("Không tìm thấy nhóm danh mục");
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(model.Code))
                        {
                            if (model.IconFile != null)
                            {
                                if (!string.IsNullOrEmpty(singleGroup.Icon) && System.IO.File.Exists(Server.MapPath("/Uploads/" + singleGroup.Icon)))
                                {
                                    //Xóa icon cũ ở trong foldler
                                    System.IO.File.Delete(Server.MapPath("/Uploads/" + singleGroup.Icon));
                                }

                                //Lưu icon mới
                                var resultUpload_FileAnhIcon = UploadProvider.SaveFile(model.IconFile, null, UploadProvider.ListExtensionCommon, UploadProvider.MaxSizeCommon, "IconMarker", Server.MapPath("/Uploads"));
                                if (resultUpload_FileAnhIcon.status)
                                {
                                    singleGroup.Icon = resultUpload_FileAnhIcon.path;
                                }
                            }

                            if (singleGroup.Code.Equals(model.Code))
                            {
                                singleGroup.Name = model.Name;
                                singleGroup.Priority = model.Priority;
                                singleGroup.Note = model.Note;
                                _dm_DulieuDanhmucService.Update(singleGroup);
                            }
                            else if (_dm_DulieuDanhmucService.CheckCodeExisted(model.GroupId, model.Code.ToUpper()))
                            {
                                throw new Exception(String.Format("Mã nhóm {0} đã tồn tại", model.Code.ToUpper()));
                            }
                            else
                            {
                                singleGroup.Name = model.Name;
                                singleGroup.Code = model.Code.ToUpper();
                                singleGroup.Priority = model.Priority;
                                singleGroup.Note = model.Note;
                                _dm_DulieuDanhmucService.Update(singleGroup);
                            }
                        }
                        else
                        {
                            throw new Exception("Thiếu mã dữ liệu");
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
        public JsonResult SearchData(FormCollection form)
        {
            var searchModel = SessionManager.GetValue(SessionSearchString) as DM_DulieuDanhmucSearchDTO;
            var GroupId = (long?)SessionManager.GetValue("GroupId");

            if (searchModel == null)
            {
                searchModel = new DM_DulieuDanhmucSearchDTO();
                searchModel.pageSize = 10;
            }

            searchModel.QueryName = form["QueryName"];
            searchModel.QueryCode = form["QueryCode"].ToUpper();
            SessionManager.SetValue(SessionSearchString, searchModel);
            var data = _dm_DulieuDanhmucService.GetDataByPage(GroupId.GetValueOrDefault(), searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            JsonResultBO result = new JsonResultBO(true);
            try
            {
                DM_DulieuDanhmuc entity = _dm_DulieuDanhmucService.GetById(id);
                if (entity != null)
                {
                    if (!string.IsNullOrEmpty(entity.Icon) && System.IO.File.Exists(Server.MapPath("/Uploads/" + entity.Icon)))
                    {
                        //Xóa icon cũ ở trong foldler
                        System.IO.File.Delete(Server.MapPath("/Uploads/" + entity.Icon));
                    }
                    _dm_DulieuDanhmucService.Delete(entity);
                    result.Message = "Xóa danh mục thành công";
                    _ILog.Info(result.Message);
                }
                else
                {
                    result.Status = false;
                    result.Message = "Danh mục không tồn tại";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Xóa danh mục không thành công";
                _ILog.Error("Xóa danh mục không thành công", ex);
            }
            return Json(result);
        }

        public ActionResult IndexQuanTri(string groupCode)
        {
            var searchModel = new DM_DulieuDanhmucSearchDTO();

            var groupName = _dm_NhomDanhmucService.GetNhomDanhMucByGroupCode(groupCode);

            if (groupName == null)
            {
                return HttpNotFound();
            }
            var model = new IndexVM();
            var id = groupName.Id;
            model.GroupId = id;
            searchModel.GroupId = id;
            model.Data = _dm_DulieuDanhmucService.GetDataByPage(id, null);
            SessionManager.SetValue(SessionSearchString, null);
            SessionManager.SetValue("GroupId", model.GroupId);
            ViewBag.PageName = "Dữ liệu " + groupName.GroupName;
            model.Code = groupName.GroupCode;
            return View("IndexQuanTri", model);
        }

        public ActionResult QuanTriDanhMuc()
        {
            var listObj = _dm_DulieuDanhmucService.GetData_QuanTri_ByPage(null);

            ViewBag.NhomDuLieu = _dm_NhomDanhmucService.GetDropdown("GroupName", "Id");

            SessionManager.SetValue(SessionSearch_QuanTri, null);
            return View("QuanTriDanhMuc", listObj);
        }

        [HttpPost]
        public JsonResult GetData_QuanTri(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(SessionSearch_QuanTri) as DM_QuanTri_DulieuDanhmucSearchDTO;
            if (searchModel == null)
            {
                searchModel = new DM_QuanTri_DulieuDanhmucSearchDTO();
            }
            if (!string.IsNullOrEmpty(sortQuery))
            {
                searchModel.sortQuery = sortQuery;
            }
            if (pageSize > 0)
            {
                searchModel.pageSize = pageSize;
            }
            SessionManager.SetValue(SessionSearch_QuanTri, searchModel);
            var data = _dm_DulieuDanhmucService.GetData_QuanTri_ByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchData_QuanTri(DM_QuanTri_DulieuDanhmucSearchDTO form)
        {
            var searchModel = SessionManager.GetValue(SessionSearch_QuanTri) as DM_QuanTri_DulieuDanhmucSearchDTO;

            if (searchModel == null)
            {
                searchModel = new DM_QuanTri_DulieuDanhmucSearchDTO();
                searchModel.pageSize = 10;
            }

            searchModel.QueryName = form.QueryName;
            searchModel.QueryCode = form.QueryCode != null ? form.QueryCode.ToUpper() : null;
            searchModel.IdNhomDuLieuFilter = form.IdNhomDuLieuFilter;
            SessionManager.SetValue(SessionSearchString, searchModel);
            var data = _dm_DulieuDanhmucService.GetData_QuanTri_ByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        public PartialViewResult CreateQuanTri()
        {
            var model = new CreateVM();
            ViewBag.NhomDuLieu = _dm_NhomDanhmucService.GetDropdown("GroupName", "Id");
            return PartialView("_CreateQuanTriPartial", model);
        }

        [HttpPost]
        public JsonResult CreateQuanTri(CreateQuanTriVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(model.Code) && _dm_DulieuDanhmucService.CheckCodeExisted(model.GroupId, model.Code.ToUpper()))
                    {
                        throw new Exception(String.Format("Mã nhóm {0} đã tồn tại!", model.Code.ToUpper()));
                    }
                    var dulieuDanhmuc = new DM_DulieuDanhmuc();
                    dulieuDanhmuc.GroupId = model.GroupId;
                    dulieuDanhmuc.Name = model.Name;
                    dulieuDanhmuc.Code = model.Code.ToUpper();
                    dulieuDanhmuc.Priority = model.Priority;
                    dulieuDanhmuc.Note = model.Note;
                    _dm_DulieuDanhmucService.Create(dulieuDanhmuc);
                    _ILog.Info(String.Format("Thêm mới nhóm danh mục {0}", dulieuDanhmuc.Name));
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

        public ActionResult ImportQuanTri()
        {
            var model = new ImportQuanTriVM();
            model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["ImportQuanTri_DM_DuLieuDanhMuc"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckImportQuanTri(FormCollection collection, HttpPostedFileBase fileImport)
        {
            JsonResultImportBO<DM_QuanTri_DuLieuDanhMucImport> result = new JsonResultImportBO<DM_QuanTri_DuLieuDanhMucImport>(true);
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

                var importHelper = new ImportExcelHelper<DM_QuanTri_DuLieuDanhMucImport>();
                importHelper.PathTemplate = saveFileResult.fullPath;
                //importHelper.StartCol = 2;
                importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
                importHelper.ConfigColumn = new List<ConfigModule>();
                importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<DM_QuanTri_DuLieuDanhMucImport>(collection);

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
        public JsonResult SaveImportDataQuanTri(List<DM_QuanTri_DuLieuDanhMucImport> Data)
        {
            var result = new JsonResultBO(true);

            var lstObjSave = new List<DM_DulieuDanhmuc>();
            try
            {
                foreach (var item in Data)
                {
                    var objNhom = _dm_NhomDanhmucService.GetNhomDanhMucByGroupCode(item.GroupCode);
                    if (objNhom != null)
                    {
                        var obj = new DM_DulieuDanhmuc()
                        {
                            Name = item.Name,
                            Priority = item.Priority,
                            Note = item.Note,
                            GroupId = objNhom.Id,
                            Code = item.Code,
                        };

                        _dm_DulieuDanhmucService.Create(obj);
                    }
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

        [HttpPost]
        public JsonResult GetNoteByCode(string code)
        {
            string note = _dm_DulieuDanhmucService.FindBy(x => x.Code == code).FirstOrDefault()?.Note;
            return Json(note);
        }
    }
}