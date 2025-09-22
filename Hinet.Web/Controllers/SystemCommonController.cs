using CommonHelper.ConvertToPDF;
using CommonHelper.String;
using Hinet.Service.Common;
using Hinet.Service.TaiLieuDinhKemService;
using Hinet.Web.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.Mvc;
using static Hinet.Service.Common.Constant;

namespace Hinet.Web.Controllers
{
	public class SystemCommonController : Controller
	{
		private readonly ITaiLieuDinhKemService _TaiLieuDinhKemService;
		private readonly log4net.ILog _logger;
		//private readonly ILichSuXuLyService _LichSuXuLyService;

		public SystemCommonController(
			ITaiLieuDinhKemService TaiLieuDinhKemService,
			//ILichSuXuLyService LichSuXuLyService,
			log4net.ILog logger)
		{
			_TaiLieuDinhKemService = TaiLieuDinhKemService;
			//_LichSuXuLyService = LichSuXuLyService;
			_logger = logger;
		}

		/// <summary>
		/// author:duynn
		/// description: tải lên nhiều file
		/// </summary>
		/// <param name="itemType"></param>
		/// <returns></returns>
		[ValidateInput(false)]
		public JsonResult UploadMultipleTempFiles(string itemType)
		{
			string serverPhysicsPath = Server.MapPath("~");
			var result = new JsonResultBO(true);
			var fileNames = new List<string>();
			for (int i = 0; i < Request.Files.Count; i++)
			{
				var file = Request.Files[i];
				var mimeType = file.ContentType;
				var fileSize = file.ContentLength;
				var fileName = StringUtilities.RemoveSign4VietnameseString(file.FileName);
				var checkFile = true;

				if (!checkFile)
				{
					fileNames = null;
					result.MessageFail("Tập tin không được hỗ trợ.");
					break;
				}

				if (!string.IsNullOrEmpty(itemType))
				{
					var path = $"{serverPhysicsPath}/Uploads/Temp/{itemType}";
					if (!System.IO.Directory.Exists(path))
					{
						System.IO.Directory.CreateDirectory(path);
					}
				}

				//để file đỡ bị dài quá
				//var fi = new FileInfo(fileName);
				//var extension = fi.Extension;
				//fileName = string.Format("{0:dd-MM-yyyy-HH-mm-ss}", DateTime.Now) + extension;
				var tempFilePath = $"{serverPhysicsPath}/Uploads/Temp/{itemType}/{fileName}";
				if (string.IsNullOrEmpty(itemType))
				{
					tempFilePath = $"{serverPhysicsPath}/Uploads/Temp/{fileName}";
				}
				if (System.IO.File.Exists(tempFilePath))
				{
					fileName = string.Format("{0:dd-MM-yyyy-HH-mm-ss-ffffff}---", DateTime.Now) + fileName;
				}
				var filePath = $"{serverPhysicsPath}/Uploads/Temp/{itemType}/{fileName}";
				if (string.IsNullOrEmpty(itemType))
				{
					filePath = $"{serverPhysicsPath}/Uploads/Temp/{fileName}";
				}
				file.SaveAs(filePath); //File will be saved in application root
				result.Status = true;
				fileNames.Add(fileName);
			}
			result.Param = fileNames;
			return Json(result);
		}

		/// <summary>
		/// author:duynn
		/// description: xóa file tạm của văn bản đến
		/// </summary>
		/// <param name="name"></param>
		/// <param name="itemType"></param>
		/// <returns></returns>
		public JsonResult DeleteTempFile(string name, string itemType)
		{
			string serverPhysicsPath = Server.MapPath("~");
			var result = new JsonResultBO(true);
			if (System.IO.File.Exists($"{serverPhysicsPath}/Uploads/Temp/{(!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)}/{name}"))
			{
				System.IO.File.Delete($"{serverPhysicsPath}/Uploads/Temp/{(!string.IsNullOrEmpty(itemType) ? itemType + "/" : string.Empty)}/{name}");
			}
			result.Message = name;
			return Json(result);
		}

		[HttpGet]
		[ValidateInput(false)]
		public JsonResult DeleteFile(string path, string type)
		{
			var result = new JsonResultBO(true);

			var listFileTemp = SessionManager.GetValue("FileTem" + type) as List<AttachTempFileItem>;
			if (listFileTemp != null)
			{
				for (int i = 0; i < listFileTemp.Count; i++)
				{
					if (listFileTemp[i].Path == path)
					{
						try
						{
							System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("/"), listFileTemp[i].Path));
							listFileTemp.RemoveAt(i);
							SessionManager.SetValue("FileTem" + type, listFileTemp);
						}
						catch (Exception exp)
						{
							_logger.Error(exp.Message, exp);
							result.MessageFail("Không xóa được tài liệu cũ");
						}
					}
				}
			}
			result.Message = "Xóa tài liệu thành công";
			result.Status = true;
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult DownloadFile(long id)
		{
			var entityFile = _TaiLieuDinhKemService.GetById(id);

			if (entityFile == null || entityFile.DuongDanFile == null)
			{
				return RedirectToAction("FileNotFound", "Home");
			}
			else
			{
				string filePath = $"/Uploads/{entityFile.DuongDanFile}";
				filePath = Server.MapPath(filePath);
				if (!System.IO.File.Exists(filePath))
				{
					return RedirectToAction("FileNotFound", "Home");
				}
				_TaiLieuDinhKemService.Save(entityFile);

				FileInfo fi = new FileInfo(filePath);
				string fileName = fi.Name;
				return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
			}
		}

