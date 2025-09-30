using AutoMapper;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.ModuleService;
using Hinet.Service.ModuleService.DTO;
using Hinet.Service.OperationService;
using Hinet.Web.Areas.ModuleArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using static Hinet.Web.Areas.ModuleArea.Models.ModuleViewModel;

namespace Hinet.Web.Areas.ModuleArea.Controllers

/*
 * @author:duynn
 * @create_date: 19/04/2019
 */
{
    public class ModuleController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "Module_index";
        public const string permissionCreate = "Module_create";
        public const string permissionEdit = "Module_edit";
        public const string permissionDelete = "Module_delete";
        public const string permissionDetail = "Module_detail";
        public const string permissionImport = "Module_Inport";
        public const string permissionExport = "Module_export";
        private readonly IModuleService _moduleService;
        private readonly IOperationService _operationService;

        public ModuleController(IModuleService moduleService,
            IOperationService operationService,
            ILog Ilog, IMapper mapper)
        {
            _operationService = operationService;
            _moduleService = moduleService;
            _Ilog = Ilog;
            _mapper = mapper;
        }

        // GET: ModuleArea/Module
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            var searchModel = new ModuleSearchDTO();
            SessionManager.SetValue("ModuleSearch", new ModuleSearchDTO());
            ModuleIndexViewModel viewModel = new ModuleIndexViewModel()
            {
                GroupData = _moduleService.GetDataByPage(searchModel)
            };
            return View(viewModel);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult GetData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue("ModuleSearch") as ModuleSearchDTO;
            if (searchModel == null)
            {
                searchModel = new ModuleSearchDTO();
            }
            if (!string.IsNullOrEmpty(sortQuery))
            {
                searchModel.sortQuery = sortQuery;
            }
            if (pageSize > 0)
            {
                searchModel.pageSize = pageSize;
            }
            SessionManager.SetValue("ModuleSearch", searchModel);
            var data = _moduleService.GetDataByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        [PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id = 0)
        {
            var viewModel = new ModuleEditViewModel();
            var editEntity = _moduleService.GetById(id) ?? new Module() { IsShow = true };
            viewModel = new ModuleEditViewModel()
            {
                Id = editEntity.Id,
                Name = editEntity.Name,
                IsShow = editEntity.IsShow,
                Order = editEntity.Order.ToString(),
                Code = editEntity.Code,
                StyleCss = editEntity.StyleCss,
                ClassCss = editEntity.ClassCss,
                Icon = editEntity.Icon,
                IsMobile = editEntity.IsMobile,
            };
            return PartialView("_EditPartial", viewModel);
        }

        [PermissionAccess(Code = permissionIndex)]
        public PartialViewResult ChuyenModule(int id)
        {
            var viewModel = new ChuyenModuleVM();
            viewModel.IdOld = id;
            ViewBag.lstModule = _moduleService.GetDropdown("Name", "Id");

            return PartialView(viewModel);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult ChuyenModule(ChuyenModuleVM model)
        {
            var result = new JsonResultBO(true);
            if (model.IdNew == model.IdOld)
            {
                result.MessageFail("Chức năng chuyển đến phải khác chức năng hiện tại");
                return Json(result);
            }
            var lstThaoTac = _operationService.GetDanhSachOperationOfModule(model.IdOld);
            if (lstThaoTac != null && lstThaoTac.Any())
            {
                foreach (var item in lstThaoTac)
                {
                    item.ModuleId = model.IdNew;

                    _operationService.Update(item);
                }
            }
            var moduleold = _moduleService.GetById(model.IdOld);
            _moduleService.Delete(moduleold);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult SearchData(FormCollection form)
        {
            var searchModel = SessionManager.GetValue("ModuleSearch") as ModuleSearchDTO;

            if (searchModel == null)
            {
                searchModel = new ModuleSearchDTO();
                searchModel.pageSize = 10;
            }
            searchModel.QueryCode = form["QUERY_CODE"];
            searchModel.QueryName = form["QUERY_NAME"];
            searchModel.QueryIcon = form["QUERY_ICON"];
            searchModel.QueryClassCss = form["QUERY_CLASS_CSS"];
            searchModel.QueryStyleCss = form["QUERY_STYLE_CSS"];
            searchModel.QueryIsShow = !string.IsNullOrEmpty(form["QUERY_SHOW"]) ? (bool?)(int.Parse(form["QUERY_SHOW"]) > 0) : null;
            SessionManager.SetValue("ModuleSearch", searchModel);
            var data = _moduleService.GetDataByPage(searchModel, 1);
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult Save(ModuleEditViewModel model, HttpPostedFileBase file)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var resultUpload = UploadProvider.SaveFile(file, null, ".jpg,.png,.ico", null, "Uploads/CommonIcons/", HostingEnvironment.MapPath("/"));

                        if (resultUpload.status == true)
                        {
                            model.Icon = resultUpload.path;
                        }
                    }
                    if (model.Id <= 0)
                    {
                        if (_moduleService.CheckExistCode(model.Code))
                        {
                            throw new Exception(string.Format("Mã {0} đã tồn tại", model.Code));
                        }
                        Module entity = new Module()
                        {
                            Code = model.Code,
                            ClassCss = model.ClassCss,
                            StyleCss = model.StyleCss,
                            Icon = model.Icon,
                            Name = model.Name,
                            IsShow = model.IsShow,
                            Order = model.Order.ToNumber<int>(),
                            IsMobile = model.IsMobile,
                        };
                        _moduleService.Create(entity);
                        _Ilog.InfoFormat("Thêm mới module {0}", model.Name);
                    }
                    else
                    {
                        if (_moduleService.CheckExistCode(model.Code, model.Id))
                        {
                            throw new Exception(string.Format("Mã {0} đã tồn tại", model.Code));
                        }
                        Module entity = _moduleService.GetById(model.Id);
                        entity.Code = model.Code;
                        entity.ClassCss = model.ClassCss;
                        entity.StyleCss = model.StyleCss;
                        if (file != null)
                        {
                            entity.Icon = model.Icon;
                        }

                        entity.Name = model.Name;
                        entity.IsShow = model.IsShow;
                        entity.Order = model.Order.ToNumber<int>();
                        entity.AllowFilterScope = model.AllowFilterScope.GetValueOrDefault();
                        entity.IsMobile = model.IsMobile;
                        _moduleService.Update(entity);

                        _Ilog.InfoFormat("Cập nhật module {0}", model.Name);
                    }

                    return Json(result);
                }
                result.Message = ModelState.GetErrors();
                result.Status = false;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
                _Ilog.Error("Lỗi cập nhật thông tin Module", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionDelete)]
        public JsonResult Delete(int id)
        {
            JsonResultBO result = new JsonResultBO(true);
            try
            {
                Module entity = _moduleService.GetById(id);
                if (entity != null)
                {
                    _moduleService.Delete(entity);
                    result.Message = "Xóa module thành công";
                }
                else
                {
                    result.Status = false;
                    result.Message = "Module không tồn tại";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Xóa module không thành công";
                _Ilog.Error("Xóa module không thành công", ex);
            }
            return Json(result);
        }
    }
}