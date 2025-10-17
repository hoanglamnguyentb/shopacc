using AutoMapper;
using CommonHelper;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.DM_NhomDanhmucService;
using Hinet.Service.GianHangService;
using Hinet.Service.GianHangService.Dto;
using Hinet.Service.ThuocTinhGianHangService;
using Hinet.Service.ThuocTinhService;
using Hinet.Web.Areas.GianHangArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Hinet.Web.Areas.GianHangArea.Controllers
{
    public class GianHangController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "GianHang_index";
        public const string permissionCreate = "GianHang_create";
        public const string permissionEdit = "GianHang_edit";
        public const string permissionDelete = "GianHang_delete";
        public const string permissionImport = "GianHang_Inport";
        public const string permissionExport = "GianHang_export";
        public const string searchKey = "GianHangPageSearchModel";
        private readonly IGianHangService _GianHangService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        private readonly IThuocTinhGianHangService _thuocTinhGianHangService; 
        private readonly IDM_NhomDanhmucService _dM_NhomDanhmucService;

        public GianHangController(IGianHangService GianHangService, 
            ILog Ilog,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper, IDM_NhomDanhmucService dM_NhomDanhmucService, 
            IThuocTinhGianHangService thuocTinhGianHangService)
        {
            _GianHangService = GianHangService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _dM_NhomDanhmucService = dM_NhomDanhmucService;
            _thuocTinhGianHangService = thuocTinhGianHangService;
        }
        // GET: GianHangArea/GianHang
        [PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {

            var listData = _GianHangService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as GianHangSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new GianHangSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _GianHangService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }
        public PartialViewResult Create()
        {
            var myModel = new CreateVM();
            return PartialView("_CreatePartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult Create(CreateVM model)
        {
            var result = new JsonResultBO(true, "Tạo  thành công");
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
                    {
                        model.AnhBia = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/GianHang");
                    }
                    var EntityModel = _mapper.Map<GianHang>(model);
                    EntityModel.Slug = SlugHelper.GenerateSlug(model.Name, 50);
                    _GianHangService.Create(EntityModel);
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

            var obj = _GianHangService.GetById(id);
            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }

            myModel = _mapper.Map(obj, myModel);
            return PartialView("_EditPartial", myModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult Edit(EditVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                if (ModelState.IsValid)
                {

                    var obj = _GianHangService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }
                    obj = _mapper.Map(model, obj);
                    if (model.FileAnh != null && model.FileAnh.ContentLength > 0)
                    {
                        FileHelper.DeleteFile(model.AnhBia);
                        obj.AnhBia = FileHelper.SaveUploadedFile(model.FileAnh, "~/Uploads/GianHang");
                    }
                    obj.Slug = SlugHelper.GenerateSlug(model.Name, 50);
                    _GianHangService.Update(obj);

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

        public PartialViewResult ThongTinNap(int id)
        {
            var myModel = new EditVM();
            ViewBag.dropdownListKieuDuLieu = ConstantExtension.GetDropdownData<KieuDuLieuThuocTinhGameConstant>();
            ViewBag.dropdownListNhomDanhMuc = _dM_NhomDanhmucService.GetDropdown("GroupName", "GroupCode");
            myModel.ThuocTinhs = _thuocTinhGianHangService.FindBy(x => x.GianHangId == id).ToList();
            return PartialView("_ThongTinNapPartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ThongTinNap(EditVM model)
        {
            var result = new JsonResultBO(true, "Cập nhật thành công");
            try
            {
                // Xóa và lưu thuộc tính
                _thuocTinhGianHangService.DeleteByGianHangId(model.Id);
                var listThuocTinhAdd = new List<ThuocTinhGianHang>();

                foreach (var tt in model.ThuocTinhs)
                {
                    var nhomDanhMuc = _dM_NhomDanhmucService.FindBy(x => x.GroupCode == tt.NhomDanhmucCode).FirstOrDefault();
                    var thuocTinh = new ThuocTinhGianHang
                    {
                        GianHangId = model.Id,
                        TenThuocTinh = tt.TenThuocTinh,
                        KieuDuLieu = tt.KieuDuLieu,
                        NhomDanhmucCode = nhomDanhMuc?.GroupCode,
                        NhomDanhMucId = nhomDanhMuc?.Id,
                        IsRequired = tt.IsRequired,
                        PlaceHolder = tt.PlaceHolder,
                    };
                    listThuocTinhAdd.Add(thuocTinh);
                }
                _thuocTinhGianHangService.InsertRange(listThuocTinhAdd);

            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
                _Ilog.Error("Lỗi cập nhật ", ex);
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult searchData(GianHangSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as GianHangSearchDto;

            if (searchModel == null)
            {
                searchModel = new GianHangSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.STTFilter = form.STTFilter;
            searchModel.NameFilter = form.NameFilter;
            searchModel.MoTaFilter = form.MoTaFilter;
            searchModel.TrangThaiFilter = form.TrangThaiFilter;
            searchModel.ViTriHienThiFilter = form.ViTriHienThiFilter;
            searchModel.SlugFilter = form.SlugFilter;
            searchModel.AnhBiaFilter = form.AnhBiaFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _GianHangService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _GianHangService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                _GianHangService.Delete(user);
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
            model.objInfo = _GianHangService.GetById(id);
            return View(model);
        }



    }
}