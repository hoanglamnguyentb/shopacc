using Hinet.Service.Constant;
using Hinet.Service.TinTucService;
using Hinet.Service.TinTucService.Dto;
using Hinet.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    [RoutePrefix("tin-tuc")]
    public class TinTucController : EndUserController
    {
        private readonly ITinTucService _tinTucService;

        public TinTucController(ITinTucService tinTucService)
        {
            _tinTucService = tinTucService;
        }

        // GET: TinTuc
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index(string query = null, int pageIndex = 1)
        {
            var searchModel = new TinTucSearchDto
            {
                TrangThaiFilter = TrangThaiTinTucConstant.XUATBAN,
                TieuDeFilter = query,
                //NoiDungFilter = query,
            };
            var listTinTuc = _tinTucService.GetDaTaByPage(searchModel, pageIndex, 5);
            ViewBag.CurrentQuery = query;
            ViewBag.MenuBottom = "tin-tuc";
            return View(listTinTuc);
        }

        [HttpGet]
        [Route("load-more")]
        [AllowAnonymous]
        public ActionResult LoadMore(string query = null, int pageIndex = 1)
        {
            var searchModel = new TinTucSearchDto
            {
                TrangThaiFilter = TrangThaiTinTucConstant.XUATBAN,
                TieuDeFilter = query,
                //NoiDungFilter = query,
            };

            var listTinTuc = _tinTucService.GetDaTaByPage(searchModel, pageIndex, 5);
            return PartialView("_TinTucItemPartial", listTinTuc.ListItem);
        }

        [Route("{slug}")]
        [AllowAnonymous]
        public ActionResult ChiTiet(string slug)
        {
            var tinTuc = _tinTucService.GetBySlug(slug);
            ViewBag.MenuBottom = "tin-tuc";
            return View(tinTuc);
        }

        #region Partial

        [AllowAnonymous]
        public PartialViewResult TinTucLienQuan(int id)
        {
            var listTT = _tinTucService.GetListTinTucLienQuan(id);
            return PartialView("_TinTucLienQuanPartial", listTT);
        }

        [AllowAnonymous]
        public PartialViewResult DMGameLienQuan(int id, string gameSlug = null)
        {
            var listDM = _tinTucService.GetListDMGameLienQuan(id);
            ViewBag.GameSlug = gameSlug;
            return PartialView("_DMGameLienQuanPartial", listDM);
        }

        [AllowAnonymous]
        public PartialViewResult DichVuLienQuan(int id)
        {
            var listDV = _tinTucService.GetListDichVuLienQuan(id);
            return PartialView("_DichVuLienQuanPartial", listDV);
        }

        #endregion Partial
    }
}