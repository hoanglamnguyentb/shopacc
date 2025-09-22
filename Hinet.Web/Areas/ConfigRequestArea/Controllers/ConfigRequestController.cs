using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.ConfigRequestService;
using Hinet.Service.ConfigRequestService.Dto;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.RoleService;
using Hinet.Web.Areas.ConfigRequestArea.Models;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Hinet.Web.Areas.ConfigRequestArea.Controllers
{
    public class ConfigRequestController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "ConfigRequest_index";
        public const string permissionCreate = "ConfigRequest_create";
        public const string permissionEdit = "ConfigRequest_edit";
        public const string permissionDelete = "ConfigRequest_delete";
        public const string permissionImport = "ConfigRequest_import";
        public const string permissionExport = "ConfigRequest_export";
        public const string searchKey = "ConfigRequestPageSearchModel";
        private readonly IConfigRequestService _ConfigRequestService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IRoleService _roleService;

        public ConfigRequestController(IConfigRequestService ConfigRequestService, ILog Ilog,
            IRoleService roleService,
        IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _roleService = roleService;
            _ConfigRequestService = ConfigRequestService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
        }

        // GET: ConfigRequestArea/ConfigRequest
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            var listData = _ConfigRequestService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as ConfigRequestSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new ConfigRequestSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _ConfigRequestService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        //[PermissionAccess(Code = permissionCreate)]
        public PartialViewResult Create()
        {
            var myModel = new CreateVM();

            List<Type> objectTypeName = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                                         from type in asm.GetTypes()
                                         where type.Namespace == "Hinet.Model.Entities" && type.IsClass && type.Name != "ConfigRequest"
                                         select type).ToList();
            List<SelectListItem> myEntities = new List<SelectListItem>();
            foreach (Type type in objectTypeName)
            {
                myEntities.Add(new SelectListItem { Text = type.Name, Value = type.Name });
            }
            List<SelectListItem> roles = new List<SelectListItem>();
            foreach (var role in _roleService.GetAll())
            {
                roles.Add(new SelectListItem { Text = role.Name, Value = role.Code });
            }
            ViewBag.MyEntities = myEntities;
            ViewBag.Roles = roles;
            ViewBag.MyProperty = new List<SelectListItem>();
            return PartialView("_CreatePartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true, "Tạo ConfigRequest thành công");
            try
            {
                if (ModelState.IsValid)
                {
                    var EntityModel = _mapper.Map<ConfigRequest>(model);

                    _ConfigRequestService.Create(EntityModel);
                }
            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
                _Ilog.Error("Lỗi tạo mới ConfigRequest", ex);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetProperty(string name)
        {
            List<SelectListItem> propertiesView = new List<SelectListItem>();

            if (name == "Chọn" || name == "Audit")
            {
                return Json(propertiesView);
            }
            Type objectType = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                               from type in asm.GetTypes()
                               where type.IsClass && type.Name == name && type.Namespace == "Hinet.Model.Entities"
                               select type).FirstOrDefault();
            PropertyInfo[] properties = objectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                propertiesView.Add(new SelectListItem { Text = property.Name, Value = property.Name });
            }
            return Json(propertiesView);
        }

        //[PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id)
        {
            var myModel = new EditVM();
            List<Type> objectTypeName = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                                         from type in asm.GetTypes()
                                         where type.Namespace == "Hinet.Model.Entities" && type.IsClass && type.Name != "ConfigRequest"
                                         select type).ToList();
            List<SelectListItem> myEntities = new List<SelectListItem>();
            foreach (Type type in objectTypeName)
            {
                myEntities.Add(new SelectListItem { Text = type.Name, Value = type.Name });
            }
            List<SelectListItem> roles = new List<SelectListItem>();

            foreach (var role in _roleService.GetAll())
            {
                roles.Add(new SelectListItem { Text = role.Name, Value = role.Code });
            }
            ViewBag.MyEntities = myEntities;
            ViewBag.Roles = roles;
            ViewBag.MyProperty = new List<SelectListItem>();
            var obj = _ConfigRequestService.GetById(id);
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
                    var obj = _ConfigRequestService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj = _mapper.Map(model, obj);

                    _ConfigRequestService.Update(obj);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không cập nhật được";
                _Ilog.Error("Lỗi cập nhật thông tin ConfigRequest", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult searchData(ConfigRequestSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as ConfigRequestSearchDto;

            if (searchModel == null)
            {
                searchModel = new ConfigRequestSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.CodeFilter = form.CodeFilter;
            searchModel.NameFilter = form.NameFilter;
            searchModel.AccessInforFilter = form.AccessInforFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _ConfigRequestService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [PermissionAccess(Code = permissionDelete)]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultBO(true, "Xóa ConfigRequest thành công");
            try
            {
                var user = _ConfigRequestService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _ConfigRequestService.Delete(user);
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
            model.objInfo = _ConfigRequestService.GetDtoById(id);
            return View(model);
        }

        //[PermissionAccess(Code = permissionExport)]
        //[PermissionAccess(Code = permissionImport)]
        public FileResult ExportExcel()
        {
            var searchModel = SessionManager.GetValue(searchKey) as ConfigRequestSearchDto;
            var data = _ConfigRequestService.GetDaTaByPage(searchModel).ListItem;
            var dataExport = _mapper.Map<List<ConfigRequestExportDto>>(data);
            var fileExcel = ExportExcelV2Helper.Export<ConfigRequestExportDto>(dataExport);
            return File(fileExcel, "application/octet-stream", "ConfigRequest.xlsx");
        }

        //[PermissionAccess(Code = permissionImport)]
        //[PermissionAccess(Code = permissionImport)]
        public ActionResult Import()
        {
            var model = new ImportVM();
            model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_ConfigRequest"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
        {
            JsonResultImportBO<ConfigRequestImportDto> result = new JsonResultImportBO<ConfigRequestImportDto>(true);
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

                var importHelper = new ImportExcelHelper<ConfigRequestImportDto>();
                importHelper.PathTemplate = saveFileResult.fullPath;
                //importHelper.StartCol = 2;
                importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
                importHelper.ConfigColumn = new List<ConfigModule>();
                importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<ConfigRequestImportDto>(collection);

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
            ExportExcelHelper<ConfigRequestImportDto> exPro = new ExportExcelHelper<ConfigRequestImportDto>();
            exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
            exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_ConfigRequest"]);
            exPro.StartRow = 5;
            exPro.StartCol = 2;
            exPro.FileName = "ErrorImportConfigRequest";
            var result = exPro.ExportText(lstData);
            if (result.Status)
            {
                result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveImportData(List<ConfigRequestImportDto> Data)
        {
            var result = new JsonResultBO(true);

            var lstObjSave = new List<ConfigRequest>();
            try
            {
                foreach (var item in Data)
                {
                    var obj = _mapper.Map<ConfigRequest>(item);
                    _ConfigRequestService.Create(obj);
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