using Hinet.Service.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Hinet.Web.Controllers
{
    public class TableExportController : Controller
    {
        private readonly log4net.ILog _logger;

        public TableExportController(log4net.ILog logger)
        {
            _logger = logger;
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult SaveModel(TableExportVM data, string name)
        {
            var result = new JsonResultBO(true, "Thành công");
            SessionManager.SetValue("TableExport", data);
            return Json(result);
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult SaveModel2(List<TableExportVM> data)
        {
            var result = new JsonResultBO(true, "Thành công");
            // Cập nhật lại vào Session
            SessionManager.SetValue("TableExport2", data);

            return Json(result);
        }



        [HttpGet]
        [AllowAnonymous]
        public FileResult Export()
        {
            var table = SessionManager.GetValue<TableExportVM>("TableExport");
            SessionManager.Remove("TableExport");
            if (table == null) return null;
            using (var memoryStream = new MemoryStream())
            {
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var ws = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    var merges = new List<Merge>();
                    var colWidth = new HashSet<int>();
                    var rowHeight = new HashSet<int>();
                    try
                    {
                        for (int i = 0; i < table.TRows.Count; i++)
                        {
                            var trow = table.TRows[i];
                            var colMerge = 0;
                            var mergeCount = 0;
                            var checkNextCol = new HashSet<int>();
                            for (int j = 0; j < trow.TDatas.Count; j++)
                            {
                                var col = j + colMerge + 1 + mergeCount;
                                var row = i + 1;

                                var check = merges.FirstOrDefault(merge => merge.Col == col && merge.Row < row && merge.Row + merge.RowSpan > row && !checkNextCol.Contains(col));
                                if (check != null)
                                {
                                    mergeCount += check.ColSpan;
                                    col += check.ColSpan;

                                    var checkNext = merges.FirstOrDefault(merge => merge.Col == col && merge.Row < row && merge.Row + merge.RowSpan > row && !checkNextCol.Contains(col));
                                    while (checkNext != null)
                                    {
                                        mergeCount += checkNext.ColSpan;
                                        col += checkNext.ColSpan;
                                        checkNext = merges.FirstOrDefault(merge => merge.Col == col && merge.Row < row && merge.Row + merge.RowSpan > row && !checkNextCol.Contains(col));
                                    }

                                }

                                var tdata = trow.TDatas[j];


                                var mergeColTo = col;
                                var mergeRowTo = row;
                                if (tdata.ColSpan > 1)
                                {
                                    colMerge += tdata.ColSpan - 1;
                                    mergeColTo = col + tdata.ColSpan - 1;
                                }
                                if (tdata.RowSpan > 1)
                                {
                                    merges.Add(new Merge(col, row, tdata.RowSpan, tdata.ColSpan));
                                    mergeRowTo = row + tdata.RowSpan - 1;
                                }
                                var cell = ws.Cells[row, col, mergeRowTo, mergeColTo];
                                cell.Merge = mergeColTo > col || mergeRowTo > row;

                                cell.Style.VerticalAlignment = (ExcelVerticalAlignment)tdata.VerticalAlign;
                                cell.Style.HorizontalAlignment = (ExcelHorizontalAlignment)tdata.TextAlign;
                                cell.Style.Indent = 1;
                                cell.Style.Numberformat.Format = "@";
                                FillText(cell, tdata.TText);

                                if (false && !string.IsNullOrEmpty(tdata.BorderColor))
                                {
                                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, ColorTranslator.FromHtml(tdata.BorderColor));
                                }
                                else
                                {
                                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                }

                                if (!string.IsNullOrEmpty(tdata.Background))
                                {
                                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(tdata.Background));
                                }
                                if (tdata.Width > 0 && !colWidth.Contains(col) && tdata.ColSpan == 1)
                                {
                                    colWidth.Add(col);
                                    ws.Column(col).Width = tdata.Width / 5;
                                }
                                if (tdata.Height > 0 && !rowHeight.Contains(row) && tdata.RowSpan == 1)
                                {
                                    rowHeight.Add(row);
                                    ws.Row(row).Height = tdata.Height * 1.5;
                                }
                                ws.Column(col).Style.WrapText = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return File(excelPackage.GetAsByteArray(), "application/octet-stream", $"{table.Name ?? "Export"}.xlsx");
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public FileResult Export2()
        {
            var tableCollection = SessionManager.GetValue<List<TableExportVM>>("TableExport2"); // Nhiều bảng
            SessionManager.Remove("TableExport2");
            if (tableCollection == null || tableCollection.Count == 0) return null;

            using (var memoryStream = new MemoryStream())
            {
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var ws = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    var merges = new List<Merge>();
                    var colWidth = new HashSet<int>();
                    var rowHeight = new HashSet<int>();
                    int currentRow = 1; // Bắt đầu từ hàng đầu tiên

                    try
                    {
                        foreach (var table in tableCollection) // Lặp qua từng bảng
                        {
                            for (int i = 0; i < table.TRows.Count; i++)
                            {
                                var trow = table.TRows[i];
                                var colMerge = 0;
                                var mergeCount = 0;
                                var checkNextCol = new HashSet<int>();
                                for (int j = 0; j < trow.TDatas.Count; j++)
                                {
                                    var col = j + colMerge + 1 + mergeCount;
                                    var row = currentRow + i;

                                    var check = merges.FirstOrDefault(merge => merge.Col == col && merge.Row < row && merge.Row + merge.RowSpan > row && !checkNextCol.Contains(col));
                                    if (check != null)
                                    {
                                        mergeCount += check.ColSpan;
                                        col += check.ColSpan;

                                        var checkNext = merges.FirstOrDefault(merge => merge.Col == col && merge.Row < row && merge.Row + merge.RowSpan > row && !checkNextCol.Contains(col));
                                        while (checkNext != null)
                                        {
                                            mergeCount += checkNext.ColSpan;
                                            col += checkNext.ColSpan;
                                            checkNext = merges.FirstOrDefault(merge => merge.Col == col && merge.Row < row && merge.Row + merge.RowSpan > row && !checkNextCol.Contains(col));
                                        }
                                    }

                                    var tdata = trow.TDatas[j];

                                    var mergeColTo = col;
                                    var mergeRowTo = row;
                                    if (tdata.ColSpan > 1)
                                    {
                                        colMerge += tdata.ColSpan - 1;
                                        mergeColTo = col + tdata.ColSpan - 1;
                                    }
                                    if (tdata.RowSpan > 1)
                                    {
                                        merges.Add(new Merge(col, row, tdata.RowSpan, tdata.ColSpan));
                                        mergeRowTo = row + tdata.RowSpan - 1;
                                    }
                                    var cell = ws.Cells[row, col, mergeRowTo, mergeColTo];
                                    cell.Merge = mergeColTo > col || mergeRowTo > row;

                                    cell.Style.VerticalAlignment = (ExcelVerticalAlignment)tdata.VerticalAlign;
                                    cell.Style.HorizontalAlignment = (ExcelHorizontalAlignment)tdata.TextAlign;
                                    cell.Style.Indent = 1;
                                    cell.Style.Numberformat.Format = "@";
                                    FillText(cell, tdata.TText);

                                    if (!string.IsNullOrEmpty(tdata.BorderColor))
                                    {
                                        cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, ColorTranslator.FromHtml(tdata.BorderColor));
                                    }
                                    else
                                    {
                                        cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    }

                                    if (!string.IsNullOrEmpty(tdata.Background))
                                    {
                                        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(tdata.Background));
                                    }
                                    if (tdata.Width > 0 && !colWidth.Contains(col) && tdata.ColSpan == 1)
                                    {
                                        colWidth.Add(col);
                                        ws.Column(col).Width = tdata.Width / 5;
                                    }
                                    if (tdata.Height > 0 && !rowHeight.Contains(row) && tdata.RowSpan == 1)
                                    {
                                        rowHeight.Add(row);
                                        ws.Row(row).Height = tdata.Height * 1.5;
                                    }
                                    ws.Column(col).Style.WrapText = true;
                                }
                            }

                            // Cập nhật vị trí hàng để bắt đầu bảng tiếp theo
                            currentRow += table.TRows.Count + 2; // Cách 2 dòng giữa các bảng
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nếu cần
                    }
                    return File(excelPackage.GetAsByteArray(), "application/octet-stream", $"Export.xlsx");
                }
            }
        }

        private void FillText(ExcelRange cell, TText text)
        {
            if (text != null && text.Display != "none")
            {
                if (!string.IsNullOrEmpty(text.Text))
                {
                    var content = text.Text;
                    if (text.Display == "block")
                    {
                        cell.RichText.Add("\n");
                    }
                    else if (cell.RichText.Any() && cell.RichText.Last().Text != "\n")
                    {
                        content = " " + content;
                    }
                    var ert = cell.RichText.Add(content);
                    if (text.FontSize > 0)
                    {
                        ert.Size = (float)text.FontSize - 2;
                    }
                    if (text.Bold)
                    {
                        ert.Bold = text.Bold;
                    }
                    if (!string.IsNullOrEmpty(text.FontFamily))
                    {
                        ert.FontName = text.FontFamily;
                    }
                    if (!string.IsNullOrEmpty(text.Color))
                    {
                        ert.Color = System.Drawing.ColorTranslator.FromHtml(text.Color);
                    }
                }

                if (text.Children != null)
                {
                    foreach (var item in text.Children)
                    {
                        FillText(cell, item);
                    }
                }
                if (text.Display == "block")
                {
                    cell.RichText.Add("\n");
                }
            }
        }

        #region models
        public class TableExportVM
        {
            public string Name { get; set; }
            public List<TRow> TRows { get; set; } = new List<TRow>();
        }


        public class TRow
        {
            public List<TData> TDatas { get; set; } = new List<TData>();
        }

        public class TData
        {
            public TText TText { get; set; }
            public string BorderColor { get; set; }
            public string Background { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public int ColSpan { get; set; } = 1;
            public int RowSpan { get; set; } = 1;
            public int TextAlign { get; set; }
            public int VerticalAlign { get; set; }
        }

        public class TText
        {
            public string Text { get; set; }
            public string Color { get; set; }
            public double FontSize { get; set; }
            public bool Bold { get; set; }
            public string FontFamily { get; set; }
            public string Display { get; set; }
            public List<TText> Children { get; set; }
        }

        private class Merge
        {
            public Merge(int col, int row, int rowspan, int colspan)
            {
                Col = col;
                Row = row;
                RowSpan = rowspan;
                ColSpan = colspan;
            }
            public int Col { get; set; }
            public int Row { get; set; }
            public int RowSpan { get; set; }
            public int ColSpan { get; set; }
        }
        #endregion

    }
}