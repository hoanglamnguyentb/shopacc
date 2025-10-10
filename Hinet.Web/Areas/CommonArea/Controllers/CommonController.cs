using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Web.Areas.CommonArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.CommonArea.Controllers
{
    public class CommonController : BaseController
    {
        // GET: CommonArea/Common
        private ILog _ILog;

        private const string SessionSearchString = "CommonSearch";

        public CommonController(ILog ILog)
        {
            _ILog = ILog;
        }
    }
}