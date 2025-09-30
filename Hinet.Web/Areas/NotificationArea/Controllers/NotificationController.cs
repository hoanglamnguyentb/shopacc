using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.NotificationService;
using Hinet.Service.NotificationService.Dto;
using Hinet.Web.Areas.NotificationArea.Models;
using Hinet.Web.Core;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.NotificationArea.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "Notification_index";
        public const string permissionCreate = "Notification_create";
        public const string permissionEdit = "Notification_edit";
        public const string permissionDelete = "Notification_delete";
        public const string permissionImport = "Notification_Inport";
        public const string permissionExport = "Notification_export";
        public const string searchKey = "NotificationPageSearchModel";
        private readonly INotificationService _NotificationService;
        private readonly IAppUserService serviceAppUser;
        private readonly IAppUserService _appUserService;

        public NotificationController(INotificationService NotificationService, ILog Ilog,
            IAppUserService serviceAppUser,
            IAppUserService appUserService,
        IMapper mapper
            )
        {
            _NotificationService = NotificationService;
            _Ilog = Ilog;
            _mapper = mapper;

            this.serviceAppUser = serviceAppUser;
            this._appUserService = appUserService;
        }

        // GET: NotificationArea/Notification
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            var listData = _NotificationService.GetDaTaByPage(CurrentUserId, null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as NotificationSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new NotificationSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _NotificationService.GetDaTaByPage(CurrentUserId, searchModel, indexPage, pageSize);
            return Json(data);
        }

        public PartialViewResult Create()
        {
            var myModel = new CreateVM();
            myModel.FromUser = CurrentUserId;
            ViewBag.dropdownListToUser = _appUserService.GetDropdown("FullName", "Id");
            return PartialView("_CreatePartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true, "Tạo Thông báo hệ thống thành công");
            try
            {
                if (ModelState.IsValid)
                {
                    model.IsRead = false;
                    var EntityModel = _mapper.Map<Notification>(model);
                    _NotificationService.Create(EntityModel);
                    NotificationProvider.SendMessage(EntityModel);
                }
            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
                _Ilog.Error("Lỗi tạo mới Thông báo hệ thống", ex);
            }
            return Json(result);
        }

        //[PermissionAccess(Code = permissionEdit)]
        public PartialViewResult Edit(long id)
        {
            var myModel = new EditVM();
            var obj = _NotificationService.GetById(id);
            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }
            myModel = _mapper.Map(obj, myModel);

            ViewBag.dropdownListToUser = _appUserService.GetDropdown("FullName", "Id");
            return PartialView("_EditPartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditVM model)
        {
            var result = new JsonResultBO(true, "Tạo Thông báo hệ thống thành công");
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = _NotificationService.GetById(model.Id);
                    obj = _mapper.Map(model, obj);
                    model.IsRead = false;

                    _NotificationService.Update(obj);
                    NotificationProvider.SendMessage(obj);
                }
            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
                _Ilog.Error("Lỗi tạo mới Thông báo hệ thống", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult searchData(NotificationSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as NotificationSearchDto;

            if (searchModel == null)
            {
                searchModel = new NotificationSearchDto();
                searchModel.pageSize = 10;
            }
            searchModel.IsReadFilter = form.IsReadFilter;
            searchModel.FromUserFilter = form.FromUserFilter;
            searchModel.ToUserFilter = form.ToUserFilter;
            searchModel.MessageFilter = form.MessageFilter;
            searchModel.TypeFilter = form.TypeFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _NotificationService.GetDaTaByPage(CurrentUserId, searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultBO(true, "Xóa Thông báo hệ thống thành công");
            try
            {
                var user = _NotificationService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _NotificationService.Delete(user);
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
            model.objInfo = _NotificationService.GetById(id);
            return View(model);
        }

        public PartialViewResult ShowNotification()
        {
            var listNotification = _NotificationService.GetByUserId(CurrentUserId, 10);
            return PartialView(listNotification);
        }

        public ActionResult ReadNotification(long id)
        {
            var Noti = _NotificationService.GetById(id);
            Noti.IsRead = true;
            _NotificationService.Update(Noti);
            return Redirect(Noti.Link);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IndexUser(string id)
        {
            var entityUser = serviceAppUser.FindBy(x => x.UserName == id)
                .FirstOrDefault() ?? new AppUser();

            var listData = _NotificationService.GetDaTaByPage(entityUser.Id, null);
            SessionManager.SetValue("NotificationDeepLink", null);
            ViewBag.UserId = entityUser.Id;
            return View(listData);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetDataUser(long userId, int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue("NotificationDeepLink") as NotificationSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new NotificationSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _NotificationService.GetDaTaByPage(userId, searchModel, indexPage, pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult XemTatCa()
        {
            JsonResultBO result = new JsonResultBO(true);

            var ListIdNoti = _NotificationService.GetListIdNoti(CurrentUserId);

            if (ListIdNoti != null)
            {
                foreach (var item in ListIdNoti)
                {
                    item.IsRead = true;
                    _NotificationService.Update(item);
                }
            }

            return Json(result);
        }
    }
}