using AutoMapper;
using Hangfire.Logging;
using Hinet.Web.Filters;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    public class MapAllController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILog _Ilog;

        public MapAllController(
            ILog Ilog,
            IMapper mapper)
        {
            _Ilog = Ilog;
            _mapper = mapper;
        }

        // GET: MapAll
        public ActionResult Index()
        {
            return View();
        }
    }
}