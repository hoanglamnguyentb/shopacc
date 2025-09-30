using CommonHelper.ObjectExtention;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CommonHelper.Excel
{
    public class ExportExcelV2Helper
    {
        /// <summary>
        /// Chuyển đổi kiểu Nullable
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        ///
        private static int DefaultCulumnWidth = 20;

        public static Type GetTypeExcelSupport(PropertyInfo propertyInfo)
        {
            var typeObj = propertyInfo.PropertyType;
            if (typeObj.IsGenericType && typeObj.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(typeObj);
            }

            return typeObj;
        }

        public static byte[] Export<T>(List<T> Data, List<Tuple<string, string, bool>> lstTextCustom = null) where T : class
        {
            try
            {
                var FileData = new DataTable();

                var memoryStream = new MemoryStream();
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    DataTable Dt = new DataTable();
                    var listProperty = typeof(T).GetProperties();

                    for (int i = 0; i < listProperty.Count(); i++)
                    {
                        var displayNameObj = listProperty[i].GetAttribute<DisplayNameAttribute>(false);
                        var name = displayNameObj != null ? displayNameObj.DisplayName : listProperty[i].Name;
                        var col = new DataColumn(name, GetTypeExcelSupport(listProperty[i]));

                        var widthAttr = listProperty[i].GetAttribute<CustomExportAttribute>(false);
                        var width = widthAttr != null ? widthAttr.Width : 0;
                        if (width > 0)
                        {
                            worksheet.Column(i + 1).Width = width;
                        }
                        else
                        {
                            worksheet.Column(i + 1).Width = DefaultCulumnWidth;
                        }

                        Dt.Columns.Add(col);
                    }

                    var dropdownAdd = new List<Dropdowm>();
                    var index = 0;
                    foreach (var dataitem in Data)
                    {
                        DataRow row = Dt.NewRow();
                        for (int i = 0; i < listProperty.Count(); i++)
                        {
                            var val = listProperty[i].GetValue(dataitem);
                            if (val != null)
                            {
                                if (listProperty[i].PropertyType == typeof(DateTime))
                                {
                                    // If the property is of type DateTime, format it as dd/MM/yyyy
                                    row[i] = string.Format("{0:dd/MM/yyyy}", ((DateTime)val));
                                }
                                else
                                {
                                    row[i] = val;
                                }
                            }
                        }
                        Dt.Rows.Add(row);
                        index++;
                    }

                    //worksheet.Cells.AutoFitColumns();
                    worksheet.DefaultColWidth = DefaultCulumnWidth;
                    //worksheet.DefaultRowHeight = 20;
                    worksheet.Cells.Style.WrapText = true;
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    //worksheet.Cells.Style.WrapText = true;
                    worksheet.Cells["A5"].LoadFromDataTable(Dt, true, TableStyles.Medium13);
                    if (lstTextCustom != null)
                    {
                        foreach (var item in lstTextCustom)
                        {
                            worksheet.Cells[item.Item1].Value = item.Item2;
                            if (item.Item3)
                            {
                                worksheet.Cells[item.Item1].Style.Font.Bold = true;
                            }
                        }
                    }
                    return excelPackage.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] ExportString(List<string> header, List<List<string>> Data)
        {
            try
            {
                var FileData = new DataTable();

                var memoryStream = new MemoryStream();
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    DataTable Dt = new DataTable();

                    for (int i = 0; i < header.Count(); i++)
                    {
                        var displayNameObj = header[i];

                        Dt.Columns.Add(displayNameObj, typeof(string));
                    }

                    foreach (var dataitem in Data)
                    {
                        DataRow row = Dt.NewRow();
                        for (int i = 0; i < header.Count(); i++)
                        {
                            var val = dataitem[i];
                            if (val != null)
                            {
                                row[i] = val;
                            }
                        }
                        Dt.Rows.Add(row);
                    }
                    //worksheet.Cells.AutoFitColumns();
                    worksheet.DefaultColWidth = 20;
                    worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.None);

                    return excelPackage.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public class Dropdowm
        {
            public string Name { get; set; }
            public List<string> Data { get; set; }
        }
    }
}