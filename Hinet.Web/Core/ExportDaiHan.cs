using Hinet.Service.Common;
using Hinet.Service.HD_QLHopDongService.Dto;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Hinet.Web.Core
{
    public class ExportDaiHan
    {
        public static byte[] ExportdaiHan(PageListResultBO<HD_QLHopDongDto> dataExport)
        {
            try
            {
                var templateName = Path.Combine(HostingEnvironment.MapPath("/"), WebConfigurationManager.AppSettings["ExportTatCaNhanSu"]);

                using (ExcelPackage MyExcel = new ExcelPackage(new FileInfo(templateName)))
                {
                    var index = 3;
                    var countItem = 0;
                    if (dataExport.ListItem != null && dataExport.ListItem.Any())
                    {
                        ExcelWorksheet MyWorksheet = MyExcel.Workbook.Worksheets.FirstOrDefault();

                        MyWorksheet.Cells["A1"].Value = "DANH SÁCH HỢP ĐỒNG KHÔNG XÁC ĐỊNH THỜI HẠN";

                        foreach (var item in dataExport.ListItem)
                        {
                            ExcelRange styleHead = MyWorksheet.SelectedRange[index + countItem, 1, index + countItem, 20];
                            //styleHead.Merge = true;
                            styleHead.Style.Font.Size = 13;
                            styleHead.Style.Font.Name = "Times New Roman";
                            //styleHead.Style.Font.Bold = true;
                            styleHead.Style.WrapText = true;
                            styleHead.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            styleHead.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            MyWorksheet.Cells[index + countItem, 1].Value = countItem + 1;
                            MyWorksheet.Cells[index + countItem, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                            MyWorksheet.Cells[index + countItem, 2].Value = item.MaHopDong;
                            MyWorksheet.Cells[index + countItem, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                            MyWorksheet.Cells[index + countItem, 3].Value = item.StatusName;
                            MyWorksheet.Cells[index + countItem, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                            if (item.BieuMauThanhPhanInfo != null && item.BieuMauThanhPhanInfo.Name != null)
                            {
                                MyWorksheet.Cells[index + countItem, 4].Value = item.BieuMauThanhPhanInfo.Name;

                            }
                            MyWorksheet.Cells[index + countItem, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                            if (item.UserInfo != null && item.UserInfo.HoTen != null)
                            {
                                MyWorksheet.Cells[index + countItem, 5].Value = item.UserInfo.HoTen.ToUpper();
                            }
                            MyWorksheet.Cells[index + countItem, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                            MyWorksheet.Cells[index + countItem, 6].Value = string.Format("{0: dd/MM/yyyy}", item.TimeStart) + "-" + string.Format("{0: MM/yyyy}", item.TimeEnd);
                            MyWorksheet.Cells[index + countItem, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                            MyWorksheet.Cells[index + countItem, 7].Value = string.Format("{0: dd/MM/yyyy}", item.TimeMakeHopDong);
                            MyWorksheet.Cells[index + countItem, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                            if (item.DepartmentInfo != null && item.DepartmentInfo.Name != null)
                            {
                                MyWorksheet.Cells[index + countItem, 8].Value = item.DepartmentInfo.Name;
                            }
                            else
                            {
                                MyWorksheet.Cells[index + countItem, 8].Value = "";
                            }
                            if (item.ChucVuInfoName != null)
                            {
                                MyWorksheet.Cells[index + countItem, 9].Value = item.ChucVuInfoName;
                            }
                            if (item.ChucVuInfoName == null)
                            {
                                MyWorksheet.Cells[index + countItem, 9].Value = "";
                            }
                            if (item.BacLuong != null)
                            {
                                MyWorksheet.Cells[index + countItem, 10].Value = item.UserInfo != null ? item.BacLuong.ToString() : "";
                                MyWorksheet.Cells[index + countItem, 11].Value = item.UserInfo != null ? item.HeSoLuong.ToString() : "";

                            }
                            else
                            {
                                MyWorksheet.Cells[index + countItem, 10].Value = "";
                                MyWorksheet.Cells[index + countItem, 11].Value = "";

                            }
                            MyWorksheet.Cells[index + countItem, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                            MyWorksheet.Cells[index + countItem, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                            MyWorksheet.Cells[index + countItem, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                            MyWorksheet.Cells[index + countItem, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                            countItem = countItem + 1;
                        }
                    }
                    return MyExcel.GetAsByteArray();
                }
            }
            catch
            {
                return new byte[] { };
            }
        }
    }
}