		[HttpGet]
		[ValidateInput(false)]
		[AllowAnonymous]
		public FileResult ExportTable(string content, string name)
		{
			name = name ?? "FileExport";
			var memoryStream = new MemoryStream();
			var excelPackage = new ExcelPackage(memoryStream);
			var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
			var regexth = new Regex(@"<th>(.*?)</th>");
			var regextr = new Regex(@"<tr>(.*?)</tr>");
			var regextd = new Regex(@"<td>(.*?)</td>");
			var ths = regexth.Matches(content).OfType<Match>();
			var trs = regextr.Matches(content).OfType<Match>();
			var typeString = typeof(string);
			try
			{
				var FileData = new DataTable();

				DataTable Dt = new DataTable();
				foreach (var th in ths)
				{
					var col = new DataColumn(th.Groups[1].Value, typeString);
					Dt.Columns.Add(col);
				}

				foreach (var tr in trs)
				{
					var tds = regextd.Matches(tr.Groups[1].Value).OfType<Match>().ToList();
					if (tds.Count > 0)
					{
						DataRow row = Dt.NewRow();
						for (int i = 0; i < tds.Count; i++)
						{
							row[i] = tds[i].Groups[1].Value;
						}
						Dt.Rows.Add(row);
					}
				}
				worksheet.Cells.AutoFitColumns();
				worksheet.DefaultColWidth = 20;
				worksheet.DefaultRowHeight = 20;
				worksheet.Cells.Style.WrapText = true;
				worksheet.Cells.Style.Font.Size = 12;
				worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
				worksheet.Cells.Style.WrapText = true;
				worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.Light13);
			}
			catch (Exception)
			{
			}

			return File(excelPackage.GetAsByteArray(), "application/octet-stream", $"{name}.xlsx");
		}

		[HttpPost]
		[ValidateInput(false)]
		public JsonResult TableToExcel(string content, string name)
		{
			var result = new JsonResultBO(true, "Thành công");
			name = name ?? "FileExport";
			var memoryStream = new MemoryStream();
			var excelPackage = new ExcelPackage(memoryStream);
			var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
			var regexth = new Regex(@"<th[ |>].*?<span>(.*?)</span>.*?</th>");
			var regextbody = new Regex(@"<tbody[ |>](.*?)</tbody>");
			var regextr = new Regex(@"<tr.*?>(.*?)</tr>");
			var regextd = new Regex(@"<td.*?>(.*?)</td>");
			var ths = regexth.Matches(content).OfType<Match>().ToList();

			var tbody = regextbody.Match(content).Value;

			var trs = regextr.Matches(tbody).OfType<Match>().ToList();
			var typeString = typeof(string);
			try
			{
				var FileData = new DataTable();

				DataTable Dt = new DataTable();
				foreach (var th in ths)
				{
					var col = new DataColumn(th.Groups[1].Value, typeString);
					Dt.Columns.Add(col);
				}

				foreach (var tr in trs)
				{
					var tds = regextd.Matches(tr.Groups[1].Value).OfType<Match>().ToList();
					if (tds.Count > 0)
					{
						DataRow row = Dt.NewRow();
						for (int i = 0; i < tds.Count; i++)
						{
							row[i] = tds[i].Groups[1].Value;
						}
						Dt.Rows.Add(row);
					}
				}
				worksheet.Cells.AutoFitColumns();
				worksheet.DefaultColWidth = 20;
				worksheet.DefaultRowHeight = 20;
				worksheet.Cells.Style.WrapText = true;
				worksheet.Cells.Style.Font.Size = 12;
				worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
				worksheet.Cells.Style.WrapText = true;
				worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.Light13);
			}
			catch (Exception ex)
			{
				result.MessageFail(ex.Message);
			}

			var dir = Path.Combine(Server.MapPath("~"), "Uploads", "ExcelTemp");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			var path = Path.Combine("Uploads", "ExcelTemp", name + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-ffff") + ".xlsx");
			var fileName = Path.Combine(Server.MapPath("~"), path);
			System.IO.File.Create(fileName).Close();

			excelPackage.SaveAs(new FileInfo(fileName));
			result.Param = path;
			return Json(result);
		}

		//[HttpGet]
		////[AllowAnonymous]
		//public ActionResult DownloadZip(string itemType, long id)
		//{
		//    var LichSuXuly = _LichSuXuLyService.GetById(id);
		//    if (LichSuXuly == null)
		//    {
		//        return RedirectToAction("NotFound", "Errors");
		//    }

		//    var entityFiles = _TaiLieuDinhKemService.GetListTaiLieuAllByType(itemType, id);
		//    if (entityFiles.Count == 0)
		//    {
		//        return RedirectToAction("InternalServer", "Errors");
		//    }
		//    else
		//    {
		//        string zipName = $"Tailieu_{DateTime.Now:dd-MM-yyyy}";
		//        string path = Server.MapPath($"~/Uploads/{MODULE_CONSTANT.LICHSU_XULY}/{id}");
		//        string tempZip = Server.MapPath($"~/Uploads/Temp/{zipName}.zip");
		//        if (System.IO.File.Exists(tempZip))
		//            System.IO.File.Delete(tempZip);
		//        ZipFile.CreateFromDirectory(path, tempZip, System.IO.Compression.CompressionLevel.Fastest, false);
		//        FileInfo fi = new FileInfo(tempZip);
		//        string fileName = fi.Name;
		//        return File(tempZip, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
		//    }
		//}
	}
}