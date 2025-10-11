using AutoMapper;
using CommonHelper;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.SiteConfigService;
using Hinet.Service.SiteConfigService.Dto;
using Hinet.Web.Areas.SiteConfigArea.Models;
using Hinet.Web.Filters;
using log4net;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace Hinet.Web.Areas.SiteConfigArea.Controllers
{
    public class SiteConfigController : BaseController
    {
        private readonly ILog _Ilog;
        private readonly IMapper _mapper;
        public const string permissionIndex = "SiteConfig_index";
        public const string permissionCreate = "SiteConfig_create";
        public const string permissionEdit = "SiteConfig_edit";
        public const string permissionDelete = "SiteConfig_delete";
        public const string permissionImport = "SiteConfig_Inport";
        public const string permissionExport = "SiteConfig_export";
        public const string searchKey = "SiteConfigPageSearchModel";
        private readonly ISiteConfigService _SiteConfigService;
        private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
        public SiteConfigController(ISiteConfigService SiteConfigService, ILog Ilog,

        IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper
            )
        {
            _SiteConfigService = SiteConfigService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;

        }
        // GET: SiteConfigArea/SiteConfig
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index()
        {
            var listData = _SiteConfigService.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as SiteConfigSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new SiteConfigSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = _SiteConfigService.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }

        public async Task<PartialViewResult> Create()
        {
            var myModel = new CreateVM();
            var banks = await FetchBanks();
            var bankSelectList = banks.Select(b => new SelectListItem
            {
                Value = b.code,
                Text = b.name
            }).ToList();

            ViewBag.BankList = bankSelectList;
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
                    if (model.FileLogo != null && model.FileLogo.ContentLength > 0)
                    {
                        FileHelper.DeleteFile(model.Logo);
                        model.Logo = FileHelper.SaveUploadedFile(model.FileLogo, "~/Uploads/SiteConfig");
                    }
                    if (model.FileFavicon != null && model.FileFavicon.ContentLength > 0)
                    {
                        FileHelper.DeleteFile(model.Favicon);
                        model.Favicon = FileHelper.SaveUploadedFile(model.FileFavicon, "~/Uploads/SiteConfig");
                    }
                    if (model.FileOgImage != null && model.FileOgImage.ContentLength > 0)
                    {
                        FileHelper.DeleteFile(model.OgImage);
                        model.OgImage = FileHelper.SaveUploadedFile(model.FileOgImage, "~/Uploads/SiteConfig");
                    }
                    var EntityModel = _mapper.Map<SiteConfig>(model);
                    _SiteConfigService.Create(EntityModel);

                }

            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
                _Ilog.Error("Lỗi tạo mới ", ex);
            }
            return Json(result);
        }

        public async Task<PartialViewResult> Edit(int id)
        {
            var myModel = new EditVM();

            var obj = _SiteConfigService.GetById(id);
            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }
            var banks = await FetchBanks();
            var bankSelectList = banks.Select(b => new SelectListItem
            {
                Value = b.code,
                Text = b.name
            }).ToList();

            ViewBag.BankList = bankSelectList;

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

                    var obj = _SiteConfigService.GetById(model.Id);
                    if (obj == null)
                    {
                        throw new Exception("Không tìm thấy thông tin");
                    }

                    obj = _mapper.Map(model, obj);
                    if (model.FileLogo != null && model.FileLogo.ContentLength > 0)
                    {
                        obj.Logo = FileHelper.SaveUploadedFile(model.FileLogo, "~/Uploads/SiteConfig");
                    }
                    if (model.FileFavicon != null && model.FileFavicon.ContentLength > 0)
                    {
                        obj.Favicon = FileHelper.SaveUploadedFile(model.FileFavicon, "~/Uploads/SiteConfig");
                    }
                    if (model.FileOgImage != null && model.FileOgImage.ContentLength > 0)
                    {
                        obj.OgImage = FileHelper.SaveUploadedFile(model.FileOgImage, "~/Uploads/SiteConfig");
                    }
                    _SiteConfigService.Update(obj);

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
        public JsonResult searchData(SiteConfigSearchDto form)
        {
            var searchModel = SessionManager.GetValue(searchKey) as SiteConfigSearchDto;

            if (searchModel == null)
            {
                searchModel = new SiteConfigSearchDto();
                searchModel.pageSize = 20;
            }
            searchModel.DescriptionFilter = form.DescriptionFilter;
            searchModel.KeywordsFilter = form.KeywordsFilter;
            searchModel.OgTitleFilter = form.OgTitleFilter;
            searchModel.OgDescriptionFilter = form.OgDescriptionFilter;
            searchModel.OgImageFilter = form.OgImageFilter;
            searchModel.SiteTitleFilter = form.SiteTitleFilter;
            searchModel.FaviconFilter = form.FaviconFilter;
            searchModel.LogoFilter = form.LogoFilter;

            SessionManager.SetValue((searchKey), searchModel);

            var data = _SiteConfigService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa  thành công");
            try
            {
                var user = _SiteConfigService.GetById(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                FileHelper.DeleteFile(user.Logo);
                FileHelper.DeleteFile(user.Favicon);
                FileHelper.DeleteFile(user.OgImage);
                _SiteConfigService.Delete(user);
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
            model.objInfo = _SiteConfigService.GetById(id);
            return View(model);
        }

        private async Task<List<Bank>> FetchBanks()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://api.vietqr.io/v2/banks");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var apiResult = JsonConvert.DeserializeObject<ApiResponse>(json);
                    return apiResult.data ?? new List<Bank>();
                }
                return new List<Bank>();
            }
        }
    }
    public class Bank
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class ApiResponse
    {
        public string code { get; set; }
        public string desc { get; set; }
        public List<Bank> data { get; set; }
    }

}