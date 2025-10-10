using CommonHelper.String;
using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.AppUserService.Dto;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService;
using Hinet.Service.TaiLieuDinhKemService;
using Hinet.Web.Common;
using Hinet.Web.Filters;
using Hinet.Web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
	public class CommonController : BaseController
	{
		// GET: Common
		private readonly ILog _Ilog;

		private readonly ITaiLieuDinhKemService _TaiLieuDinhKemService;
		private readonly IDM_DulieuDanhmucService _DM_DulieuDanhmucService;
		protected string uploadPath = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UPLOAD_FOLDER"]);

		public CommonController(
			ITaiLieuDinhKemService TaiLieuDinhKemService,
			IDM_DulieuDanhmucService DM_DulieuDanhmucService,
			ILog Ilog)
		{
			_TaiLieuDinhKemService = TaiLieuDinhKemService;
			_DM_DulieuDanhmucService = DM_DulieuDanhmucService;
			_Ilog = Ilog;
		}

		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// @author:duynn
		/// @description:xóa file
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult DeleteFile(long id)
		{
			JsonResultBO result = new JsonResultBO(true);
			try
			{
				var entity = _TaiLieuDinhKemService.GetById(id);
				if (entity == null)
				{
					result.Status = false;
					result.Message = "Tài liệu đính kèm không tồn tại";
					return Json(result);
				}

				_TaiLieuDinhKemService.Delete(entity);

				//xóa file vật lý
				string virtualPath = $"Uploads\\{entity.DuongDanFile}";
				string physicsPath = $"{Server.MapPath("~")}\\{virtualPath}";
				if (System.IO.File.Exists(physicsPath))
				{
					System.IO.File.Delete(physicsPath);
				}
			}
			catch (Exception ex)
			{
				_Ilog.Error(ex.Message, ex);
				result.Status = false;
				result.Message = "Xóa tài liệu đính kèm thất bại";
			}
			return Json(result);
		}

		/// <summary>
		/// @author:duynn
		/// @description:cập nhật file tạm
		/// </summary>
		/// <param name="itemType"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult UploadMultipleFiles(string itemType = "")
		{
			var result = new JsonResultBO(true);
			var lstResult = new List<string>();
			for (int i = 0; i < Request.Files.Count; i++)
			{
				HttpPostedFileBase file = Request.Files[i]; //Uploaded file
															//Use the following properties to get file's name, size and MIMEType
				int fileSize = file.ContentLength;
				string fileName = StringUtilities.RemoveSign4VietnameseString(file.FileName);
				string mimeType = file.ContentType;
				System.IO.Stream fileContent = file.InputStream;
				//To save file, use SaveAs method

				if (!string.IsNullOrEmpty(itemType))
				{
					var path = Server.MapPath($"~/Uploads/Temp/{CurrentUserInfo.Id}/" + itemType);
					if (!System.IO.Directory.Exists(path))
					{
						System.IO.Directory.CreateDirectory(path);
					}
				}

				if (System.IO.File.Exists(Server.MapPath($"~/Uploads/Temp/{CurrentUserInfo.Id}/" + (!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)) + fileName))
				{
					fileName = string.Format("{0:dd-MM-yyyy-HH-mm-ss-ffffff}", DateTime.Now) + fileName;
				}
				file.SaveAs(Server.MapPath($"~/Uploads/Temp/{CurrentUserInfo.Id}/" + (!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)) + fileName); //File will be saved in application root
				lstResult.Add(fileName);
			}
			return Json(lstResult);
		}

		/// <summary>
		/// @author:duynn
		/// @description:cập nhật file tạm
		/// </summary>
		/// <param name="name"></param>
		/// <param name="itemType"></param>
		/// <returns></returns>
		public JsonResult DeleteTempFile(string name, string itemType = "")
		{
			var result = new JsonResultBO(true);
			if (System.IO.File.Exists(Server.MapPath($"~/Uploads/Temp/" + (!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)) + name))
			{
				System.IO.File.Delete(Server.MapPath($"~/Uploads/Temp/" + (!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)) + name);
			}
			return Json(result);
		}

		[ValidateInput(false)]
		public void Export(string GridHtml, string TenFile = "", string Css = "")
		{
			StringBuilder StrExport = new StringBuilder();
			var header = string.Empty;
			if (string.IsNullOrEmpty(Css))
			{
				header =
					"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head>" +
					"<title></title><meta charset='utf-8'/><style>table td{border: 1px solid #ddd}table th{border: 1px solid #ddd}</style>" +
					"</head>";
			}
			else
			{
				header = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head>" +
					"<title></title><meta charset='utf-8'/>" +
						 Css +
					"</head>";
			}
			StrExport.Append(@header);
			StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
			StrExport.Append("<div style='font-size:12px;'>");
			StrExport.Append(GridHtml);
			StrExport.Append("</div></body></html>");
			string strFile = string.Empty;
			if (string.IsNullOrEmpty(TenFile))
			{
				strFile = "Baocao.xls";
			}
			else
			{
				strFile = TenFile.GetFileNameFormart() + ".xls";
			}

			string strcontentType = "application/x-msexcel";

			Response.ClearContent();
			//Response.ClearHeaders();
			Response.BufferOutput = true;
			//Response.ContentType = strcontentType;
			Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);

			Response.ContentType = "application/vnd.ms-excel";
			Response.Write(StrExport.ToString());
			Response.Flush();
			Response.Close();
			Response.End();
		}

		[HttpPost]
		public JsonResult UploadFile(HttpPostedFileBase NewFile, string NameOfFile, string NoteOfFile, string type)
		{
			var CurrentUserInfo = SessionManager.GetUserInfo() as UserDto;
			var result = new JsonResultBO(true);
			var appAttachment = new AttachTempFileItem();
			var listFileTemp = SessionManager.GetValue("FileTem" + type) as List<AttachTempFileItem>;
			if (listFileTemp == null)
			{
				listFileTemp = new List<AttachTempFileItem>();
			}
			if (NewFile != null)
			{
				var pathDate = DateTime.Now.ToString("yyyy/MM/dd/");
				if (CurrentUserInfo != null)
				{
					pathDate += CurrentUserInfo.Id.ToString() + "/";
				}
				else
				{
					pathDate += "unknow/";
				}
				var resultUpload = UploadProvider.SaveFile(NewFile, "", UploadProvider.ListExtensionCommon, UploadProvider.MaxSizeCommon, "Uploads/TempUpload/" + pathDate, HostingEnvironment.MapPath("/"));
				if (resultUpload.status)
				{
					appAttachment.IdToken = Guid.NewGuid().ToString();
					appAttachment.Path = resultUpload.path;
					appAttachment.Name = Path.GetFileName(resultUpload.path);
					appAttachment.Size = NewFile.ContentLength;
					appAttachment.Extension = NewFile.ContentType;
					appAttachment.Note = NoteOfFile;
					listFileTemp.Add(appAttachment);
					SessionManager.SetValue("FileTem" + type, listFileTemp);
				}
				else
				{
					result.MessageFail(resultUpload.message);
					return Json(result);
				}
			}
			else
			{
				result.MessageFail("Vui lòng chọn tệp để tải lên");
				return Json(result);
			}
			return Json(result);
		}

		[ValidateInput(false)]
		public JsonResult UploadMultipleTempFiles(string itemType)
		{
			string serverPhysicsPath = Server.MapPath("~");
			var result = new List<string>();
			for (int i = 0; i < Request.Files.Count; i++)
			{
				HttpPostedFileBase file = Request.Files[i];
				string mimeType = file.ContentType;
				int fileSize = file.ContentLength;
				string fileName = StringUtilities.RemoveSign4VietnameseString(file.FileName);

				FileInfo fi = new FileInfo(fileName);
				string extension = fi.Extension;

				System.IO.Stream fileContent = file.InputStream;

				if (!string.IsNullOrEmpty(itemType))
				{
					var path = $"{serverPhysicsPath}/Uploads/Temp/{itemType}";
					if (!System.IO.Directory.Exists(path))
					{
						System.IO.Directory.CreateDirectory(path);
					}
				}

				if (System.IO.File.Exists($"{serverPhysicsPath}/Uploads/Temp/{(!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)}/{fileName}"))
				{
					fileName = string.Format("{0:dd-MM-yyyy-HH-mm-ss-ffffff}", DateTime.Now) + fileName;
				}
				string filePath = $"{serverPhysicsPath}/Uploads/Temp/{(!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)}/{fileName}";
				file.SaveAs(filePath); //File will be saved in application root
				result.Add(fileName);
			}
			return Json(result);
		}
	}
}