using AutoMapper;
using CommonHelper;
using CommonHelper.Excel;
using CommonHelper.ObjectExtention;
using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Service.AppUserService;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.RoleService;
using Hinet.Service.UserOperationService;
using Hinet.Service.UserRoleService;
using Hinet.Web.Areas.UserArea.Models;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using Web.Common;

namespace Hinet.Web.Areas.UserArea.Controllers
{
	public class UserController : BaseController
	{
		private readonly IAppUserService _appUserService;
		private readonly IRoleService _roleService;
		private readonly IUserRoleService _userRoleService;
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "QLTaiKhoan";
		public const string permissionRemoveChecked = "QLTaiKhoan_RemoveChecked";
		public const string permissionDSQuyen = "QLTaiKhoan_DSQuyen";

		public const string permissionCreate = "QLTaiKhoan_create";
		public const string permissionEdit = "QLTaiKhoan_edit";
		public const string permissionDelete = "QLTaiKhoan_delete";
		public const string permissionDetail = "QLTaiKhoan_detail";
		public const string permissionImport = "QLTaiKhoan_Inport";
		public const string permissionExport = "QLTaiKhoan_export";

		private IDM_DulieuDanhmucService _dm_DulieuDanhmucService;
		private IUserOperationService _userOperationService;

		public UserController(IAppUserService appUserService, ILog Ilog,
			IRoleService roleService,
			IUserRoleService userRoleService,
			IDM_DulieuDanhmucService dm_DulieuDanhmucService,
			IMapper mapper,
			IUserOperationService userOperationService)
		{
			_userOperationService = userOperationService;
			_userRoleService = userRoleService;
			_appUserService = appUserService;
			_Ilog = Ilog;
			_roleService = roleService;
			_mapper = mapper;
			_dm_DulieuDanhmucService = dm_DulieuDanhmucService;
		}

