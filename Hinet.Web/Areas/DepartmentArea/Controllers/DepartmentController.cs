using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DepartmentService;
using Hinet.Service.DepartmentService.Dto;
using Hinet.Service.DepartmentService.DTO;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.HistoryService;
using Hinet.Service.ModuleService;
using Hinet.Service.RoleService;
using Hinet.Web.Areas.DepartmentArea.Models;
using Hinet.Web.Common;
using Hinet.Web.Core;
using Hinet.Web.Filters;
using log4net;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DepartmentArea.Controllers
{
    public class DepartmentController : BaseController
    {
        // GET: DepartmentArea/Department
        private IDepartmentService _departmentService;

        private ILog _ILog;
        private IMapper _mapper;
        public const string permissionIndex = "Department_index";
        public const string permissionDetail = "Department_detail";
        public const string permissionCreate = "Department_create";
        public const string permissionEdit = "Department_edit";
        public const string permissionDelete = "Department_delete";
        public const string permissionImport = "Department_import";
        private const string SessionSearchString = "DepartmentSearch";
        private readonly IRoleService _roleService;
        private readonly IModuleService _moduleService;
        private readonly IAppUserService _appUserService;
        private readonly IHistoryService _historyService;

        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;

        public DepartmentController(IDepartmentService departmentService,
            IAppUserService appUserService,
            IMapper mapper,
            IRoleService roleService,
            IModuleService moduleService,
            IHistoryService historyService, ILog ILog,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService
            )
        {
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;

            _historyService = historyService;
            _appUserService = appUserService;
            _roleService = roleService;
            _mapper = mapper;
            _departmentService = departmentService;
            _ILog = ILog;
            _moduleService = moduleService;
        }

        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            ViewBag.isDetail = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionDetail);
            ViewBag.isCreate = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionCreate);
            ViewBag.isEdit = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionEdit);
            ViewBag.isDelete = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionDelete);
            ViewBag.isImport = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionImport);
            var searchModel = new DepartmentDTO();
            SessionManager.SetValue(SessionSearchString, null);
            var data = _departmentService.GetTree();
            return View("Index", data);
        }

        public JsonResult ReloadTree()
        {
            var result = new JsonResultBO(true);
            try
            {
                result.Param = _departmentService.GetTree();
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không lấy được dữ liệu!";
                _ILog.Error("Lỗi tải lại cây Khoa phòng", ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ConfigLimitData(long id)
        {
            var model = new ConfigLimitDataVM();
            model.department = _departmentService.GetById(id);

            return PartialView(model);
        }

        public PartialViewResult DetailDepartment(long id)
        {
            var model = new DepartmentDetailVM();
            model.department = _departmentService.GetById(id);
            model.modules = _moduleService.GetListModuleLimitData();

            return PartialView(model);
        }

        [PermissionAccess(Code = permissionDetail)]
        public PartialViewResult DetailPartial(long id)
        {
            var model = new DetailVM();
            model.ObjInfo = _departmentService.GetInfoDto(id);
            model.historyDtos = _historyService.GetDaTaByIdHistory(id, ItemTypeConstant.Department);

            return PartialView("DetailPartial", model);
        }

        [HttpPost]
        public JsonResult GetDepartmentByMa(long? Ma)
        {
            var department = _departmentService.GetById(Ma);
            return Json(department);
        }

        #region Lưu thay đổi thứ tự

        [HttpPost]
        public JsonResult SaveChanges(string jsonStr = "")
        {
            var result = new JsonResultBO(true);
            try
            {
                if (!String.IsNullOrEmpty(jsonStr))
                {
                    // Đánh thứ tự
                    long priorityCounter = 1;
                    // Cây từ Client
                    List<DepartmentCM> listObj = JsonConvert.DeserializeObject<List<DepartmentCM>>(jsonStr);
                    // Nút gốc
                    Department rootObj = _departmentService.FindBy(x => x.ParentId == null).FirstOrDefault();

                    foreach (DepartmentCM obj in listObj)
                    {
                        // Object thuộc tầng đầu tiên
                        Department floorModel = _departmentService.GetById(obj.id);

                        // Nếu object đứng cùng hàng với object gốc -> thành con object gốc
                        if (floorModel.ParentId != null)
                        {
                            floorModel.ParentId = rootObj.Id;
                            floorModel.Level = rootObj.Level + 1;
                        }
                        floorModel.Priority = priorityCounter++;
                        _departmentService.Update(floorModel);
                        if (obj.children != null && obj.children.Count > 0)
                        {
                            UpdateChild(ref priorityCounter, obj);
                        }
                    }
                    result.Message = "Cập nhật thay đổi thành công";
                }
                else
                {
                    result.Status = false;
                    result.Message = "Không có thay đổi";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không lưu được thay đổi";
                _ILog.Error("Lỗi lưu thay đổi chỉnh sửa cây Khoa phòng", ex);
            }
            return Json(result);
        }

        private void UpdateChild(ref long priorityCounter, DepartmentCM obj)
        {
            foreach (DepartmentCM child in obj.children)
            {
                Department nextModel = _departmentService.GetById(child.id);
                nextModel.ParentId = obj.id;
                nextModel.Level = _departmentService.FindBy(x => x.Id == obj.id).Select(x => x.Level).FirstOrDefault() + 1;
                nextModel.Priority = priorityCounter++;
                _departmentService.Update(nextModel);
                if (child.children != null)
                {
                    UpdateChild(ref priorityCounter, child);
                }
            }
        }

        #endregion Lưu thay đổi thứ tự

        [PermissionAccess(Code = permissionCreate)]
        public PartialViewResult Create(long id = 0)
        {
            var model = new CreateVM();
            if (id > 0)
            {
                model.ParentId = id;
            }
            ViewBag.ListDepartment = _departmentService.GetDropdown("Name", "Id", (long)id);
            ViewBag.ListRole = _roleService.GetDropdown("Name", "Id");
            ViewBag.DropdownListLoai = ConstantExtension.GetDropdownData<DepartmentTypeConstant>();

            return PartialView("_CreatePartial", model);
        }

        [HttpPost]
        public JsonResult Create(CreateVM model)
        {
            ViewBag.DropdownListLoai = ConstantExtension.GetDropdownData<DepartmentTypeConstant>();

            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(model.Code) && _departmentService.CheckCodeExisted(model.Code.ToUpper()))
                    {
                        throw new Exception(String.Format("Mã phòng ban {0} đã tồn tại!", model.Code.ToUpper()));
                    }
                    var savedModel = new Department();
                    savedModel.Name = model.Name;
                    savedModel.Code = model.Code.ToUpper();
                    savedModel.ParentId = model.ParentId;
                    savedModel.Loai = model.Loai;
                    savedModel.Mota = model.Mota;
                    savedModel.Status = DepartmentStatusConstant.HoatDong;
                    savedModel.Level = model.ParentId.HasValue
                        ? _departmentService.FindBy(x => x.Id == model.ParentId).Select(x => x.Level).FirstOrDefault() + 1
                        : 1;
                    if (model.IsAllProvine == true)
                    {
                        savedModel.IsAllProvine = true;
                    }
                    else
                    {
                        if (model.Province != null && model.Province.Any())
                        {
                            savedModel.ProvinceManagement = string.Join(",", model.Province);
                        }
                    }
                    savedModel.DefaultRole = model.DefaultRole.GetValueOrDefault();
                    _departmentService.Create(savedModel);
                    _ILog.Info(String.Format("Thêm mới phòng ban {0}", savedModel.Name));

                    #region History

                    var history = new History();
                    history.TypeItem = ItemTypeConstant.Department;
                    history.IdItem = savedModel.Id;
                    history.LogId = CurrentUserId;
                    history.HistoryContent = CurrentUserInfo.FullName + " đã tạo mới " + ConstantExtension.GetName<DepartmentTypeConstant>(savedModel.Loai) + " " + savedModel.Name;
                    _historyService.Create(history);

                    #endregion History
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
                _ILog.Error("Lỗi khởi tạo phòng ban", ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id = 0)
        {
            var model = new EditVM();
            var existedModel = _departmentService.GetById(id);
            if (existedModel == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin phòng ban");
            }
            model = new EditVM()
            {
                Id = existedModel.Id,
                Name = existedModel.Name,
                Code = existedModel.Code,
                Loai = existedModel.Loai,
                ParentId = existedModel.ParentId,
                Level = existedModel.Level,
                Province = !string.IsNullOrEmpty(existedModel.ProvinceManagement) ? existedModel.ProvinceManagement.Split(',').ToList() : new List<string>(),
                DefaultRole = existedModel.DefaultRole,
                IsAllProvine = existedModel.IsAllProvine,
                IsHigh = existedModel.IsHigh,
                Mota = existedModel.Mota
            };
            ViewBag.ListDepartment = _departmentService.GetDropdown("Name", "Id", model.ParentId);

            ViewBag.ListRole = _roleService.GetDropdown("Name", "Id", model.DefaultRole);
            ViewBag.DropdownListLoai = ConstantExtension.GetDropdownData<DepartmentTypeConstant>(model.Loai);
            ViewBag.lstLoaiDoThi = _dM_DulieuDanhmucService.GetDropDownListByGroup(DanhMucConstant.LOAIDOTHI);
            return PartialView("_EditPartial", model);
        }

        public ActionResult SapNhap(long id)
        {
            var obj = _departmentService.GetInfoDto(id);
            ViewBag.DepartmentInfo = obj;
            ViewBag.LstChucVu = _dM_DulieuDanhmucService.GetDropDownListByCodeGroup(DanhMucConstant.ChucVu);
            ViewBag.LstDepartment = _departmentService.GetTreeDropdownList(null, string.Empty);
            var model = new SapNhapVM();
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult SapNhap(SapNhapVM model, HttpPostedFileBase File)
        {
            var obj = _departmentService.GetInfoDto(model.Id);
            ViewBag.DepartmentInfo = obj;
            ViewBag.LstChucVu = _dM_DulieuDanhmucService.GetDropDownListByCodeGroup(DanhMucConstant.ChucVu);
            ViewBag.LstDepartment = _departmentService.GetTreeDropdownList(null, string.Empty);
            try
            {
                var old = _departmentService.GetById(model.Id);
                old.Status = DepartmentStatusConstant.DaGiaiThe;
                var newPa = _departmentService.GetById(model.IdNew);

                #region History

                var pathFIle = string.Empty;

                var history = new History();
                history.TypeItem = ItemTypeConstant.Department;
                history.IdItem = old.Id;
                history.LogId = CurrentUserId;
                if (File != null)
                {
                    var uploadResult = UploadProvider.SaveFile(File, null, UploadProvider.ListExtensionCommon, UploadProvider.MaxSizeCommon, "Uploads/QuyetDinh", Server.MapPath("/"));
                    if (uploadResult.status)
                    {
                        pathFIle = uploadResult.path;
                    }
                    else
                    {
                        ModelState.AddModelError("File", uploadResult.message);
                        return View();
                    }
                }
                history.HistoryContent = CurrentUserInfo.FullName + " đã sáp nhập Khoa phòng " + ConstantExtension.GetName<DepartmentTypeConstant>(old.Loai) + " " + old.Name + " vào " + newPa.Name;
                var stringThongTinQuyetDinh = $"<p>Số quyết định: {model.thongTinQuyetDinhVM.SoQuyetDinh}</p>";
                stringThongTinQuyetDinh += $"<p>Ngày quyết định: {string.Format("{0:dd/MM/yyyy}", model.thongTinQuyetDinhVM.NgayQuyetDinh)}</p>";
                stringThongTinQuyetDinh += $"<p>Người ký: {model.thongTinQuyetDinhVM.NguoiKy}</p>";
                stringThongTinQuyetDinh += $"<p>Ghi chú: {model.thongTinQuyetDinhVM.GhiChu}</p>";
                if (!string.IsNullOrEmpty(pathFIle))
                {
                    pathFIle = pathFIle.StandardPath();
                    stringThongTinQuyetDinh += $"<p>Tệp đính kèm: <a href='/{pathFIle}' download>Tải xuống</a></p>";
                }

                history.Comment = stringThongTinQuyetDinh;
                _historyService.Create(history);

                var historyNew = new History();
                historyNew.TypeItem = ItemTypeConstant.Department;
                historyNew.IdItem = newPa.Id;
                historyNew.LogId = CurrentUserId;
                historyNew.HistoryContent = CurrentUserInfo.FullName + " đã sáp nhập Khoa phòng " + ConstantExtension.GetName<DepartmentTypeConstant>(old.Loai) + " " + old.Name + " vào " + newPa.Name;
                historyNew.Comment = stringThongTinQuyetDinh;
                _historyService.Create(historyNew);

                #endregion History

                return RedirectToAction("Detail", new { id = newPa.Id });
            }
            catch (Exception ex)
            {
                _ILog.Error(ex.Message, ex);
            }

            return View(model);
        }

        public ActionResult TachPhongBan(long id)
        {
            var obj = _departmentService.GetInfoDto(id);
            ViewBag.DepartmentInfo = obj;
            ViewBag.LstChucVu = _dM_DulieuDanhmucService.GetDropDownListByCodeGroup(DanhMucConstant.ChucVu);
            ViewBag.LstDepartment = _departmentService.GetTreeDropdownList(null, string.Empty);
            var model = new TachPhongVM();
            model.Id = id;
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var model = new DetailVM();
            model.ObjInfo = _departmentService.GetInfoDto(id);
            model.historyDtos = _historyService.GetDaTaByIdHistory(id, ItemTypeConstant.Department);

            return View(model);
        }

        [HttpPost]
        public ActionResult TachPhongBan(TachPhongVM model, HttpPostedFileBase File)
        {
            var obj = _departmentService.GetInfoDto(model.Id);
            ViewBag.DepartmentInfo = obj;
            ViewBag.LstChucVu = _dM_DulieuDanhmucService.GetDropDownListByCodeGroup(DanhMucConstant.ChucVu);
            ViewBag.LstDepartment = _departmentService.GetTreeDropdownList(null, string.Empty);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(model.phongBanNewVM.Code) && _departmentService.CheckCodeExisted(model.phongBanNewVM.Code.ToUpper()))
                    {
                        ModelState.AddModelError("phongBanNewVM.Code", String.Format("Mã Khoa phòng {0} đã tồn tại!", model.phongBanNewVM.Code.ToUpper()));
                        return View(model);
                    }
                    var savedModel = new Department();
                    savedModel.Name = model.phongBanNewVM.Name;
                    savedModel.Code = model.phongBanNewVM.Code.ToUpper();
                    savedModel.ParentId = model.phongBanNewVM.ParentId;
                    savedModel.Loai = model.phongBanNewVM.Loai;
                    savedModel.Status = DepartmentStatusConstant.HoatDong;
                    savedModel.Level = model.phongBanNewVM.ParentId.HasValue
                        ? _departmentService.FindBy(x => x.Id == model.phongBanNewVM.ParentId).Select(x => x.Level).FirstOrDefault() + 1
                        : 1;

                    savedModel.DefaultRole = model.phongBanNewVM.DefaultRole.GetValueOrDefault();
                    _departmentService.Create(savedModel);

                    #region History

                    var pathFIle = string.Empty;

                    var history = new History();
                    history.TypeItem = ItemTypeConstant.Department;
                    history.IdItem = savedModel.Id;
                    history.LogId = CurrentUserId;
                    if (File != null)
                    {
                        var uploadResult = UploadProvider.SaveFile(File, null, UploadProvider.ListExtensionCommon, UploadProvider.MaxSizeCommon, "Uploads/QuyetDinh", Server.MapPath("/"));
                        if (uploadResult.status)
                        {
                            pathFIle = uploadResult.path;
                        }
                        else
                        {
                            ModelState.AddModelError("File", uploadResult.message);
                            return View();
                        }
                    }
                    history.HistoryContent = CurrentUserInfo.FullName + " đã tạo Khoa phòng " + ConstantExtension.GetName<DepartmentTypeConstant>(savedModel.Loai) + " " + savedModel.Name;
                    var stringThongTinQuyetDinh = $"<p>Số quyết định: {model.thongTinQuyetDinhVM.SoQuyetDinh}</p>";
                    stringThongTinQuyetDinh += $"<p>Ngày quyết định: {string.Format("{0:dd/MM/yyyy}", model.thongTinQuyetDinhVM.NgayQuyetDinh)}</p>";
                    stringThongTinQuyetDinh += $"<p>Người ký: {model.thongTinQuyetDinhVM.NguoiKy}</p>";
                    stringThongTinQuyetDinh += $"<p>Ghi chú: {model.thongTinQuyetDinhVM.GhiChu}</p>";
                    if (!string.IsNullOrEmpty(pathFIle))
                    {
                        pathFIle = pathFIle.StandardPath();
                        stringThongTinQuyetDinh += $"<p>Tệp đính kèm: <a href='/{pathFIle}' download>Tải xuống</a></p>";
                    }

                    history.Comment = stringThongTinQuyetDinh;
                    _historyService.Create(history);

                    #endregion History

                    return RedirectToAction("Detail", new { id = savedModel.Id });
                }
            }
            catch (Exception ex)
            {
                _ILog.Error("Lỗi khởi tạo Khoa phòng", ex);
                return View(model);
            }
            return View(model);
        }

        public PartialViewResult GiaiThe(long id)
        {
            var obj = _departmentService.GetInfoDto(id);
            ViewBag.DepartmentInfo = obj;
            var model = new GiaiTheVM();
            model.Id = id;
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult GiaiThe(GiaiTheVM model, HttpPostedFileBase File)
        {
            var result = new JsonResultBO(true);
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = _departmentService.GetById(model.Id);
                    obj.Status = DepartmentStatusConstant.DaGiaiThe;
                    var pathFIle = string.Empty;
                    _departmentService.Update(obj);
                    if (File != null)
                    {
                        var uploadResult = UploadProvider.SaveFile(File, null, UploadProvider.ListExtensionCommon, UploadProvider.MaxSizeCommon, "Uploads/QuyetDinh", Server.MapPath("/"));
                        if (uploadResult.status)
                        {
                            pathFIle = uploadResult.path;
                        }
                    }

                    #region History

                    var history = new History();
                    history.TypeItem = ItemTypeConstant.Department;
                    history.IdItem = obj.Id;
                    history.LogId = CurrentUserId;
                    history.HistoryContent = CurrentUserInfo.FullName + " đã giải thể " + ConstantExtension.GetName<DepartmentTypeConstant>(obj.Loai) + " " + obj.Name;
                    var stringThongTinQuyetDinh = $"<p>Số quyết định: {model.SoQuyetDinh}</p>";
                    stringThongTinQuyetDinh += $"<p>Ngày quyết định: {string.Format("{0:dd/MM/yyyy}", model.NgayQuyetDinh)}</p>";
                    stringThongTinQuyetDinh += $"<p>Người ký: {model.NguoiKy}</p>";
                    stringThongTinQuyetDinh += $"<p>Ghi chú: {model.GhiChu}</p>";
                    if (!string.IsNullOrEmpty(pathFIle))
                    {
                        pathFIle = pathFIle.StandardPath();
                        stringThongTinQuyetDinh += $"<p>Tệp đính kèm: <a href='/{pathFIle}' download>Tải xuống</a></p>";
                    }

                    history.Comment = stringThongTinQuyetDinh;
                    _historyService.Create(history);

                    #endregion History
                }
                catch (Exception ex)
                {
                    _ILog.Error(ex.Message, ex);
                    result.MessageFail(ex.Message);
                }
            }
            return Json(result);
        }

        [HttpGet]
        public PartialViewResult ListDepartmentUser(long idDepartment)
        {
            var model = new ListDepartmentUserViewModel();
            model.ListUser = _appUserService.getListUserInDepartment(idDepartment);
            return PartialView("_ListDepartmentUser", model);
        }

        [HttpPost]
        public JsonResult Edit(EditVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    var existedModel = _departmentService.GetById(model.Id);
                    if (existedModel == null)
                    {
                        throw new Exception("Không tìm thấy phòng ban");
                    }
                    else
                    {
                        var strmss = HistoryExtension.GetChange<Department, EditVM>(existedModel, model);
                        if (existedModel.Code.Equals(model.Code.ToUpper()))
                        {
                            existedModel.ParentId = model.ParentId;
                            existedModel.Name = model.Name;
                            existedModel.Level = _departmentService.FindBy(x => x.Id == model.ParentId).Select(x => x.Level).FirstOrDefault() + 1;
                        }
                        else if (_departmentService.CheckCodeExisted(model.Code.ToUpper(), model.Id))
                        {
                            throw new Exception(String.Format("Mã phòng ban {0} đã tồn tại", model.Code.ToUpper()));
                        }
                        else
                        {
                            existedModel.Name = model.Name;
                            existedModel.Loai = model.Loai;
                            existedModel.Code = model.Code.ToUpper();
                            existedModel.ParentId = model.ParentId;
                            existedModel.Level = _departmentService.FindBy(x => x.Id == model.ParentId).Select(x => x.Level).FirstOrDefault() + 1;
                        }

                        if (model.IsAllProvine == true)
                        {
                            existedModel.IsAllProvine = true;
                            existedModel.ProvinceManagement = string.Empty;
                        }
                        else
                        {
                            if (model.Province != null && model.Province.Any())
                            {
                                existedModel.ProvinceManagement = string.Join(",", model.Province);
                                existedModel.IsAllProvine = false;
                            }
                            else
                            {
                                existedModel.ProvinceManagement = null;
                                existedModel.IsAllProvine = false;
                            }
                        }
                        existedModel.DefaultRole = model.DefaultRole.GetValueOrDefault();
                        existedModel.IsHigh = model.IsHigh;
                        existedModel.Mota = model.Mota;
                        _departmentService.Update(existedModel);

                        #region History

                        var history = new History();
                        history.TypeItem = ItemTypeConstant.Department;
                        history.IdItem = existedModel.Id;
                        history.LogId = CurrentUserId;
                        history.HistoryContent = CurrentUserInfo.FullName + " đã cập nhật thông tin đơn vị " + existedModel.Name;
                        history.Comment = strmss;
                        _historyService.Create(history);

                        #endregion History
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không cập nhật được!";
                _ILog.Error("Lỗi chỉnh sửa phòng ban", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionDelete)]
        public JsonResult Delete(long id)
        {
            JsonResultBO result = new JsonResultBO(true);
            try
            {
                Department entity = _departmentService.GetById(id);
                if (entity != null)
                {
                    // Chỉ xoá khi nút được trỏ tới là nút lá (không có con)
                    List<Department> entityChildren = _departmentService.FindBy(x => x.ParentId == entity.Id).ToList();
                    if (entityChildren != null && entityChildren.Count > 0)
                    {
                        result.Status = false;
                        result.Message = "Không thể xoá phòng ban";
                        _ILog.Info(result.Message);
                    }
                    else
                    {
                        _departmentService.Delete(entity);
                        result.Message = "Xóa phòng ban thành công";
                        _ILog.Info(result.Message);
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = "Cấu hình không tồn tại";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Xóa phòng ban không thành công";
                _ILog.Error("Xóa phòng ban không thành công", ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionImport)]
        public FileResult ExportExcel(long id)
        {
            // Lấy dữ liệu của phòng ban và các con của nó từ service
            var data = _departmentService.GetDepartmentWithChildren(id);

            // Chuẩn bị danh sách dữ liệu để xuất ra Excel
            var dataToExport = _departmentService.GetAllDepartmentsWithChildren(data);

            // Chuẩn bị file Excel
            var memoryStream = new MemoryStream();
            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("DanhMucDonVi");

                // Điền tiêu đề
                worksheet.Cells[1, 1].Value = "Danh mục đơn vị";
                worksheet.Cells[1, 1, 1, 5].Merge = true;  // Hợp nhất các ô từ cột 1 đến cột 8
                worksheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 1, 5].Style.Font.Size = 16;  // Tăng kích thước chữ
                worksheet.Cells[1, 1, 1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Điền tiêu đề các cột
                worksheet.Cells[2, 1].Value = "Level";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 2].Value = "Tên khoa phòng";
                worksheet.Cells[2, 2].Style.Font.Bold = true;
                worksheet.Cells[2, 3].Value = "Mã khoa phòng";
                worksheet.Cells[2, 3].Style.Font.Bold = true;
                worksheet.Cells[2, 4].Value = "Tên khoa cha";
                worksheet.Cells[2, 4].Style.Font.Bold = true;

                // Điền dữ liệu vào worksheet từ danh sách dataToExport
                int rowIndex = 3;
                foreach (var department in dataToExport)
                {
                    worksheet.Cells[rowIndex, 1].Value = department.Level;
                    worksheet.Cells[rowIndex, 2].Value = department.Name;
                    worksheet.Cells[rowIndex, 3].Value = department.Code;
                    if (department.ParentId != null)
                    {
                        worksheet.Cells[rowIndex, 4].Value = _departmentService.GetName(department.ParentId.Value);
                    }

                    rowIndex++;
                }

                // Tự động điều chỉnh định dạng cột để vừa với nội dung
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Lưu file Excel vào memoryStream
                package.Save();
            }

            // Chuẩn bị trả về file Excel dưới dạng FileResult
            memoryStream.Position = 0;
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhMucDonVi.xlsx");
        }

        [PermissionAccess(Code = permissionImport)]
        public ActionResult Import(long departmentId)
        {
            var model = new ImportVM();
            model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_Department"]);
            model.DepartmentId = departmentId;
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
        {
            var data = new DepartmentImportWithDepartmentIdDto();
            JsonResultImportBO<DepartmentImportDto> result = new JsonResultImportBO<DepartmentImportDto>(true);
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

                var importHelper = new ImportExcelHelper<DepartmentImportDto>();
                importHelper.PathTemplate = saveFileResult.fullPath;
                //importHelper.StartCol = 2;
                importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
                importHelper.ConfigColumn = new List<ConfigModule>();
                importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<DepartmentImportDto>(collection);

                #endregion Config để import dữ liệu

                var rsl = importHelper.ImportCustomRow();
                if (rsl.ListTrue != null && rsl.ListTrue.Any())
                {
                    var lstTrueCheckTrung = rsl.ListTrue.ToList();
                    rsl.ListTrue = new List<DepartmentImportDto>();
                    foreach (var item in lstTrueCheckTrung)
                    {
                        if (!string.IsNullOrEmpty(item.Code) && !rsl.ListTrue.Any(x => x.Code.Equals(item.Code)))
                        {
                            var CheckCodeExist = _departmentService.CheckCodeExisted(item.Code);
                            if (CheckCodeExist == true)
                            {
                                var stringErr = "+ Mã Khoa phòng trùng lặp \n";
                                rsl.lstFalse.Add(ExcelImportExtention.GetErrMess<DepartmentImportDto>(item, stringErr));
                            }
                            else
                            {
                                item.Code = item.Code;
                                rsl.ListTrue.Add(item);
                            }
                        }
                        else
                        {
                            var stringErr = "+ Mã Khoa phòng trùng lặp \n";
                            rsl.lstFalse.Add(ExcelImportExtention.GetErrMess<DepartmentImportDto>(item, stringErr));
                        }
                    }
                }
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
            data.ListResult = result;
            data.DepartmentId = collection["DepartmentId"].ToLongOrZero();
            return View(data);
        }

        [HttpPost]
        public JsonResult GetExportError(long DepartmentId, List<List<string>> lstData)
        {
            ExportExcelHelper<DepartmentImportDto> exPro = new ExportExcelHelper<DepartmentImportDto>();
            exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
            exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_Department"]);
            exPro.StartRow = 5;
            exPro.StartCol = 2;
            exPro.FileName = "ErrorImportDepartment";
            var result = exPro.ExportText(lstData);
            if (result.Status)
            {
                result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveImportData(long DepartmentId, List<DepartmentImportDto> Data)
        {
            var result = new JsonResultBO(true);

            var lstObjSave = new List<Department>();
            try
            {
                foreach (var item in Data)
                {
                    var obj = _mapper.Map<Department>(item);
                    var ParentObj = _departmentService.GetById(DepartmentId);
                    if (ParentObj != null)
                    {
                        obj.ParentId = ParentObj.Id;
                        obj.Level = ParentObj.Level + 1;
                    }
                    _departmentService.Create(obj);
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

        /// <summary>
        /// DepartmentArea/Department/GetDonViFromDTI
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public JsonResult GetDonViFromDTI()
        //{
        //    var result = new JsonResultBO(true, "Cập nhật đơn vị thành công");

        //    var rootId = 1L;
        //    var listCapDanhGia = new List<int>()
        //    {
        //        62,63,618
        //    };
        //    var connectionString = "Data Source=DUYPC;Initial Catalog=DTI_BACGIANG_2023;User ID=sa;Password=12345678;";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        foreach (var idCapDanhGia in listCapDanhGia)
        //        {
        //            var selectQuery = $"SELECT * FROM CdsQuanLyDonVi WHERE CapDanhGia = {idCapDanhGia}";
        //            using (SqlCommand readCmd = new SqlCommand(selectQuery, connection))
        //            {
        //                using (SqlDataReader reader = readCmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var idDonvi = int.Parse(reader["Id"].ToString());
        //                        var nameDonVi = reader["TenDonVi"].ToString();
        //                        var codeDonVi = StringUtilities.RemoveSign4VietnameseString(nameDonVi).Replace(" ", string.Empty) + "_" + idDonvi;
        //                        var idParentFromDTI = int.Parse(reader["DonViCha"].ToString());

        //                        var idParentFromSDI = rootId;
        //                        if (idCapDanhGia == 618)
        //                        {
        //                            idParentFromSDI = _departmentService
        //                            .FindBy(d => d.Code.Contains("_" + idParentFromDTI))
        //                            .Select(d => d.Id)
        //                            .FirstOrDefault();
        //                        }

        //                        var dept = new Department()
        //                        {
        //                            Name = nameDonVi,
        //                            Code = codeDonVi,
        //                            ParentId = idParentFromSDI,
        //                            Level = 0,
        //                            DefaultRole = 0,
        //                            Loai = "DonVi",
        //                            IsAllProvine = false
        //                        };
        //                        _departmentService.Save(dept);

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return Json(result);
        //}
    }
}