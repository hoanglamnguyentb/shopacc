using AutoMapper;
using CommonHelper;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Web.Areas.DanhMucGameArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Hinet.Web.Areas.DanhMucGameArea.Controllers
{
    public class DanhMucGameController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "DanhMucGame_index";
        public const string permissionCreate = "DanhMucGame_create";
        public const string permissionEdit = "DanhMucGame_edit";
        public const string permissionDelete = "DanhMucGame_delete";
        public const string permissionImport = "DanhMucGame_Inport";
        public const string permissionExport = "DanhMucGame_export";
        public const string searchKey = "DanhMucGamePageSearchModel";
        private readonly IDanhMucGameService _DanhMucGameService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IGameService _gameService;

        public DanhMucGameController(IDanhMucGameService DanhMucGameService, ILog Ilog,
        IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper, IGameService gameService)
        {
            _DanhMucGameService = DanhMucGameService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _gameService = gameService;
        }
        // GET: DanhMucGameArea/DanhMucGame
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index(int? id = null)//Id của game
        {
            var searchModel = new DanhMucGameSearchDto
            {
                GameIdFilter = id
            };
            ViewBag.Game = _gameService.GetById(id) as Game ?? null;
            ViewBag.dropdownListGameId = _gameService.GetDropdown("Name", "Id");
            SessionManager.SetValue(searchKey, searchModel);
            var listData = _DanhMucGameService.GetDaTaByPage(searchModel);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as DanhMucGameSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new DanhMucGameSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _DanhMucGameService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }
        public PartialViewResult Create(int? id = null)
        {
            var myModel = new CreateVM()
            {
                GameId = id,
            };
            ViewBag.dropdownListGameId = _gameService.GetDropdown("Name", "Id");
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
                        model.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/DanhMucGame");
                    }
                    var EntityModel = _mapper.Map<DanhMucGame>(model);
                    _DanhMucGameService.Create(EntityModel);

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

            var obj = _DanhMucGameService.GetById(id);
            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }

            ViewBag.dropdownListGameId = _gameService.GetDropdown("Name", "Id");
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

                    var obj = _DanhMucGameService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj = _mapper.Map(model, obj);
                    if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
                    {
                        FileHelper.DeleteFile(model.DuongDanAnh);
                        obj.DuongDanAnh = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/DanhMucGame");
                    }
                    _DanhMucGameService.Update(obj);
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
        public JsonResult searchData(DanhMucGameSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as DanhMucGameSearchDto;

            if (searchModel == null)
            {
                searchModel = new DanhMucGameSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.GameIdFilter = form.GameIdFilter;
            searchModel.NameFilter = form.NameFilter;
            searchModel.DuongDanAnhFilter = form.DuongDanAnhFilter;
            searchModel.MoTaFilter = form.MoTaFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _DanhMucGameService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _DanhMucGameService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _DanhMucGameService.Delete(user);
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
            model.objInfo = _DanhMucGameService.GetById(id);
            return View(model);
        }


        [HttpGet]
        public JsonResult GetDanhMucByGame(int gameId)
        {
            var list = _DanhMucGameService.GetDanhMucByGame(gameId)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}