		// GET: UserArea/User
		[PermissionAccess(Code = permissionIndex)]
		public ActionResult Index()
		{
			var userData = _appUserService.GetDaTaByPage(null);
			return View(userData);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionIndex)]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue("UserPageSearchModel") as AppUserSearchDto;
			if (searchModel == null)
			{
				searchModel = new AppUserSearchDto();
			}
			if (!string.IsNullOrEmpty(sortQuery))
			{
				searchModel.sortQuery = sortQuery;
			}
			if (pageSize > 0)
			{
				searchModel.pageSize = pageSize;
			}
			SessionManager.SetValue("UserPageSearchModel", searchModel);
			var data = _appUserService.GetDaTaByPage(searchModel, indexPage, pageSize);
			return Json(data);
		}

		[PermissionAccess(Code = permissionIndex)]
		public ActionResult ConfigureOperation(long id)
		{
			ViewBag.isRemoveChecked = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionRemoveChecked);
			ViewBag.isDSQuyen = CurrentUserInfo.ListOperations.Any(t => t.Code == permissionDSQuyen);
			var viewModel = _userOperationService.GetConfigureOperation(id);
			ViewBag.UserDto = _appUserService.GetById(id);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[PermissionAccess(Code = permissionIndex)]
		public JsonResult SaveConfigureOperation(FormCollection form)
		{
			JsonResultBO result = new JsonResultBO(true);
			long userId = long.Parse(form["USER_ID"]);
			try
			{
				List<UserOperation> obsoluteData = _userOperationService.FindBy(x => x.UserId == userId).ToList();
				_userOperationService.DeleteRange(obsoluteData);
				var operationIds = form["OPERATION"].ToListNumber<long>().ToList();
				List<UserOperation> configData = new List<UserOperation>();
				foreach (var operationId in operationIds)
				{
					UserOperation config = new UserOperation()
					{
						OperationId = operationId,
						UserId = userId,
						IsAccess = 1,
						CreatedDate = DateTime.Now,
						UpdatedDate = DateTime.Now
					};
					_userOperationService.Create(config);
				}
			}
			catch (Exception ex)
			{
				result.Status = false;
				result.Message = "Cập nhật quyền không thành công";
				_Ilog.Error($"Cập nhật quyền cho vai trò Id = {userId} không thành công", ex);
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionCreate)]
		public PartialViewResult Create(long? IdDoiTuong, string TypeAccount)
		{
			var myModel = new CreateVM();
			myModel.IdDoiTuong = IdDoiTuong;
			myModel.TypeAccount = TypeAccount;
			var type = typeof(IAuditableEntity);
			var listJobPosition = _dm_DulieuDanhmucService.GetDropdownlist(DanhMucConstant.ChucVu, "Id");
			ViewBag.listJobPosition = listJobPosition;
			ViewBag.LinhVucDropdownData = _dm_DulieuDanhmucService.GetDropdownlistID(DanhMucConstant.LinhVuc, null);
			ViewBag.LoaiDonViBaoCao = _dm_DulieuDanhmucService.GetDropdownlistCode(DanhMucConstant.LOAIDOANHNGHIEP, null);
			return PartialView("_CreatePartial", myModel);
		}

		[PermissionAccess(Code = permissionIndex)]
		public ActionResult MyProfile()
		{
			var model = new DetailVM();
			model.users = _appUserService.GetDtoById((long)CurrentUserId);
			string job = "Chưa cập nhật";
			ViewBag.job = job;
			return View(model);
		}

		[PermissionAccess(Code = permissionEdit)]
		public PartialViewResult Edit(long id)
		{
			var user = _appUserService.GetById(id);
			if (user == null)
			{
				throw new HttpException(404, "Không tìm thấy thông tin");
			}
			var myModel = new EditVM()
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				BirthDay = user.BirthDay,
				Gender = user.Gender,
				Address = user.Address,
			};

			var listJobPosition = _dm_DulieuDanhmucService.GetDropdownlist(DanhMucConstant.ChucVu, "Id");
			ViewBag.listJobPosition = listJobPosition;
			ViewBag.ListTypeDashBoard = ConstantExtension.GetDropdownData<TypeDashboardConstant>();
			myModel = _mapper.Map(user, myModel);
			return PartialView("_EditPartial", myModel);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionIndex)]
		public JsonResult SendMailInfo(long id)
		{
			var result = new JsonResultBO(true);
			var ThongtinMail = new ThongTinMailVM();
			ThongtinMail.UserData = _appUserService.GetById(id);
			ThongtinMail.UserName = ThongtinMail.UserData.UserName;
			ThongtinMail.LinkHeThong = WebConfigurationManager.AppSettings["LinkHeThong"];
			try
			{
				var template = System.IO.File.ReadAllText(Server.MapPath("/MailTemplate/MailUser.html"));
				var contentmessage = EmailProvider.BindingDataToMailContent<ThongTinMailVM>(ThongtinMail, template);
				var resultMail = EmailProvider.sendEmailSingle(contentmessage, "Thông báo tài khoản truy cập", ThongtinMail.UserData.Email);
				if (resultMail)
				{
					ThongtinMail.UserData.IsSendMail = true;
					_appUserService.Update(ThongtinMail.UserData);
				}
			}
			catch (Exception ex)
			{
				_Ilog.Error(ex.Message, ex);
				result.MessageFail(ex.Message);
			}
			return Json(result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[PermissionAccess(Code = permissionEdit)]
		public JsonResult Edit(EditVM model, List<long> lstLinhVuc)
		{
			ViewBag.ListTypeDashBoard = ConstantExtension.GetDropdownData<TypeDashboardConstant>();

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
						if (model.BirthDay >= DateTime.Today)
						{
							throw new Exception(string.Format("Ngày sinh không được lớn hơn hoặc bằng ngày hiện tại"));
						}
						user = _mapper.Map(model, user);
						_appUserService.Update(user);
					}
				}
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
				_Ilog.Error("Lỗi cập nhật thông tin User", ex);
				/*if (ex is DbEntityValidationException)
                {
                    var err = (DbEntityValidationException)ex;
                    foreach (var eve in err.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }*/
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionEdit)]
		public PartialViewResult EditProfile(long id)
		{
			var user = _appUserService.GetById(id);
			if (user == null)
			{
				throw new HttpException(404, "Không tìm thấy thông tin");
			}
			var myModel = new EditVM()
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				BirthDay = user.BirthDay,
				Gender = user.Gender,
				Address = user.Address
			};
			return PartialView("_EditProfilePartial", myModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[PermissionAccess(Code = permissionEdit)]
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
				_Ilog.Error("Lỗi cập nhật thông tin User", ex);
			}
			return Json(result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[PermissionAccess(Code = permissionCreate)]
		public JsonResult Create(CreateVM model)
		{
			//ViewBag.ListTinh = _TINHService.GetDropdown("TenTinh", "MaTinh", string.Empty);
			var listJobPosition = _dm_DulieuDanhmucService.GetDropdownlist(DanhMucConstant.ChucVu, "Id");
			ViewBag.listJobPosition = listJobPosition;
			var result = new JsonResultBO(true, "Tạo tài khoản thành công");
			try
			{
				if (ModelState.IsValid)
				{
					if (_appUserService.CheckExistUserName(model.UserName))
					{
						throw new Exception(string.Format("Tài khoản {0} đã tồn tại", model.UserName));
					}

					if (!string.IsNullOrEmpty(model.Email) && _appUserService.CheckExistEmail(model.Email))
					{
						throw new Exception(string.Format("Email {0} đã được sửa dụng", model.Email));
					}

					if (model.BirthDay >= DateTime.Today)
					{
						throw new Exception(string.Format("Ngày sinh không được lớn hơn hoặc bằng ngày hiện tại"));
					}
					var user = new AppUser();
					user.UserName = model.UserName;
					user.FullName = model.FullName;
					user.PhoneNumber = model.PhoneNumber;
					user.BirthDay = model.BirthDay;
					user.Address = model.Address;
					user.Gender = model.Gender;
					user.Email = model.Email;
					user.Avatar = "images/avatars/profile-pic.jpg";
					user.TypeAccount = model.TypeAccount;

					var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
					//Kiểm tra thông tin tài khoản
					var resultUser = UserManager.CreateAsync(user, UserConst.DefaultPassword).Result;
					if (!resultUser.Succeeded)
					{
						throw new Exception(getErrorString(resultUser));
					}

					//Tạo vai trò mặc định của Khoa phòng

					var roleInfo = _roleService.GetIdByCode(RoleConstant.KHACH);
					var userRole = new UserRole();
					userRole.UserId = user.Id;
					userRole.RoleId = (int)roleInfo;
					_userRoleService.Create(userRole);
				}
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
				_Ilog.Error("Lỗi tạo mới tài khoản", ex);
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionIndex)]
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
		[PermissionAccess(Code = permissionIndex)]
		public JsonResult searchData(AppUserSearchDto form)
		{
			var searchModel = SessionManager.GetValue("UserPageSearchModel") as AppUserSearchDto;

			if (searchModel == null)
			{
				searchModel = new AppUserSearchDto();
				searchModel.pageSize = 10;
			}

			searchModel.UserNameFilter = form.UserNameFilter;
			searchModel.AddressFilter = form.AddressFilter;
			searchModel.EmailFilter = form.EmailFilter;
			searchModel.FullNameFilter = form.FullNameFilter;
			SessionManager.SetValue("UserPageSearchModel", searchModel);

			var data = _appUserService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionDelete)]
		public JsonResult Delete(long id)
		{
			var result = new JsonResultBO(true, "Xóa tài khoản thành công");
			try
			{
				var user = _appUserService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				else _appUserService.Delete(user);
			}
			catch (Exception ex)
			{
				result.MessageFail("Không thực hiện được");
				_Ilog.Error("Lỗi khi xóa tài khoản id=" + id, ex);
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionIndex)]
		public ActionResult SetupRole(long id)
		{
			var user = _appUserService.GetById(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			ViewBag.UserInfo = user;
			ViewBag.ListRole = _roleService.GetAll().ToList();
			ViewBag.UserListRole = _userRoleService.GetRoleOfUser(user.Id);
			return PartialView("_SetupRolePartial");
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		[PermissionAccess(Code = permissionIndex)]
		public ActionResult SaveSetupRole(List<int> ListRoleUser, long userId)
		{
			var result = new JsonResultBO(true, "Thiết lập vai trò thành công");
			var userData = _appUserService.GetById(userId);
			if (userData == null)
			{
				return HttpNotFound();
			}
			var isSuccess = _userRoleService.SaveRole(ListRoleUser, userId);
			if (!isSuccess)
			{
				result.MessageFail("Lỗi khi thiết lập vai trò người dùng");
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionDetail)]
		public ActionResult Detail(int id)
		{
			var model = new DetailVM();
			model.users = _appUserService.GetDtoById(id);
			return View(model);
		}

		[PermissionAccess(Code = permissionEdit)]
		public PartialViewResult EditAvatar(long id)
		{
			var model = new DetailVM();
			model.users = _appUserService.GetDtoById(id);
			return PartialView("_EditAvatarPartial", model);
		}

		[PermissionAccess(Code = permissionIndex)]
		public PartialViewResult DsQuyenTruyCap(long id)
		{
			var model = new DetailVM();
			model.users = _appUserService.GetDtoById(id);
			return PartialView(model);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionEdit)]
		public ActionResult EditAvatar(FormCollection collection, HttpPostedFileBase File)
		{
			var id = collection["ID"].ToIntOrZero();
			var myModel = _appUserService.GetById(id);
			var result = new JsonResultBO(true);
			try
			{
				if (File != null && File.ContentLength > 0)
				{
					var resultUpload = UploadProvider.SaveFile(File, null, ".jpg,.png", null, "Uploads/Avatars/", HostingEnvironment.MapPath("/"));

					if (resultUpload.status == true)
					{
						myModel.Avatar = resultUpload.path;
					}
				}
				_appUserService.Update(myModel);
				var currentUser = (UserDto)SessionManager.GetUserInfo();
				currentUser.Avatar = myModel.Avatar;
			}
			catch
			{
				result.Status = false;
				result.Message = "Không cập nhật được";
			}

			return RedirectToAction("MyProfile");
		}

		[HttpPost]
		[PermissionAccess(Code = permissionEdit)]
		public JsonResult ResetPassword(long id)
		{
			var result = new JsonResultBO(true);
			var user = _appUserService.GetById(id);
			if (user == null)
			{
				result.MessageFail("Không tìm thấy người dùng");
				return Json(result);
			}

			_appUserService.Update(user);
			var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			if (string.IsNullOrEmpty(user.SecurityStamp))
			{
				UserManager.UpdateSecurityStamp(user.Id);
			}

			var tokenUpdatePass = UserManager.GeneratePasswordResetToken(user.Id);
			var defaultPass = "12345678";
			var rs = UserManager.ResetPassword(user.Id, tokenUpdatePass, defaultPass);
			if (!rs.Succeeded)
			{
				result.MessageFail(rs.Errors.FirstOrDefault());
				return Json(result);
			}

			return Json(result);
		}

		[HttpGet]
		[PermissionAccess(Code = permissionEdit)]
		public string ResetPasswordEndUser(string id)
		{
			var result = new JsonResultBO(true);
			var user = _appUserService.GetUserByUsername(id);
			if (user == null)
			{
				result.MessageFail("Không tìm thấy người dùng");
				return result.Message;
			}
			_appUserService.Update(user);
			var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			if (string.IsNullOrEmpty(user.SecurityStamp))
			{
				var rsUpdateStamp = UserManager.UpdateSecurityStamp(user.Id);
			}
			var userObj = UserManager.FindById(user.Id);

			var tokenUpdatePass = UserManager.GeneratePasswordResetToken(user.Id);
			var defaultPass = "12345678";
			var rs = UserManager.ResetPassword(user.Id, tokenUpdatePass, defaultPass);
			if (!rs.Succeeded)
			{
				result.MessageFail(rs.Errors.FirstOrDefault());
				return result.Message;
			}

			return "Thành công";
		}

		[PermissionAccess(Code = "EndUserChangeEmail")]
		public PartialViewResult UpdateEmailEnduser(long id)
		{
			var model = new UpdateEnduserVM();
			var userData = _appUserService.GetById(id);
			if (userData != null && userData.TypeAccount == AccountTypeConstant.EndUser)
			{
				ViewBag.userData = userData;
				model.Id = userData.Id;
			}
			return PartialView(model);
		}

		[PermissionAccess(Code = "EndUserChangeEmail")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult UpdateEmailEnduser(UpdateEnduserVM model)
		{
			var result = new JsonResultBO(true, "Cập nhật email thành công");
			var userData = _appUserService.GetById(model.Id);
			if (userData == null || userData.TypeAccount != AccountTypeConstant.EndUser)
			{
				result.MessageFail("Không tìm thấy tài khoản");
				return Json(result);
			}
			userData.Email = model.Email;
			_appUserService.Update(userData);
			return Json(result);
		}

		[PermissionAccess(Code = "EndUserChangePass")]
		public PartialViewResult UpdatePassEnduser(long id)
		{
			var model = new UpdateEnduserPasswordVM();
			var userData = _appUserService.GetById(id);
			if (userData != null && userData.TypeAccount == AccountTypeConstant.EndUser)
			{
				ViewBag.userData = userData;
				model.Id = userData.Id;
			}
			return PartialView(model);
		}

		[PermissionAccess(Code = "EndUserChangePass")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult UpdatePassEnduser(UpdateEnduserPasswordVM model)
		{
			var result = new JsonResultBO(true, "Cập nhật mật khẩu thành công");
			try
			{
				var user = _appUserService.GetById(model.Id);
				if (user == null || user.TypeAccount != AccountTypeConstant.EndUser)
				{
					result.MessageFail("Không tìm thấy tài khoản");
					return Json(result);
				}
				_appUserService.Update(user);
				var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
				if (string.IsNullOrEmpty(user.SecurityStamp))
				{
					var rsUpdateStamp = UserManager.UpdateSecurityStamp(user.Id);
				}
				var userObj = UserManager.FindById(user.Id);

				var tokenUpdatePass = UserManager.GeneratePasswordResetToken(user.Id);

				var rs = UserManager.ResetPassword(user.Id, tokenUpdatePass, model.Password);
				if (!rs.Succeeded)
				{
					result.MessageFail(rs.Errors.FirstOrDefault());
					return Json(result);
				}
			}
			catch (Exception ex)
			{
				_Ilog.Error(ex.Message, ex);
				result.MessageFail(ex.Message);
				return Json(result);
			}
			return Json(result);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionIndex)]
		public JsonResult LockUser(long? id, bool islock)
		{
			var result = new JsonResultBO(true);
			var obj = _appUserService.GetById(id);
			if (obj == null)
			{
				result.MessageFail("Không tìm thấy thông tin");
				return Json(result);
			}
			try
			{
				if (islock == true)
				{
					obj.LockoutEnabled = true;
					obj.LockoutEndDateUtc = DateTime.Now.AddYears(1000);
				}
				else if (islock == false)
				{
					obj.LockoutEnabled = false;
					obj.LockoutEndDateUtc = null;
				}
				_appUserService.Update(obj);
			}
			catch (Exception ex)
			{
				result.Status = false;
				result.Message = "Không khóa/mở khóa được tài khoản";
				_Ilog.Error("Lỗi khóa/mở khóa tài khoản", ex);
				result.MessageFail(ex.Message);
				return Json(result);
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionExport)]
		public ActionResult Import()
		{
			var model = new ImportVM();
			model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_AppUser"]);

			return View(model);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionExport)]
		public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
		{
			JsonResultImportBO<AppUserImportDto> result = new JsonResultImportBO<AppUserImportDto>(true);
			//Kiểm tra file có tồn tại k?
			if (fileImport == null)
			{
				result.Status = false;
				result.Message = "Không có file đọc dữ liệu";
				return View(result);
			}

			//Lưu file upload để đọc
			var saveFileResult = UploadProvider.SaveFile(fileImport, null, ".xls,.xlsx", null, "TempImportFile", HostingEnvironment.MapPath("/Uploads"));
			if (!saveFileResult.status)
			{
				result.Status = false;
				result.Message = saveFileResult.message;
				return View(result);
			}
			else
			{
				#region Config để import dữ liệu

				var importHelper = new ImportExcelHelper<AppUserImportDto>();
				importHelper.PathTemplate = saveFileResult.fullPath;
				//importHelper.StartCol = 2;
				importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
				importHelper.ConfigColumn = new List<ConfigModule>();
				importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<AppUserImportDto>(collection);

				#endregion Config để import dữ liệu

				var rsl = importHelper.ImportCustomRow();
				if (rsl.ListTrue != null && rsl.ListTrue.Any())
				{
					var lstTrueCheckTrung = rsl.ListTrue.ToList();
					rsl.ListTrue = new List<AppUserImportDto>();
					foreach (var item in lstTrueCheckTrung)
					{
						if (!rsl.ListTrue.Any(x => x.UserName.Equals(item.UserName)) && _appUserService.CheckExistUserName(item.UserName) != true)
						{
							item.UserName = item.UserName;
							rsl.ListTrue.Add(item);
						}
						else
						{
							var stringErr = "+ Tên đăng nhập trùng lặp \n";
							rsl.lstFalse.Add(ExcelImportExtention.GetErrMess<AppUserImportDto>(item, stringErr));
						}
					}
				}
				if (rsl.Status)
				{
					result.Status = true;
					result.Message = rsl.Message;

					result.ListData = rsl.ListTrue;
					result.ListFalse = rsl.lstFalse;
				}
				else
				{
					result.Status = false;
					result.Message = rsl.Message;
				}
			}
			return View(result);
		}

		[HttpPost]
		[PermissionAccess(Code = permissionExport)]
		public JsonResult GetExportError(List<List<string>> lstData)
		{
			ExportExcelHelper<AppUserImportDto> exPro = new ExportExcelHelper<AppUserImportDto>();
			exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
			exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_AppUser"]);
			exPro.StartRow = 5;
			exPro.StartCol = 2;
			exPro.FileName = "ErrorImportAppUser";
			var result = exPro.ExportText(lstData);
			if (result.Status)
			{
				result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
			}
			return Json(result);
		}

		[PermissionAccess(Code = permissionExport)]
		public FileResult ExportExcel()
		{
			var searchModel = SessionManager.GetValue("UserPageSearchModel") as AppUserSearchDto;
			var data = _appUserService.GetDaTaByPage(searchModel, 1, -1).ListItem;
			var dataExport = new List<AppUserExportDto>();
			foreach (var item in data)
			{
				var AppUserExportDto = new AppUserExportDto();
				AppUserExportDto.FullName = item.FullName;
				AppUserExportDto.UserName = item.UserName;
				AppUserExportDto.BirthDay = item.BirthDay.HasValue ? item.BirthDay.Value.ToString("dd/MM/yyyy") : "";
				AppUserExportDto.Email = item.Email;
				AppUserExportDto.PhoneNumber = item.PhoneNumber;
				AppUserExportDto.Address = item.Address;
				dataExport.Add(AppUserExportDto); ;
			}
			var fileExcel = ExportExcelV2Helper.Export(dataExport);
			return File(fileExcel, "application/octet-stream", "DanhSachNguoiDung.xlsx");
		}

		[HttpPost]
		[PermissionAccess(Code = permissionExport)]
		public JsonResult SaveImportData(List<AppUserImportDto> Data)
		{
			var result = new JsonResultBO(true);

			var lstObjSave = new List<AppUser>();
			try
			{
				foreach (var item in Data)
				{
					var obj = _mapper.Map<AppUser>(item);
					obj.TypeAccount = AccountTypeConstant.BussinessUser;

					var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
					//Kiểm tra thông tin tài khoản
					var resultUser = UserManager.CreateAsync(obj, UserConst.DefaultPassword).Result;
					if (!resultUser.Succeeded)
					{
						throw new Exception(getErrorString(resultUser));
					}
				}
			}
			catch (Exception ex)
			{
				result.Status = false;
				result.Message = "Lỗi dữ liệu, không thể import";
				_Ilog.Error("Lỗi Import", ex);
			}

			return Json(result);
		}

		/// <summary>
		/// /UserArea/User/GenUser
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[PermissionAccess(Code = permissionIndex)]
		public JsonResult GenUser()
		{
			var result = new JsonResultBO(true, "Tạo người dùng thành công");
			try
			{
				var queryVaiTro = "SELECT * FROM Role WHERE CODE IN ('CHUYENVIENDONVI', 'LANHDAODONVI')";

				var listVaiTro = _roleService.GetListBySqlQuery(queryVaiTro);

				var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
			}
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[PermissionAccess(Code = permissionDSQuyen)]
		public ActionResult DSQuyenNguoiDung(long id)
		{
			var viewModel = _userOperationService.GetConfigureOperation(id);
			return View(viewModel);
		}

		[HttpGet]
		[PermissionAccess(Code = permissionIndex)]
		public ActionResult Checked(long id)
		{
			List<UserOperation> obsoluteData = _userOperationService.FindBy(x => x.UserId == id).ToList();
			if (obsoluteData.Count > 0)
			{
				return Json(true, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(false, JsonRequestBehavior.AllowGet);
			}
		}

		// lấy hai từ cuối của tên
		public static string GetLastTwoWordsWithoutDiacritics(string input)
		{
			// Kiểm tra chuỗi đầu vào null hoặc rỗng
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty; // Trả về chuỗi rỗng nếu đầu vào không hợp lệ
			}

			// Tách chuỗi thành các từ và loại bỏ các phần tử rỗng
			string[] words = input.Split(' ').Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();

			// Nếu chuỗi chỉ có 1 từ, trả về từ đó đã bỏ dấu
			if (words.Length == 1)
			{
				return RemoveDiacritics(words[0]);
			}

			// Lấy 2 từ cuối và nối lại
			if (words.Length >= 2)
			{
				string lastTwoWords = words[words.Length - 2] + words[words.Length - 1];
				return RemoveDiacritics(lastTwoWords);
			}

			// Trường hợp không có từ nào hợp lệ
			return string.Empty;
		}

		// bỏ dấu
		public static string RemoveDiacritics(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}

			// Thay thế riêng chữ đ và Đ
			text = text.Replace('đ', 'd').Replace('Đ', 'D');

			// Chuẩn hóa và loại bỏ dấu diacritics
			string normalizedText = text.ToLower().Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();

			foreach (char c in normalizedText)
			{
				// Bỏ các ký tự thuộc loại dấu (diacritics)
				if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) !=
					System.Globalization.UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			// Trả về chuỗi không có dấu
			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}