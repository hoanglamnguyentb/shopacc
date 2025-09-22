using CommonHelper.String;
using Hinet.Service.Constant;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Web.Custom.HtmlHelpers
{
    public static class UIHelpers
    {
        public static MvcHtmlString GridRowCount(this HtmlHelper html, WebGrid grid)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class='grid-row-count'>");
            result.AppendLine(string.Format("{0} bản ghi", grid.Rows.Count));
            result.AppendLine("</div>");
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString ToTextEmpty(this object obj, bool? bold = false)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                if (bold == true)
                {
                    return MvcHtmlString.Create("<span style='font-weight:bold'>" + obj.ToString() + "</span>");
                }
                return MvcHtmlString.Create(obj.ToString());
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static MvcHtmlString ToDateEmpty(this object obj, bool? bold = false)
        {
            if (obj != null)
            {
                if (bold == true)
                {
                    return MvcHtmlString.Create("<span style='font-weight:bold'>" + string.Format("{0:dd/MM/yyyy}", obj) + "</span>");
                }
                return MvcHtmlString.Create(string.Format("{0:dd/MM/yyyy}", obj));
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        /// <summary>
        /// where nhỏ hơn show warning
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bold"></param>
        /// <returns></returns>
        public static MvcHtmlString ToValueLess(this object obj, double valuePress, bool bold = false)
        {
            if (obj != null)
            {
                var value = obj.ToString().ToDoublelOrNull();
                var boldClass = string.Empty;
                if (bold == true)
                {
                    boldClass = "bold";
                }
                if (value.HasValue)
                {
                    if (value <= valuePress)
                    {
                        return MvcHtmlString.Create(string.Format("<span class='text-danger {0}'>{1}</span>", boldClass, obj));
                    }
                    return MvcHtmlString.Create(string.Format("<span class='text-success'>{0}</span>", obj));
                }
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        /// <summary>
        /// where lớn hơn show warning
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bold"></param>
        /// <returns></returns>
        public static MvcHtmlString ToValueLarger(this object obj, double valueLarger, bool bold = false)
        {
            if (obj != null)
            {
                var value = obj.ToString().ToDoublelOrNull();
                var boldClass = string.Empty;
                if (bold == true)
                {
                    boldClass = "bold";
                }
                if (value.HasValue)
                {
                    if (value >= valueLarger)
                    {
                        return MvcHtmlString.Create(string.Format("<span class='text-danger {0}'>{1}</span>", boldClass, obj));
                    }
                    return MvcHtmlString.Create(string.Format("<span class='text-success'>{0}</span>", obj));
                }
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        /// <summary>
        /// where lớn hơn show warning
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bold"></param>
        /// <returns></returns>
        public static MvcHtmlString ToValueLessEqual(this object obj, double valueEqual, bool bold = false)
        {
            if (obj != null)
            {
                var value = obj.ToString().ToDoublelOrNull();
                var boldClass = string.Empty;
                if (bold == true)
                {
                    boldClass = "bold";
                }
                if (value.HasValue)
                {
                    if (value > valueEqual)
                    {
                        return MvcHtmlString.Create(string.Format("<span class='text-danger {0}'>{1}</span>", boldClass, obj));
                    }
                    return MvcHtmlString.Create(string.Format("<span class='text-success'>{0}</span>", obj));
                }
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        /// <summary>
        /// where value khác trong phạm vi show warning
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bold"></param>
        /// <returns></returns>
        public static MvcHtmlString ToValueScope(this object obj, double formValue, double toValue, bool bold = false)
        {
            if (obj != null)
            {
                var value = obj.ToString().ToDoublelOrNull();
                if (value.HasValue)
                {
                    if (value < formValue || value > toValue)
                    {
                        if (bold == true)
                        {
                            return MvcHtmlString.Create("<span style='font-weight:bold' class='text-danger'>" + obj + "</span>");
                        }
                    }
                    return MvcHtmlString.Create("<span class='text-success'>" + obj + "</span>");
                }
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        /// <summary>
        /// Trả về âm tính hoặc dương tính
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bold"></param>
        /// <returns></returns>
        public static MvcHtmlString ToXetNghiemEmpty(this object obj, bool? bold = false)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                string boldClass = string.Empty;
                string classWarning = "";
                string ketQua = "";
                if (obj.ToString().ToLower() == HSSKConstant.AM_TINH.ToLower())
                {
                    classWarning = "text-success";
                    ketQua = "Âm tính";
                }
                else if (obj.ToString().ToLower() == HSSKConstant.DUONG_TINH.ToLower())
                {
                    classWarning = "text-danger";
                    ketQua = "Dương tính";
                }
                if (bold == true)
                {
                    boldClass = "bold";
                }
                return MvcHtmlString.Create(string.Format("<span class='{0} {1}'>{2}</span>", boldClass, classWarning, ketQua));
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static MvcHtmlString GenericChartFromTo(double valuefrom, double valueto, double? value, double size = 3, string bgSuccess = "#dc3545", string bgError = "#ddd")
        {
            if (value.HasValue)
            {
                //tổng số giá trị
                var total = (valueto * size);
                //Phạm vi an toàn
                var safe = ((valueto - valuefrom) * 100) / total;
                var totalFrom = (valuefrom * 100) / total;
                var totalTo = (valueto * 100) / total;

                var chart = "<div class='width100 text-black bg-gray'>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", totalFrom);
                chart += "<span class='float-left'>0</span>";
                if (value < valuefrom)
                {
                    chart += string.Format("<span class='bold font-size16 text-danger'>{0}</span>", value);
                }
                chart += "</div>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", safe);
                chart += string.Format("<span class='float-left text-success'>{0}</span>", valuefrom);
                if (value >= valuefrom && value <= valueto)
                {
                    chart += string.Format("<span class='bold font-size16 text-success'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right text-success'>{0}</span>", valueto);
                chart += "</div>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", (100 - totalTo));
                if (value > valueto)
                {
                    chart += string.Format("<span class='bold font-size16 text-danger'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right'>{0}</span>", total);
                chart += "</div>";
                chart += "</div>";
                return MvcHtmlString.Create(chart);
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static MvcHtmlString GenericChartLess(double valueSafe, double? value, double? size = 3, string bgSuccess = "#dc3545", string bgError = "#ddd")
        {
            if (value.HasValue)
            {
                //tổng số giá trị
                var total = (valueSafe * size);
                //số % hiển thị an toàn
                var totalSafe = (valueSafe * 100) / total;
                var chart = "<div class='width100  text-black bg-gray'>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", totalSafe);
                chart += "<span class='float-left text-success'>0</span>";
                if (value < valueSafe)
                {
                    chart += string.Format("<span class='bold font-size16 text-success'>{0}</span>", value);
                }
                chart += "</div>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", (100 - totalSafe));
                chart += string.Format("<span class='float-left  text-success'>{0}</span>", valueSafe);
                if (value > valueSafe)
                {
                    chart += string.Format("<span class='bold font-size16  text-danger'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right'>{0}</span>", total);
                chart += "</div>";

                chart += "</div>";
                return MvcHtmlString.Create(chart);
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static MvcHtmlString GenericChartLarger(double valueSafe, double? value, double size = 3, string bgSuccess = "#dc3545", string bgError = "#ddd")
        {
            if (value.HasValue)
            {
                //tổng số giá trị
                var total = (valueSafe * size);
                //số % hiển thị an toàn
                var totalSafe = (valueSafe * 100) / total;
                var chart = "<div class='width100 text-black bg-gray'>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", totalSafe);
                chart += "<span class='float-left'>0</span>";
                if (value < valueSafe)
                {
                    chart += string.Format("<span class='bold font-size16 text-danger'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right text-success'>{0}</span>", valueSafe);
                chart += "</div>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center text-success'>", (100 - totalSafe));
                if (value > valueSafe)
                {
                    chart += string.Format("<span class='bold font-size16 text-success'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right text-success'>{0}</span>", total);
                chart += "</div>";

                chart += "</div>";
                return MvcHtmlString.Create(chart);
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static MvcHtmlString GenericChartLessEqual(double valueSafe, double? value, double size, string bgSuccess = "#dc3545", string bgError = "#ddd")
        {
            if (value.HasValue)
            {
                //tổng số giá trị
                var total = (valueSafe * size);
                //số % hiển thị an toàn
                var totalSafe = (valueSafe * 100) / total;
                var chart = "<div class='width100 text-black bg-gray'>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", totalSafe);
                chart += "<span class='float-left text-success'>0</span>";
                if (value <= valueSafe)
                {
                    chart += string.Format("<span class='bold font-size16 text-success'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right text-success'>{0}</span>", valueSafe);
                chart += "</div>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;' class='text-center'>", (100 - totalSafe));
                if (value > valueSafe)
                {
                    chart += string.Format("<span class='bold font-size16 text-danger'>{0}</span>", value);
                }
                chart += string.Format("<span class='float-right'>{0}</span>", total);
                chart += "</div>";

                chart += "</div>";
                return MvcHtmlString.Create(chart);
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static MvcHtmlString GenericLine(double? value, double total = 1000, string bgSuccess = "#dc3545", string bgError = "#ddd")
        {
            if (value.HasValue)
            {
                var totalValue = (value * 100) / total;
                var chart = "<div class='width100 text-black bg-gray'>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;background:{1}' class='text-center'>", totalValue, bgSuccess);
                chart += "<span class='float-left'>0</span>";
                chart += string.Format("<span class='bold font-size16 text-white'>{0}</span>", value);
                chart += "</div>";

                chart += string.Format("<div style='width:{0}%; float:left;height:25px;background:{1}' class='text-center'>", (100 - totalValue), bgError);
                chart += string.Format("<span class='float-right'>{0}</span>", total);
                chart += "</div>";

                chart += "</div>";
                return MvcHtmlString.Create(chart);
            }
            return MvcHtmlString.Create("<span class='italic'>Chưa có thông tin</span>");
        }

        public static string GenericColor(string value, object obj)
        {
            if (obj == null)
            {
                return value;
            }
            if (Convert.ToBoolean(obj))
            {
                return "<span class='text-danger'>" + value + "</span>";
            }
            return value;
        }

        public static string GenericLabelLess(double? value, double safe)
        {
            if (value.HasValue)
            {
                if (value < safe)
                {
                    return "text-success";
                }
                else
                {
                    return "text-danger";
                }
            }
            return string.Empty;
        }

        public static string GenericLabelLarger(double? value, double safe)
        {
            if (value.HasValue)
            {
                if (value > safe)
                {
                    return "text-success";
                }
                else
                {
                    return "text-danger";
                }
            }
            return string.Empty;
        }

        public static string GenericLabelFromTo(double? value, double from, double to)
        {
            if (value.HasValue)
            {
                if (value >= from && value <= to)
                {
                    return "text-success";
                }
                else
                {
                    return "text-danger";
                }
            }
            return string.Empty;
        }

        public static string GenericLabelLessEqual(double? value, double safe)
        {
            if (value.HasValue)
            {
                if (value <= safe)
                {
                    return "text-success";
                }
                else
                {
                    return "text-danger";
                }
            }
            return string.Empty;
        }

        public static string GetDisplayNameAttribute<T>(string name)
        {
            var propInfo = typeof(T).GetProperty(name);
            var displayNameAttribute = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            var displayName = (displayNameAttribute[0] as DisplayNameAttribute).DisplayName;
            return displayName;
        }
    }
}