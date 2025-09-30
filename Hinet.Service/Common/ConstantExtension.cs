using CommonHelper.ObjectExtention;
using CommonHelper.String;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Hinet.Service.Common
{
    public static class ConstantExtension
    {
        public static CommonHelper.String.HTML.ShowStatusDto GetStatusInfo2<TConst>(string value)
        {
            var rs = new CommonHelper.String.HTML.ShowStatusDto();
            rs.Code = value;

            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        if (val == value)
                        {
                            rs.Name = name;
                        }

                        var getObjColor = item.GetAttribute<ColorAttribute>(false);
                        if (val == value && getObjColor != null)
                        {
                            rs.Color = getObjColor.Color;
                            rs.BgColor = getObjColor.BgColor;
                            rs.Icon = getObjColor.Icon;
                        }
                    }
                }
            }
            return rs;
        }

        public static List<Tuple<string, string>> GetTupleNameAndValue<TConst>()
        {
            var result = new List<Tuple<string, string>>();
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;

                        result.Add(new Tuple<string, string>(name, val));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy danh sách dropdownlist constant theo class
        /// </summary>
        /// <typeparam name="TConst"></typeparam>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetDropdownData<TConst>(string selectedItem = null)
        {
            var result = new List<SelectListItem>();
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        result.Add(new SelectListItem()
                        {
                            Text = name,
                            Value = val,
                            Selected = !string.IsNullOrEmpty(selectedItem) ? val == selectedItem : false
                        });
                    }
                }
            }
            return result;
        }
        public static List<SelectListItem> GetDropdownDataMulti<TConst>(string selectedItem = null)
        {
            var result = new List<SelectListItem>();
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        result.Add(new SelectListItem()
                        {
                            Text = name,
                            Value = val,
                            Selected = !string.IsNullOrEmpty(selectedItem) ? selectedItem.Split(',').Contains(val) : false
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Lấy danh sách value constant theo class
        /// </summary>
        /// <typeparam name="TConst"></typeparam>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public static List<string> GetListData<TConst>()
        {
            var result = new List<string>();
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        result.Add(val);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy Tên của constant đề hiển thị
        /// </summary>
        /// <typeparam name="TConst"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>

        public static string GetName<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        if (val == value)
                        {
                            return name;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string GetVariable<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        if (name == value)
                        {
                            return val;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static List<ConstantObj> GetConstants<TConst>()
        {
            var listProperty = typeof(TConst).GetProperties();
            List<ConstantObj> lstResult = new List<ConstantObj>();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var obj = new ConstantObj();
                        obj.Value = item.GetValue(null).ToString();
                        obj.Name = item.GetAttribute<DisplayNameAttribute>(false).DisplayName;
                        obj.Color = item.GetAttribute<ColorAttribute>(false).Color;
                        lstResult.Add(obj);
                    }
                }
            }
            return lstResult;
        }

        public static string GetAtrr<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<Attribute>(true).ToString();
                        if (val == value)
                        {
                            return name;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string GetNameBool<TConst>(bool value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString().ToBoolOrFalse();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        if (val == value)
                        {
                            return name;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string GetColor<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var getObj = item.GetAttribute<ColorAttribute>(false);
                        if (val == value && getObj != null)
                        {
                            return getObj.Color;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static string GetBackgroundColor<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var getObj = item.GetAttribute<ColorAttribute>(false);
                        if (val == value && getObj != null)
                        {
                            return getObj.BgColor;
                        }
                    }
                }
            }
            return string.Empty;
        }

       

        public static string GetDisplayNameById<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        if (val == value)
                        {
                            return name;
                        }
                    }
                }
            }
            return string.Empty;
        }
        public static string GetDisplayNameByIdMulti<TConst>(string value)
        {
            List<string> listValue = value.Split(',').ToList();
            List<string> listName = new List<string>();
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        foreach (var x in listValue)
                        {
                            var val = item.GetValue(null).ToString();
                            var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                            if (val == x)
                            {
                                listName.Add(name);
                            }
                        }
                        
                    }
                }
            }
            return string.Join(",", listName);
        }


        public static string GetConstantName<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var getObj = item.GetMethod.Name;
                        if (val == value && getObj != null)
                        {
                            getObj = getObj.Replace("get_", "");
                            return getObj;
                        }
                    }
                }
            }
            return string.Empty;
        }

        internal static object GetName<T>()
        {
            throw new NotImplementedException();
        }

        public static ShowStatusDto GetStatusInfo<TConst>(string value)
        {
            var rs = new ShowStatusDto();
            rs.Code = value;

            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var name = item.GetAttribute<DisplayNameAttribute>(true).DisplayName;
                        if (val == value)
                        {
                            rs.Name = name;
                        }

                        var getObjColor = item.GetAttribute<ColorAttribute>(false);
                        if (val == value && getObjColor != null)
                        {
                            rs.Color = getObjColor.Color;
                            rs.BgColor = getObjColor.BgColor;
                            rs.Icon = getObjColor.Icon;
                        }
                    }
                }
            }
            return rs;
        }

        public static string GetIcon<TConst>(string value)
        {
            var listProperty = typeof(TConst).GetProperties();
            if (listProperty != null)
            {
                foreach (var item in listProperty)
                {
                    if (item.GetGetMethod().IsStatic)
                    {
                        var val = item.GetValue(null).ToString();
                        var getObj = item.GetAttribute<ColorAttribute>(false);
                        if (val == value && getObj != null)
                        {
                            return getObj.Icon;
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Lấy value theo tên
        /// </summary>
        /// <typeparam name="TConst"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetValueConstant<TConst>(string prop)
        {
            var propInfo = typeof(TConst).GetProperty(prop);
            if (propInfo != null)
            {
                return propInfo.GetValue(null);
            }

            return null;
        }
    }
}