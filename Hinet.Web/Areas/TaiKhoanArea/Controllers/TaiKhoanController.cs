using AutoMapper;
using CommonHelper;
using Hinet.Model;
using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DanhMucGameService;
using Hinet.Service.DanhMucGameTaiKhoanService;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.GameService;
using Hinet.Service.TaiKhoanService;
using Hinet.Service.TaiKhoanService.Dto;
using Hinet.Service.TaiLieuDinhKemService;
using Hinet.Service.ThuocTinhService;
using Hinet.Web.Areas.TaiKhoanArea.Models;
using Hinet.Web.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Areas.TaiKhoanArea.Controllers
{
	public class TaiKhoanController : BaseController
	{
		private readonly ILog _Ilog;
		private readonly IMapper _mapper;
		public const string permissionIndex = "TaiKhoan_index";
		public const string permissionCreate = "TaiKhoan_create";
		public const string permissionEdit = "TaiKhoan_edit";
		public const string permissionDelete = "TaiKhoan_delete";
		public const string permissionImport = "TaiKhoan_Inport";
		public const string permissionExport = "TaiKhoan_export";
		public const string searchKey = "TaiKhoanPageSearchModel";
		private readonly ITaiKhoanService _TaiKhoanService;
		private readonly IDM_DulieuDanhmucService _dM_DulieuDanhmucService;
		private readonly IGameService _gameService;
		private readonly ITaiLieuDinhKemService _taiLieuDinhKemService;
		private readonly IDanhMucGameTaiKhoanService _danhMucGameTaiKhoanService;
		private readonly IDanhMucGameService _danhMucGameService;
		private readonly IThuocTinhService _thuocTinhService;

        public TaiKhoanController(ITaiKhoanService TaiKhoanService, ILog Ilog,
            IDM_DulieuDanhmucService dM_DulieuDanhmucService,
            IMapper mapper,
            IGameService gameService, ITaiLieuDinhKemService taiLieuDinhKemService,
            IDanhMucGameTaiKhoanService danhMucGameTaiKhoanService, IDanhMucGameService danhMucGameService, IThuocTinhService thuocTinhService)
        {
            _TaiKhoanService = TaiKhoanService;
            _Ilog = Ilog;
            _mapper = mapper;
            _dM_DulieuDanhmucService = dM_DulieuDanhmucService;
            _gameService = gameService;
            _taiLieuDinhKemService = taiLieuDinhKemService;
            _danhMucGameTaiKhoanService = danhMucGameTaiKhoanService;
            _danhMucGameService = danhMucGameService;
            _thuocTinhService = thuocTinhService;
        }

        // GET: TaiKhoanArea/TaiKhoan
        //[PermissionAccess(Code = permissionIndex)]
        public ActionResult Index(int? id = null)
		{
			var searchModel = new TaiKhoanSearchDto
			{
				GameIdFilter = id
			};
			ViewBag.Game = _gameService.GetById(id) as Game;
            ViewBag.dropdownListGameId = _gameService.GetDropdown("Name", "Id");
			SessionManager.SetValue(searchKey, searchModel);
            var listData = _TaiKhoanService.GetDaTaByPage(searchModel);
			return View(listData);
		}

		[HttpPost]
		public JsonResult getData(int indexPage, string sortQuery, int pageSize)
		{
			var searchModel = SessionManager.GetValue(searchKey) as TaiKhoanSearchDto;
			if (!string.IsNullOrEmpty(sortQuery))
			{
				if (searchModel == null)
				{
					searchModel = new TaiKhoanSearchDto();
				}
				searchModel.sortQuery = sortQuery;
				if (pageSize > 0)
				{
					searchModel.pageSize = pageSize;
				}
				SessionManager.SetValue(searchKey, searchModel);
			}
			var data = _TaiKhoanService.GetDaTaByPage(searchModel, indexPage, pageSize);
			return Json(data);
		}

		public PartialViewResult Create(int? id = null)
		{
			var myModel = new CreateVM()
			{
				GameId = id,
				ThuocTinhs = _thuocTinhService.GetDaTaByGameId(id.GetValueOrDefault())
			};
			//var game = _gameService.GetById(id);
            ViewBag.dropdownListGameId = _gameService.GetDropdown("Name", "Id");
			ViewBag.dropdownListDanhMucId = new List<SelectListItem>();
            return PartialView("_CreatePartial", myModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Create(CreateVM model, string[] UploadedFiles)
		{
			var result = new JsonResultBO(true, "Tạo  thành công");
			try
			{
				if (ModelState.IsValid)
				{
					var entity = _mapper.Map<TaiKhoan>(model);
					_TaiKhoanService.Create(entity);

                    //Các tài liệu đính kèm liên quan
                    var listTaiLieu = new List<TaiLieuDinhKem>();

					if (UploadedFiles != null)
					{
						var finalPath = Server.MapPath($"~/Uploads/TaiKhoan/{entity.Id}");
						if (!Directory.Exists(finalPath)) Directory.CreateDirectory(finalPath);

						foreach (var fileName in UploadedFiles)
						{
							var tempPath = Path.Combine(Server.MapPath("~/Uploads/Temp"), fileName);
							var destPath = Path.Combine(finalPath, fileName);

							if (System.IO.File.Exists(tempPath))
							{
								System.IO.File.Move(tempPath, destPath);
							}

							listTaiLieu.Add(new TaiLieuDinhKem
							{
								TenTaiLieu = fileName,
								LoaiTaiLieu = nameof(TaiKhoan),
								Item_ID = entity.Id,
								MoTa = string.Empty,
                                DuongDanFile = $"/Uploads/TaiKhoan/{entity.Id}/{fileName}",
                            });
						}
					}
					_taiLieuDinhKemService.InsertRange(listTaiLieu);
				}
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
				_Ilog.Error("Lỗi tạo mới ", ex);
			}
			return Json(result);
		}

		public PartialViewResult Edit(long id)
		{
			var myModel = new EditVM();

			var obj = _TaiKhoanService.GetById(id);
			if (obj == null)
			{
				throw new HttpException(404, "Không tìm thấy thông tin");
			}

            ViewBag.dropdownListGameId = _gameService.GetDropdown("Name", "Id");
            ViewBag.dropdownListDanhMucId = _danhMucGameService.GetDanhMucByGame(obj.GameId).Select(x => new SelectListItem()
			{
				Value = x.Id.ToString(),
				Text = x.Name
            }).ToList();

            myModel = _mapper.Map(obj, myModel);
			myModel.TaiLieuDinhKemList = _taiLieuDinhKemService.GetListTaiLieuAllByType(nameof(TaiKhoan), id);
			return PartialView("_EditPartial", myModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Edit(EditVM model, string[] UploadedFiles)
		{
			var result = new JsonResultBO(true, "Cập nhật thành công");
			try
			{
				if (ModelState.IsValid)
				{
					// Lấy entity cũ từ DB
					var entity = _TaiKhoanService.GetById(model.Id);
					if (entity == null)
					{
						result.MessageFail("Không tìm thấy tài khoản");
						return Json(result);
					}

					// Cập nhật thông tin
					_mapper.Map(model, entity);
					_TaiKhoanService.Update(entity);

                    // Danh sách file hiện tại trong DB
                    var oldFiles = _taiLieuDinhKemService.GetListTaiLieuAllByType(nameof(TaiKhoan), entity.Id);

					// Nếu form không gửi gì thì coi như []
					var keepFiles = UploadedFiles ?? new string[0];

					var finalPath = Server.MapPath($"~/Uploads/TaiKhoan/{entity.Id}");
					if (!Directory.Exists(finalPath)) Directory.CreateDirectory(finalPath);

					// 1. Xóa file không còn trong danh sách
					var removedFiles = oldFiles.Where(f => !keepFiles.Contains(f.TenTaiLieu)).ToList();
					foreach (var f in removedFiles)
					{
						if (System.IO.File.Exists(f.DuongDanFile))
							System.IO.File.Delete(f.DuongDanFile);
						_taiLieuDinhKemService.Delete(f);
					}

					var newFiles = new List<TaiLieuDinhKem>();
					// 2. Thêm file mới upload
					foreach (var fileName in keepFiles)
					{
						if (!oldFiles.Any(f => f.TenTaiLieu == fileName))
						{
							var tempPath = Path.Combine(Server.MapPath("~/Uploads/Temp"), fileName);
							var destPath = Path.Combine(finalPath, fileName);

							if (System.IO.File.Exists(tempPath))
								System.IO.File.Move(tempPath, destPath);

							newFiles.Add(new TaiLieuDinhKem
							{
								TenTaiLieu = fileName,
								LoaiTaiLieu = nameof(TaiKhoan),
								Item_ID = entity.Id,
								MoTa = string.Empty,
								DuongDanFile = $"/Uploads/TaiKhoan/{entity.Id}/{fileName}",
							});
						}
					}
					if (newFiles.Any())
					{
						_taiLieuDinhKemService.InsertRange(newFiles);
					}
				}
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
				_Ilog.Error("Lỗi cập nhật ", ex);
			}
			return Json(result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult searchData(TaiKhoanSearchDto form)
		{
			var searchModel = SessionManager.GetValue(searchKey) as TaiKhoanSearchDto;

			if (searchModel == null)
			{
				searchModel = new TaiKhoanSearchDto();
				searchModel.pageSize = 20;
			}
			searchModel.CodeFilter = form.CodeFilter;
			searchModel.GameIdFilter = form.GameIdFilter;
			searchModel.TrangThaiFilter = form.TrangThaiFilter;
			searchModel.UserNameFilter = form.UserNameFilter;
			searchModel.PasswordFilter = form.PasswordFilter;
			searchModel.GiaGocFilter = form.GiaGocFilter;
			searchModel.GiaKhuyenMaiFilter = form.GiaKhuyenMaiFilter;
			searchModel.MotaFilter = form.MotaFilter;
			searchModel.ViTriFilter = form.ViTriFilter;

			SessionManager.SetValue((searchKey), searchModel);

			var data = _TaiKhoanService.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
			return Json(data);
		}

		[HttpPost]
		public JsonResult Delete(long id)
		{
			var result = new JsonResultBO(true, "Xóa  thành công");
			try
			{
				var user = _TaiKhoanService.GetById(id);
				if (user == null)
				{
					throw new Exception("Không tìm thấy thông tin để xóa");
				}
				_TaiKhoanService.Delete(user);
			}
			catch (Exception ex)
			{
				result.MessageFail("Không thực hiện được");
				_Ilog.Error("Lỗi khi xóa tài khoản id=" + id, ex);
			}
			return Json(result);
		}

		public ActionResult Detail(long id)
		{
			var model = new DetailVM();
			model.objInfo = _TaiKhoanService.GetById(id);
			return View(model);
		}

		[HttpPost]
		public ActionResult UploadAnh(HttpPostedFileBase file)
		{
			if (file != null && file.ContentLength > 0)
			{
				var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
				var tempPath = Server.MapPath("~/Uploads/Temp");
				if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

				var path = Path.Combine(tempPath, fileName);
				file.SaveAs(path);

				return Json(new { success = true, fileName = fileName });
			}
			return Json(new { success = false });
		}
	}
}