using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace CommonHelper.ObjectExtention
{
    public static class PropertyInfoExtension
    {
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).FirstOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (attr == null)
            {
                return memberInfo.Name;
            }

            return attr.DisplayName;
        }

        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        public static List<SelectListItem> GetListItems<T>(string selectedItem)
        {
            var result = new List<SelectListItem>();
            var lstProp = typeof(T).GetProperties();
            foreach (var item in lstProp)
            {
                var DisplayName = item.GetAttribute<DisplayNameAttribute>(false);

                var objItem = new SelectListItem();
                if (DisplayName != null && !string.IsNullOrEmpty(DisplayName.DisplayName))
                {
                    objItem.Text = DisplayName.DisplayName;
                }
                else
                {
                    objItem.Text = item.Name;
                }
                if (objItem.Text == selectedItem)
                {
                    objItem.Selected = true;
                }
                objItem.Value = item.Name;
                result.Add(objItem);
            }
            return result;
        }

        public static object GetValueOfObject<T>(string stringKey, T obj)
        {
            if (!string.IsNullOrEmpty(stringKey))
            {
                var prop = typeof(T).GetProperty(stringKey);
                if (prop != null && obj != null)
                {
                    var objResult = prop.GetValue(obj);
                    if (objResult != null)
                    {
                        return objResult;
                    }
                }
            }

            return null;
        }
    }
}