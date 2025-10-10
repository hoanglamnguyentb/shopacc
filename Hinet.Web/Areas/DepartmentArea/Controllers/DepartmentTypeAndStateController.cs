using Hinet.Service.DepartmentService;
using Hinet.Service.DepartmentService.DTO;
using Hinet.Web.Filters;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DepartmentArea.Controllers
{
    public class DepartmentTypeAndStateController : BaseController
    {
        private IDepartmentService _departmentService;

        public DepartmentTypeAndStateController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: DepartmentArea/DepartmentTypeAndState
        public ActionResult Index(string type, string state)
        {
            var data = _departmentService.GetDaTaByPage(type, state, null);
            ViewBag.Datastate = state;
            ViewBag.Datatype = type;
            return View(data);
        }

        [HttpPost]
        public JsonResult GetData(string state, string type, int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue("SearchModelDepartmentStateType") as DepartmentSearchDTO;
            if (searchModel == null)
            {
                searchModel = new DepartmentSearchDTO();
            }
            if (!string.IsNullOrEmpty(sortQuery))
            {
                searchModel.sortQuery = sortQuery;
            }
            if (pageSize > 0)
            {
                searchModel.pageSize = pageSize;
            }
            SessionManager.SetValue("SearchModelDepartmentStateType", searchModel);
            var data = _departmentService.GetDaTaByPage(type, state, searchModel, indexPage, pageSize);
            return Json(data);
        }
    }
}