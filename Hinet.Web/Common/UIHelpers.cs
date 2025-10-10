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

		public static string GetDisplayNameAttribute<T>(string name)
		{
			var propInfo = typeof(T).GetProperty(name);
			var displayNameAttribute = propInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
			var displayName = (displayNameAttribute[0] as DisplayNameAttribute).DisplayName;
			return displayName;
		}
	}
}