using Hinet.Service.Constant;
using Hinet.Web.Areas.QLCanhBao.Data;
using System.Web.Mvc;

namespace Hinet.Web.Areas.QLCanhBao.Controllers
{
    public class CanhBaoController : Controller
    {
        private readonly IQlAntensService _qlAntensService;
        private readonly ITramBTSService _tramBTSService;

        public CanhBaoController(IQlAntensService qlAntensService, ITramBTSService tramBTSService)
        {
            _qlAntensService = qlAntensService;
            _tramBTSService = tramBTSService;
        }

        // GET: QLCanhBao/CanhBao
        public ActionResult Index()
        {
            var saTisFyModel = new saTisFyModel();
            saTisFyModel.canhbaoTramBts = _tramBTSService.getTramBtsHetHanKd();
            saTisFyModel.canhbaoAnten = _qlAntensService.getCanhBao();
            return View(saTisFyModel);
        }

        public ActionResult InforTram()
        {
            var modelTram = new ModelTramBts();
            modelTram.keyCanhBao = _tramBTSService.getTramBtsHetHanKdDetaiLV2();
            modelTram.CONHANLST = _tramBTSService.getPageDetailCanhBao(CanhBaoConstant.CONHAN, 1, 10);
            modelTram.HETHANLST = _tramBTSService.getPageDetailCanhBao(CanhBaoConstant.HETHAN, 1, 10);
            modelTram.SAPHETHANLST = _tramBTSService.getPageDetailCanhBao(CanhBaoConstant.SAPHETHAN, 1, 10);
            return PartialView("InforTram", modelTram);
        }

        public ActionResult InforAntens()
        {
            var modelAnten = new ModelAntens();
            modelAnten.keyCanhBao = _qlAntensService.getCanhBaoDetailV2();
            modelAnten.CONHANLST = _qlAntensService.getPageDetailCanhBao(CanhBaoConstant.CONHAN, 1, 10);
            modelAnten.HETHANLST = _qlAntensService.getPageDetailCanhBao(CanhBaoConstant.HETHAN, 1, 10);
            modelAnten.SAPHETHANLST = _qlAntensService.getPageDetailCanhBao(CanhBaoConstant.SAPHETHAN, 1, 10);
            return PartialView("InforAntens", modelAnten);
        }

        [HttpPost]
        public JsonResult getAntensByPage(string typePage = "", int pageIndex = 1, int pageSize = 10)
        {
            var data = _qlAntensService.getPageDetailCanhBao(typePage, pageIndex, pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult getTrambtsByPage(string typePage = "", int pageIndex = 1, int pageSize = 10)
        {
            var data = _qlAntensService.getPageDetailCanhBao(typePage, pageIndex, pageSize);
            return Json(data);
        }

        public ActionResult AntensConHan(string strSearch = "")
        {
            var searchModel = new QlAntensSearchDto();
            searchModel.MaCotFilter = strSearch;
            searchModel.TenCotFilter = strSearch;
            var listdata = _qlAntensService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.CONHAN, 1, 10);
            return PartialView("_AntensConHan", listdata);
        }

        public ActionResult getDataAntensConHan(string strSearch = "")
        {
            var searchModel = new QlAntensSearchDto();
            searchModel.MaCotFilter = strSearch;
            searchModel.TenCotFilter = strSearch;
            var listdata = _qlAntensService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.CONHAN, 1, 10);
            return Json(listdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AntensHetHan(string strSearch = "")
        {
            var searchModel = new QlAntensSearchDto();
            searchModel.MaCotFilter = strSearch;
            searchModel.TenCotFilter = strSearch;
            var listdata = _qlAntensService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.HETHAN, 1, 10);
            return PartialView("_AntensHetHan", listdata);
        }

        public ActionResult getDataAntensHetHan(string strSearch = "")
        {
            var searchModel = new QlAntensSearchDto();
            searchModel.MaCotFilter = strSearch;
            searchModel.TenCotFilter = strSearch;
            var listdata = _qlAntensService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.HETHAN, 1, 10);
            return Json(listdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AntensSapHetHan(string strSearch = "")
        {
            var searchModel = new QlAntensSearchDto();
            searchModel.MaCotFilter = strSearch;
            searchModel.TenCotFilter = strSearch;
            var listdata = _qlAntensService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.SAPHETHAN, 1, 10);
            return PartialView("_AntensSapHetHan", listdata);
        }

        public ActionResult getDataAntensSapHetHan(string strSearch = "")
        {
            var searchModel = new QlAntensSearchDto();
            searchModel.MaCotFilter = strSearch;
            searchModel.TenCotFilter = strSearch;
            var listdata = _qlAntensService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.SAPHETHAN, 1, 10);
            return Json(listdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TramBTSConHan(string strSearch = "")
        {
            var searchModel = new TramBTSSearchDto();
            searchModel.MaTram_TenTramFilter = strSearch;
            var listdata = _tramBTSService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.CONHAN, 1, 10);
            return PartialView("_TramBTSConHan", listdata);
        }

        public ActionResult getDataTramBTSConHan(string strSearch = "")
        {
            var searchModel = new TramBTSSearchDto();
            searchModel.MaTram_TenTramFilter = strSearch;
            var listdata = _tramBTSService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.CONHAN, 1, 10);
            return Json(listdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TramBTSHetHan(string strSearch = "")
        {
            var searchModel = new TramBTSSearchDto();
            searchModel.MaTram_TenTramFilter = strSearch;
            var listdata = _tramBTSService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.HETHAN, 1, 10);
            return PartialView("_TramBTSHetHan", listdata);
        }

        public ActionResult getDataTramBTSHetHan(string strSearch = "")
        {
            var searchModel = new TramBTSSearchDto();
            searchModel.MaTram_TenTramFilter = strSearch;
            var listdata = _tramBTSService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.HETHAN, 1, 10);
            return Json(listdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TramBTSSapHetHan(string strSearch = "")
        {
            var searchModel = new TramBTSSearchDto();
            searchModel.MaTram_TenTramFilter = strSearch;
            var listdata = _tramBTSService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.SAPHETHAN, 1, 10);
            return PartialView("_TramBTSSapHetHan", listdata);
        }

        [HttpGet]
        public JsonResult getDataTramBTSSapHetHan(string strSearch = "")
        {
            var searchModel = new TramBTSSearchDto();
            searchModel.MaTram_TenTramFilter = strSearch;
            var listdata = _tramBTSService.getPageDetailThongKeCanhBao(searchModel, CanhBaoConstant.SAPHETHAN, 1, 10);
            return Json(listdata, JsonRequestBehavior.AllowGet);
        }
    }
}