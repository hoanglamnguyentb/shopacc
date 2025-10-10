using CommonHelper.String;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.RoleOperationService;
using Hinet.Service.RoleService;
using Hinet.Service.RoleService.DTO;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Hinet.Web.Areas.RoleArea.Models.RoleViewModel;

namespace Hinet.Web.Areas.RoleArea.Controllers
{
    /// <summary>
    /// @author:duynn
    /// @since: 22/04/2019
    /// </summary>
    public class RoleController : BaseController
    {
        private IRoleService _roleService;
        private IRoleOperationService _roleOperationService;
        private ILog _iLog;
        public const string permissionIndex = "Role_index";
        public const string permissionCreate = "Role_create";
        public const string permissionEdit = "Role_edit";
        public const string permissionDelete = "Role_delete";
        public const string permissionDetail = "Role_detail";
        public const string permissionImport = "Role_Inport";
        public const string permissionExport = "Role_export";

        public RoleController(IRoleService roleService,
            IRoleOperationService roleOperationService,
        ILog Ilog)
        {
            _roleService = roleService;
            _roleOperationService = roleOperationService;
            _iLog = Ilog;
        }

        // GET: RoleArea/Role
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            var searchModel = new RoleSearchDTO();
            SessionManager.SetValue("RoleSearch", new RoleSearchDTO());
            RoleIndexViewModel viewModel = new RoleIndexViewModel()
            {
                GroupData = _roleService.GetDataByPage(searchModel, 1, 20)
            };
            return View(viewModel);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult GetData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue("RoleSearch") as RoleSearchDTO;
            if (searchModel == null)
            {
                searchModel = new RoleSearchDTO();
            }
            if (!string.IsNullOrEmpty(sortQuery))
            {
                searchModel.sortQuery = sortQuery;
            }
            if (pageSize > 0)
            {
                searchModel.pageSize = pageSize;
            }
            SessionManager.SetValue("RoleSearch", searchModel);
            var data = _roleService.GetDataByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        [PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id = 0)
        {
            var viewModel = new RoleEditViewModel();
            var editEntity = _roleService.GetById(id) ?? new Role();
            viewModel = new RoleEditViewModel()
            {
                Id = editEntity.Id,
                Name = editEntity.Name,
                Code = editEntity.Code
            };
            return PartialView("_EditPartial", viewModel);
        }

        [PermissionAccess(Code = permissionDetail)]
        public PartialViewResult Detail(long id = 0)
        {
            var viewModel = new RoleEditViewModel();
            var editEntity = _roleService.GetById(id) ?? new Role();
            viewModel = new RoleEditViewModel()
            {
                Id = editEntity.Id,
                Name = editEntity.Name,
                Code = editEntity.Code
            };
            return PartialView("_DetailPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult SearchData(FormCollection form)
        {
            var searchModel = SessionManager.GetValue("RoleSearch") as RoleSearchDTO;

            if (searchModel == null)
            {
                searchModel = new RoleSearchDTO();
                searchModel.pageSize = 10;
            }

            searchModel.QueryName = form["QUERY_NAME"];
            searchModel.QueryCode = form["QUERY_CODE"];
            SessionManager.SetValue("RoleSearch", searchModel);
            var data = _roleService.GetDataByPage(searchModel, 1);
            return Json(data);
        }

        [HttpPost]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult Save(RoleEditViewModel model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id <= 0)
                    {
                        Role entity = new Role()
                        {
                            Name = model.Name,
                            Code = model.Code,
                        };
                        _roleService.Create(entity);
                        _iLog.InfoFormat("Thêm mới vai trò {0}", model.Name);
                    }
                    else
                    {
                        Role entity = _roleService.GetById(model.Id);
                        entity.Name = model.Name;
                        entity.Code = model.Code;
                        _roleService.Update(entity);

                        _iLog.InfoFormat("Cập nhật vai trò {0}", model.Name);
                    }
                    return Json(result);
                }
                result.Status = false;
                result.Message = ModelState.GetErrors();
                return Json(result);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không cập nhật được";
                _iLog.Error("Lỗi cập nhật thông tin vai trò", ex);
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
                Role entity = _roleService.GetById(id);
                if (entity != null)
                {
                    _roleService.Delete(entity);
                    result.Message = "Xóa vai trò thành công";
                }
                else
                {
                    result.Status = false;
                    result.Message = "Vai trò không tồn tại";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Xóa vai trò không thành công";
                _iLog.Error("Xóa vai trò không thành công", ex);
            }
            return Json(result);
        }

        [PermissionAccess(Code = permissionIndex)]
        public ActionResult ConfigureOperation(int roleId)
        {
            RoleOperationConfigViewModel viewModel = new RoleOperationConfigViewModel()
            {
                ConfigureData = _roleOperationService.GetConfigureOperation(roleId)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAccess(Code = permissionIndex)]
        public JsonResult SaveConfigureOperation(FormCollection form)
        {
            JsonResultBO result = new JsonResultBO(true);
            int roleId = int.Parse(form["ROLE_ID"]);
            try
            {
                List<RoleOperation> obsoluteData = _roleOperationService.FindBy(x => x.RoleId == roleId).ToList();
                _roleOperationService.DeleteRange(obsoluteData);
                var operationIds = form["OPERATION"].ToListNumber<long>().ToList();
                List<RoleOperation> configData = new List<RoleOperation>();
                foreach (var operationId in operationIds)
                {
                    RoleOperation config = new RoleOperation()
                    {
                        OperationId = operationId,
                        RoleId = roleId,
                        IsAccess = 1,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    _roleOperationService.Create(config);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Cập nhật quyền không thành công";
                _iLog.Error($"Cập nhật quyền cho vai trò Id = {roleId} không thành công", ex);
            }
            return Json(result);
        }
    }
}