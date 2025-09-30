using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.GameService.Dto;
using Hinet.Web.Areas.GameArea.Models;
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



namespace Hinet.Web.Areas.GameArea.Controllers
{
    public class GameController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "Game_index";
        public const string permissionCreate = "Game_create";
        public const string permissionEdit = "Game_edit";
        public const string permissionDelete = "Game_delete";
        public const string permissionImport = "Game_Inport";
        public const string permissionExport = "Game_export";
        public const string searchKey = "GamePageSearchModel";
        private readonly IGameService _GameService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;


        public GameController(IGameService GameService, ILog Ilog,

        IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _GameService = GameService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;

        }
        // GET: GameArea/Game
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {

            var listData = _GameService.GetDaTaByPage(null);
            ViewBag.dropdownListViTriHienThi = ConstantExtension.GetDropdownData<ViTriHienThiGameConstant>();
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as GameSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new GameSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _GameService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }
        public PartialViewResult Create()
        {
            var myModel = new CreateVM();
            ViewBag.dropdownListViTriHienThi = ConstantExtension.GetDropdownData<ViTriHienThiGameConstant>();
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
                    var EntityModel = _mapper.Map<Game>(model);
                    _GameService.Create(EntityModel);

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
            ViewBag.dropdownListViTriHienThi = ConstantExtension.GetDropdownData<ViTriHienThiGameConstant>();
            var obj = _GameService.GetById(id);
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

                    var obj = _GameService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj = _mapper.Map(model, obj);
                    _GameService.Update(obj);

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
        public JsonResult searchData(GameSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as GameSearchDto;

            if (searchModel == null)
            {
                searchModel = new GameSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.NameFilter = form.NameFilter;
            searchModel.MoTaFilter = form.MoTaFilter;
            searchModel.TrangThaiFilter = form.TrangThaiFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _GameService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _GameService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _GameService.Delete(user);
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
            model.objInfo = _GameService.GetById(id);
            return View(model);
        }
        //[PermissionAccess(Code = permissionImport)]
        public FileResult ExportExcel()
        {
            var searchModel = SessionManager.GetValue(searchKey) as GameSearchDto;
            var data = _GameService.GetDaTaByPage(searchModel).ListItem;
            var dataExport = _mapper.Map<List<GameExportDto>>(data);
            var fileExcel = ExportExcelV2Helper.Export<GameExportDto>(dataExport);
            return File(fileExcel, "application/octet-stream", "Game.xlsx");
        }
        //[PermissionAccess(Code = permissionImport)]
        public ActionResult Import()
        {
            var model = new ImportVM();
            model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_Game"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
        {
            JsonResultImportBO<GameImportDto> result = new JsonResultImportBO<GameImportDto>(true);
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
                var importHelper = new ImportExcelHelper<GameImportDto>();
                importHelper.PathTemplate = saveFileResult.fullPath;
                //importHelper.StartCol = 2;
                importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
                importHelper.ConfigColumn = new List<ConfigModule>();
                importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<GameImportDto>(collection);
                #endregion
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
            ExportExcelHelper<GameImportDto> exPro = new ExportExcelHelper<GameImportDto>();
            exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
            exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_Game"]);
            exPro.StartRow = 5;
            exPro.StartCol = 2;
            exPro.FileName = "ErrorImportGame";
            var result = exPro.ExportText(lstData);
            if (result.Status)
            {
                result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveImportData(List<GameImportDto> Data)
        {
            var result = new JsonResultBO(true);

            var lstObjSave = new List<Game>();
            try
            {
                foreach (var item in Data)
                {
                    var obj = _mapper.Map<Game>(item);
                    _GameService.Create(obj);
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