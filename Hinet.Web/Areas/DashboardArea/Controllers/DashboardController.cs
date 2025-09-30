using AutoMapper;
using Hinet.Service.AppUserService;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.NotificationService;
using Hinet.Web.Areas.UserArea.Models;
using Hinet.Web.Filters;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace Hinet.Web.Areas.DashboardArea.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly ILog _log;
		public const string permissionIndexSoTTTT = "Dashboard_indexSoTTTT";
		public const string permissionIndexDN = "Dashboard_indexDN";
		private readonly INotificationService _notificationService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IDM_DulieuDanhmucService _IDM_DulieuDanhmucService;
		private readonly IAppUserService _appUserService;
		private string searchGiamSat = "searchGiamSat";

		//IConnectionMultiplexer _connectionMultiplexer;
		//IDatabase _cacheDatabase;
		public DashboardController(
			IMapper mapper,
			ILog log,
			IDM_DulieuDanhmucService dM_DulieuDanhmucService,
			INotificationService notificationService,
			IDM_DulieuDanhmucService IDM_DulieuDanhmucService,
			IAppUserService appUserService)
		{
			_dM_DulieuDanhmucService = dM_DulieuDanhmucService;
			_log = log;
			_mapper = mapper;
			_notificationService = notificationService;
			_IDM_DulieuDanhmucService = IDM_DulieuDanhmucService;
			_appUserService = appUserService;
		}

		// GET: DashboardArea/Dashboard

		public ActionResult Index()
		{
			return View();
		}

		private string getErrorString(IdentityResult identityResult)
		{
			var strMessage = string.Empty;
			foreach (var item in identityResult.Errors)
			{
				strMessage += item;
			}
			return strMessage;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult EditProfile(EditVM model)
		{
			var result = new JsonResultBO(true);
			try
			{
				if (ModelState.IsValid)
				{
					var user = _appUserService.GetById(model.Id);
					if (user == null)
					{
						throw new Exception("Không tìm thấy thông tin");
					}
					else
					{
						if (!string.IsNullOrEmpty(model.Email) && _appUserService.CheckExistEmail(model.Email, user.Id))
						{
							throw new Exception(string.Format("Email {0} đã được sử dụng", model.Email));
						}

						user.FullName = model.FullName;
						user.Email = model.Email;
						user.PhoneNumber = model.PhoneNumber;
						user.BirthDay = model.BirthDay;
						user.Gender = (int)model.Gender;
						user.Address = model.Address;
						_appUserService.Update(user);
					}
				}
			}
			catch (Exception ex)
			{
				result.Status = false;
				result.Message = "Không cập nhật được";
			}
			return Json(result);
		}

		private static string CapitalizeFirstLetter(string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;
			// Capitalize the first letter and leave the rest as is
			return char.ToUpper(input[0]) + input.Substring(1).ToLower();
		}
	}
};