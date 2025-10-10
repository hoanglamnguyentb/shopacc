using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.OperationService;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hinet.Web.Areas.UserArea.Controllers
{
	public class UserAPIController : BaseController
	{
		private readonly IAppUserService _appUserService;
		private readonly IOperationService _operationService;
		private readonly ILog _Ilog;

		// GET: UserArea/UserAPI
		public UserAPIController(IAppUserService appUserService,
			ILog Ilog,
			IOperationService operationService
			)
		{
			_appUserService = appUserService;
			_Ilog = Ilog;
			_operationService = operationService;
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}