using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GianHangService;
using Hinet.Service.MaGiamGiaService;
using Hinet.Service.MaGiamGiaService.Dto;
using Hinet.Web.Areas.MaGiamGiaArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Hinet.Web.Areas.MaGiamGiaArea.Controllers
{
    public class MaGiamGiaController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "MaGiamGia_index";
        public const string permissionCreate = "MaGiamGia_create";
        public const string permissionEdit = "MaGiamGia_edit";
        public const string permissionDelete = "MaGiamGia_delete";
        public const string permissionImport = "MaGiamGia_Inport";
        public const string permissionExport = "MaGiamGia_export";
        public const string searchKey = "MaGiamGiaPageSearchModel";
        private readonly IMaGiamGiaService _MaGiamGiaService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IGianHangService _gianHangService;


        public MaGiamGiaController(IMaGiamGiaService MaGiamGiaService, ILog Ilog,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper, IGianHangService gianHangService)
        {
            _MaGiamGiaService = MaGiamGiaService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _gianHangService = gianHangService;
        }
        // GET: MaGiamGiaArea/MaGiamGia
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {

            var listData = _MaGiamGiaService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            ViewBag.dropdownListGianHangId = _gianHangService.GetDropdown("Name", "Id");
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as MaGiamGiaSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new MaGiamGiaSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _MaGiamGiaService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }
        public PartialViewResult Create()
        {
            var myModel = new CreateVM();
            ViewBag.dropdownListGianHangId = _gianHangService.GetDropdown("Name", "Id");
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
                    var EntityModel = _mapper.Map<MaGiamGia>(model);
                    EntityModel.GianHangApDung = model.GianHangApDung != null ? string.Join(",", model.GianHangApDung) : "";
                    _MaGiamGiaService.Create(EntityModel);
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

            var obj = _MaGiamGiaService.GetById(id);
            ViewBag.dropdownListGianHangId = _gianHangService.GetDropdown("Name", "Id");

            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }

            myModel = _mapper.Map(obj, myModel);
            if (!string.IsNullOrEmpty(obj.GianHangApDung))
                myModel.GianHangApDung = obj.GianHangApDung.Split(',').ToList();
            else
                myModel.GianHangApDung = new List<string>();
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

                    var obj = _MaGiamGiaService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj = _mapper.Map(model, obj);
                    obj.GianHangApDung = model.GianHangApDung != null ? string.Join(",", model.GianHangApDung) : "";
                    _MaGiamGiaService.Update(obj);

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
        public JsonResult searchData(MaGiamGiaSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as MaGiamGiaSearchDto;

            if (searchModel == null)
            {
                searchModel = new MaGiamGiaSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.SoLuongFilter = form.SoLuongFilter;
            searchModel.TuNgayFilter = form.TuNgayFilter;
            searchModel.DenNgayFilter = form.DenNgayFilter;
            searchModel.ToanHeThongFilter = form.ToanHeThongFilter;
            searchModel.TrangThaiFilter = form.TrangThaiFilter;
            searchModel.ThongTinFilter = form.ThongTinFilter;
            searchModel.GianHangApDungFilter = form.GianHangApDungFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _MaGiamGiaService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _MaGiamGiaService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _MaGiamGiaService.Delete(user);
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
            model.objInfo = _MaGiamGiaService.GetById(id);
            return View(model);
        }



    }
}