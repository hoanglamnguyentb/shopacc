using CommonHelper.String;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace Hinet.API2.Providers
{
    public class MapperFormDataToObjProvider
    {
        public static T Map<T>(NameValueCollection dataForm, T output)
        {
            var typeInfo = typeof(T);
            if (dataForm != null && dataForm.AllKeys.Any())
            {
                foreach (var item in dataForm.AllKeys)
                {
                    var property = typeInfo.GetProperty(item);
                    var value = dataForm.GetValues(item).FirstOrDefault();
                    if (property != null && !string.IsNullOrEmpty(value))
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(output, value);
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            property.SetValue(output, value.ToDateTime().Value);
                        }
                        else if (property.PropertyType == typeof(DateTime?))
                        {
                            property.SetValue(output, value.ToDateTime().Value);
                        }
                        else if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(output, value.ToIntOrZero());
                        }
                        else if (property.PropertyType == typeof(int?))
                        {
                            property.SetValue(output, value.ToIntOrNULL());
                        }
                        else if (property.PropertyType == typeof(long))
                        {
                            property.SetValue(output, value.ToLongOrZero());
                        }
                        else if (property.PropertyType == typeof(long?))
                        {
                            property.SetValue(output, value.ToLongOrNULL());
                        }
                    }
                }
            }
            return output;
        }
    }
}