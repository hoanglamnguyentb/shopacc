using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using MSExcelApp = Microsoft.Office.Interop.Excel.Application;
using MSExcelWorkBook = Microsoft.Office.Interop.Excel.Workbook;
using MSExcelWorkSheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace CommonHelper.Excel
{
    public class ExportExcelSimple<T> where T : class
    {
        //vị trí lưu file
        public string PathStore { get; set; }

        //tên file
        public string FileName { get; set; }

        //Đường dẫn template
        public string PathTemplate { get; set; }

        //dòng bắt đầu
        public int StartRow { get; set; }

        //cột bắt đầu
        public int StartCol { get; set; }

        //Cấu hình các trường thông tin theo property của class
        public List<string> ConfigColumn { get; set; }

        public ExportExcelSimple()
        {
            StartRow = 5;
            StartCol = 1;
        }

        public List<string> propertyColumns { set; get; } //danh sách thuộc tính đối tượng cần kết xuất
        public int startCell { set; get; } //cột bắt đầu
        public int startRow { set; get; } //cột kết thúc

        public string templateFilePath { set; get; } //đường dẫn file mẫu
        public string outputFolderPath { set; get; } //thư mục chứa file kết quả
        public string fileName { set; get; } //tên file kết quả

        public MSExcelApp app { set; get; }
        public MSExcelWorkBook workBook { set; get; }
        public MSExcelWorkSheet workSheet { set; get; }

        //open workbook
        public bool OpenWorkBook()
        {
            if (string.IsNullOrEmpty(templateFilePath) == false || File.Exists(templateFilePath))
            {
                app = new MSExcelApp();
                workBook = app.Workbooks.Open(templateFilePath);
                workSheet = workBook.ActiveSheet;
                return true;
            }
            else
            {
                return false;
            }
        }

        public ExportExcelResult SaveAndCloseWorkBook()
        {
            ExportExcelResult exportResult = new ExportExcelResult();
            if (string.IsNullOrEmpty(fileName))
            {
                exportResult.exportResultMessage = "Vui lòng nhập tên file";
            }
            if (string.IsNullOrEmpty(outputFolderPath) || Directory.Exists(outputFolderPath) == false)
            {
                exportResult.exportResultMessage = "Thư mục kết xuất không tồn tại!";
                return exportResult;
            }

            if (app != null && workBook != null)
            {
                string outputFilePath = Path.Combine(outputFolderPath, fileName);
                if (File.Exists(outputFilePath))
                {
                    fileName = SetNewFileName(fileName);
                    outputFilePath = Path.Combine(outputFolderPath, fileName);
                }

                workBook.SaveAs(outputFilePath);
                workBook.Close();

                app.DisplayAlerts = false;
                app.Quit();
                ExcelKiller.TerminateExcelProcess(app);

                exportResult.exportSuccess = true;
                exportResult.exportResultFileName = fileName;
            }

            return exportResult;
        }

        //fill data
        public void FillTableData(List<T> objectsToExport)
        {
            Type objectType = typeof(T);
            int rowCount = objectsToExport.Count();
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < propertyColumns.Count(); col++)
                {
                    string propertyName = propertyColumns[col];
                    object cellValue = string.Empty;
                    if (string.IsNullOrEmpty(propertyName) == false)
                    {
                        PropertyInfo property = objectType.GetProperty(propertyName);
                        if (property != null)
                        {
                            cellValue = property.GetValue(objectsToExport[row]) ?? string.Empty;
                        }
                        else
                        {
                            //trường hợp số stt
                            cellValue = (row + 1);
                        }
                    }
                    workSheet.Cells[startRow + row, startCell + col] = cellValue;
                }
            }
        }

        public void WrapTextAndAutoFitRange(Range range)
        {
            range.Columns.AutoFit();
            range.WrapText = true;
        }

        public void SetTextCenterRange(Range range)
        {
            range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
        }

        public void AlignRange(Range range)
        {
            range.Columns.AutoFit();
            range.WrapText = true;
            range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
        }

        //tạo viền
        public void SetBorderRange(Range range)
        {
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
        }

        private string SetNewFileName(string oldFileName)
        {
            string result = string.Empty;
            result = oldFileName.Replace(".xlsx", string.Empty)
                    .Replace(".xls", string.Empty)
                    .Replace(".xlt", string.Empty)
                    .Replace(".xlm", string.Empty)
                    .Replace(".xlsm", string.Empty)
                    .Replace(".xltx", string.Empty)
                    .Replace("xltm", string.Empty)
                    .Replace("xlsb", string.Empty)
                    .Replace("xla", string.Empty)
                    .Replace("xlam", string.Empty)
                    .Replace("xll", string.Empty)
                    .Replace("xlw", string.Empty) + DateTime.Now.ToString("-ddMMyyyy_hhmmss") + ".xlsx";
            return result;
        }

        private Style GetStyleCell(Workbook workbook)
        {
            Style style = workbook.Styles.Add("new style");

            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style.Borders.Color = Color.Black;
            style.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            style.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            style.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
            style.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            return style;
        }

        private Style GetStyleText(Workbook workbook)
        {
            Style styleCell = workbook.Styles.Add("new style cell");

            styleCell.Font.Name = "Times New Roman";
            styleCell.Font.Size = 14;
            styleCell.Font.Bold = true;

            return styleCell;
        }

        public ResponseExportExCel ExportText(List<List<string>> listObj)
        {
            var result = new ResponseExportExCel(true);
            if (string.IsNullOrEmpty(PathStore))
            {
                result.Status = false;
                result.Message = "Vui lòng thiết lập vị trí lưu file kết xuất";
                return result;
            }

            if (listObj == null || !listObj.Any())
            {
                result.Status = false;
                result.Message = "Không có dữ liệu kết xuất";
                return result;
            }

            if (string.IsNullOrEmpty(FileName))
            {
                result.Status = false;
                result.Message = "Vui lòng thiết lập tên file";
                return result;
            }
            if (string.IsNullOrEmpty(PathTemplate))
            {
                result.Status = false;
                result.Message = "Vui lòng thiết lập đường dẫn template kết xuất";
                return result;
            }
            else
            {
                if (!File.Exists(PathTemplate))
                {
                    result.Status = false;
                    result.Message = "Template kết xuất không tồn tại";
                    return result;
                }
            }

            if (string.IsNullOrEmpty(PathTemplate))
            {
                result.Status = false;
                result.Message = "Vui lòng thiết lập đường dẫn template kết xuất";
                return result;
            }

            try
            {
                Application app = null;
                Microsoft.Office.Interop.Excel.Workbook workbook = null;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                app = new Microsoft.Office.Interop.Excel.Application();
                workbook = app.Workbooks.Open(Path.Combine(PathTemplate));
                worksheet = workbook.ActiveSheet;
                int rows = listObj.Count;

                int startRow = StartRow;
                int startColumn = StartCol;
                //Style
                var stylecell = GetStyleCell(workbook);
                var styleText = GetStyleText(workbook);
                //End style

                //worksheet.Range[GetExcelColumnName(startColumn) + startRow, GetExcelColumnName(listObj[0].Count + startColumn) + (startRow + rows)].Style = stylecell;
                Type typeClass = typeof(T);
                var colCount = listObj[0].Count;
                for (int i = 0; i < rows; i++)
                {
                    var dataItem = listObj[i];
                    if (startColumn - 1 > 0)
                    {
                        worksheet.Cells[startRow + i, startColumn - 1] = i + 1;
                    }

                    for (int col = 0; col < colCount; col++)
                    {
                        worksheet.Cells[startRow + i, startColumn + col] = listObj[i][col];
                    }
                }

                if (!Directory.Exists(PathStore))
                {
                    Directory.CreateDirectory(PathStore);
                }

                var pathFileName = Path.Combine(PathStore, FileName + ".xlsx");
                if (File.Exists(pathFileName))
                {
                    FileName += DateTime.Now.ToString("yyyyMMdd_hhmmss");
                }
                pathFileName = Path.Combine(PathStore, FileName + ".xlsx");

                workbook.SaveAs(pathFileName);
                workbook.Close();
                app.DisplayAlerts = false;
                app.Quit();
                ExcelKiller.TerminateExcelProcess(app);
                result.Status = true;
                result.PathStore = PathStore;
                result.FileName = FileName + ".xlsx";
            }
            catch
            {
                result.Status = false;
                result.Message = "Không kết xuất được file";
            }

            return result;
        }
    }

    public class ExcelKiller
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        public static void TerminateExcelProcess(Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            var process = Process.GetProcessById(id);
            if (process != null)
            {
                process.Kill();
            }
        }
    }

    public class ExportExcelResult
    {
        public bool exportSuccess { set; get; }
        public string exportResultMessage { set; get; }
        public string exportResultUrl { set; get; }
        public string exportResultFileName { set; get; }

        public ExportExcelResult()
        {
            exportResultMessage = string.Empty;
            exportResultUrl = string.Empty;
        }
    }

    public class EpplusExcel<T> where T : class
    {
        public static HttpServerUtilityBase Server { get; }

        private static string UploadFolderPath = Server.MapPath("/Uploads");

        public string FileName { set; get; }
        public int StartColumn { set; get; }
        public int StartRow { set; get; }
        public int[] LeftAignColumnIndex { set; get; } //cấu hình cột căn giữa
        public int[] CenterRowIndex { get; set; } // cấu hình hàng căn giữa
        public Dictionary<string, string> InstanceProperties { set; get; }

        public Stream CreateExcelFile(List<T> data, Func<ExcelWorksheet, string, ExcelWorksheet> formatWorkSheet)
        {
            Type objectType = typeof(T);
            using (var excelPackage = new ExcelPackage(new MemoryStream()))
            {
                excelPackage.Workbook.Properties.Author = "Author";

                //tạo title cho file excel
                excelPackage.Workbook.Properties.Title = "Title";

                //commment
                excelPackage.Workbook.Properties.Comments = "Comments";

                //add sheet vào fiel excel
                excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //lấy sheet vừa mới tạo để thao tác
                var workSheet = excelPackage.Workbook.Worksheets[1];

                var rowCount = data.Count;
                var columnCount = this.InstanceProperties.Count;

                //int colIndex = 1;

                //int Height = 120;
                //int Width = 100;

                //đặt tên cho cột
                //for (int column = 0; column < columnCount; column++)
                //{
                //    KeyValuePair<string, string> keyValue = this.InstanceProperties.ElementAt(column);
                //    workSheet.Cells[this.StartRow, column + this.StartColumn].Value = keyValue.Value;
                //}

                for (int row = 0; row < rowCount; row++)
                {
                    var item = data[row];
                    for (int column = 0; column < columnCount; column++)
                    {
                        KeyValuePair<string, string> keyValue = this.InstanceProperties.ElementAt(column);

                        if (row == 0)
                        {
                            workSheet.Cells[this.StartRow, column + this.StartColumn].Value = keyValue.Value;
                        }

                        string propertyName = keyValue.Key;
                        object cellValue = string.Empty;
                        if (!string.IsNullOrEmpty(propertyName))
                        {
                            PropertyInfo property = objectType.GetProperty(propertyName);
                            if (property != null)
                            {
                                if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                                {
                                    cellValue = string.Format("{0:dd/MM/yyyy}", property.GetValue(data[row]));
                                }
                                else
                                {
                                    cellValue = property.GetValue(data[row]) ?? string.Empty;
                                }
                            }
                            else if (propertyName == "STT")
                            {
                                cellValue = (row + 1);
                            }
                        }
                        workSheet.Cells[this.StartRow + row + 1, column + this.StartColumn].Value = cellValue;
                        workSheet.Row(this.StartRow + row + 1).Height = 30;
                    }
                }
                //định dạng biểu mẫu
                if (formatWorkSheet != null)
                {
                    workSheet = formatWorkSheet(workSheet, this.FileName);
                }

                //tạo border cho tất cả các ô có chứa dữ liệu
                for (int row = this.StartRow; row <= (this.StartRow + data.Count); row++)
                {
                    for (int column = this.StartColumn; column <= (this.StartColumn + this.InstanceProperties.Count) - 1; column++)
                    {
                        workSheet.Cells[row, column].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                    }
                }

                ExcelRange dataRangeRowStart = workSheet.SelectedRange[this.StartRow, this.StartColumn, this.StartRow, (this.StartColumn + this.InstanceProperties.Count) - 1];
                dataRangeRowStart.Style.Font.Bold = true;
                dataRangeRowStart.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                dataRangeRowStart.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                //căn giữa cho toàn bộ sheet

                ExcelRange dataRange = workSheet.SelectedRange[this.StartRow, this.StartColumn, (this.StartRow + data.Count), (this.StartColumn + this.InstanceProperties.Count) - 1];
                //dataRange.Style.Font.Bold = true;
                dataRange.Style.Font.Size = 12;
                dataRange.Style.WrapText = true;
                dataRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                dataRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                if (this.LeftAignColumnIndex != null && this.LeftAignColumnIndex.Count() > 0)
                {
                    foreach (var column in this.LeftAignColumnIndex)
                    {
                        ExcelRange centerRange = workSheet.SelectedRange[this.StartRow, column, (this.StartRow + data.Count), column];
                        centerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    workSheet.Row(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
                if (this.CenterRowIndex != null && this.CenterRowIndex.Count() > 0)
                {
                    foreach (var row in this.CenterRowIndex)
                    {
                        workSheet.Row(row).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                }

                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFileWithImage(List<T> data, Func<ExcelWorksheet, string, ExcelWorksheet> formatWorkSheet)
        {
            Type objectType = typeof(T);
            using (var excelPackage = new ExcelPackage(new MemoryStream()))
            {
                excelPackage.Workbook.Properties.Author = "Author";

                //tạo title cho file excel
                excelPackage.Workbook.Properties.Title = "Title";

                //commment
                excelPackage.Workbook.Properties.Comments = "Comments";

                //add sheet vào fiel excel
                excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //lấy sheet vừa mới tạo để thao tác
                var workSheet = excelPackage.Workbook.Worksheets[1];

                var rowCount = data.Count;
                var columnCount = this.InstanceProperties.Count;

                int colIndex = 1;

                int Height = 100;
                int Width = 120;

                for (int row = 0; row < rowCount; row++)
                {
                    var item = data[row];
                    for (int column = 0; column < columnCount; column++)
                    {
                        KeyValuePair<string, string> keyValue = this.InstanceProperties.ElementAt(column);

                        if (row == 0)
                        {
                            workSheet.Cells[this.StartRow, column + this.StartColumn].Value = keyValue.Value;
                        }

                        string propertyName = keyValue.Key;
                        object cellValue = string.Empty;
                        if (!string.IsNullOrEmpty(propertyName))
                        {
                            PropertyInfo property = objectType.GetProperty(propertyName);
                            if (property != null)
                            {
                                if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                                {
                                    cellValue = string.Format("{0:dd/MM/yyyy}", property.GetValue(data[row]));
                                }
                                else
                                {
                                    if (propertyName == "ANH_PATH" || propertyName == "PATH_ANH")
                                    {
                                        cellValue = property.GetValue(data[row]) ?? string.Empty;
                                        if (cellValue == null || cellValue.ToString() == "hinhanh/users.jpg")
                                        {
                                            cellValue = "";
                                        }
                                        else
                                        {
                                            if (File.Exists(UploadFolderPath + "/" + cellValue))
                                            {
                                                Image img = Image.FromFile(UploadFolderPath + "/" + cellValue);
                                                ExcelPicture pic = workSheet.Drawings.AddPicture("hocvien" + row + 1, img);
                                                pic.SetPosition(row + 6, 0, colIndex, 0);
                                                //pic.SetPosition(PixelTop, PixelLeft);
                                                pic.SetSize(Height, Width);
                                                //pic.SetSize(40);

                                                workSheet.Protection.IsProtected = false;
                                                workSheet.Protection.AllowSelectLockedCells = false;
                                            }
                                        }
                                        cellValue = "";
                                    }
                                    else
                                    {
                                        cellValue = property.GetValue(data[row]) ?? string.Empty;
                                    }
                                }
                            }
                            else if (propertyName == "STT")
                            {
                                cellValue = (row + 1);
                            }
                        }
                        workSheet.Cells[this.StartRow + row + 1, column + this.StartColumn].Value = cellValue;
                        workSheet.Row(this.StartRow + row + 1).Height = 100;
                    }
                }
                //định dạng biểu mẫu
                if (formatWorkSheet != null)
                {
                    workSheet = formatWorkSheet(workSheet, this.FileName);
                }

                //tạo border cho tất cả các ô có chứa dữ liệu
                for (int row = this.StartRow; row <= (this.StartRow + data.Count); row++)
                {
                    for (int column = this.StartColumn; column <= (this.StartColumn + this.InstanceProperties.Count) - 1; column++)
                    {
                        workSheet.Cells[row, column].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                    }
                }

                ExcelRange dataRangeRowStart = workSheet.SelectedRange[this.StartRow, this.StartColumn, this.StartRow, (this.StartColumn + this.InstanceProperties.Count) - 1];
                dataRangeRowStart.Style.Font.Bold = true;
                dataRangeRowStart.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                dataRangeRowStart.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                //căn giữa cho toàn bộ sheet

                ExcelRange dataRange = workSheet.SelectedRange[this.StartRow, this.StartColumn, (this.StartRow + data.Count), (this.StartColumn + this.InstanceProperties.Count) - 1];
                //dataRange.Style.Font.Bold = true;
                dataRange.Style.Font.Size = 12;
                dataRange.Style.WrapText = true;
                dataRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                dataRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                if (this.LeftAignColumnIndex != null && this.LeftAignColumnIndex.Count() > 0)
                {
                    foreach (var column in this.LeftAignColumnIndex)
                    {
                        ExcelRange centerRange = workSheet.SelectedRange[this.StartRow, column, (this.StartRow + data.Count), column];
                        centerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    workSheet.Row(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
                if (this.CenterRowIndex != null && this.CenterRowIndex.Count() > 0)
                {
                    foreach (var row in this.CenterRowIndex)
                    {
                        workSheet.Row(row).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }
                }

                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        public Stream CreateExcelFile(List<List<string>> data, Func<ExcelWorksheet, string, ExcelWorksheet> formatWorkSheet)
        {
            Type objectType = typeof(T);
            using (var excelPackage = new ExcelPackage(new MemoryStream()))
            {
                excelPackage.Workbook.Properties.Author = "Author";

                //tạo title cho file excel
                excelPackage.Workbook.Properties.Title = "Title";

                //commment
                excelPackage.Workbook.Properties.Comments = "Comments";

                //add sheet vào fiel excel
                excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //lấy sheet vừa mới tạo để thao tác
                var workSheet = excelPackage.Workbook.Worksheets[1];

                var rowCount = data.Count;
                //var columnCount = this.InstanceProperties.Count;

                for (int row = 0; row < rowCount; row++)
                {
                    var item = data[row];
                    int columnCount = data[row].Count;
                    for (int column = 0; column < columnCount; column++)
                    {
                        workSheet.Cells[row + 1, column + 1].Value = data[row][column].Replace("<br/>", string.Empty);
                        workSheet.Cells[row + 1, column + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                        workSheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        workSheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    }
                }
                //định dạng biểu mẫu
                if (formatWorkSheet != null)
                {
                    workSheet = formatWorkSheet(workSheet, this.FileName);
                }
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
    }

    public class ResponseExportExCel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string PathStore { get; set; }

        public ResponseExportExCel(bool status)
        {
            Status = status;
        }
    }
}