using Microsoft.Office.Interop.Excel;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            
            
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult InternalServer()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }

        public PartialViewResult TimeOutSession()
        {
            return PartialView();
        }

        public ActionResult EndUserdetention()
        {
            return View();
        }
    }
}