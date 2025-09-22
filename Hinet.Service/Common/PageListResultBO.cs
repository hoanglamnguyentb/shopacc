using CommonHelper.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Hinet.Service.Common
{
    public class PageListResultBO<T> where T : class
    {
        public List<T> ListItem { get; set; }

        public List<IGrouping<string,T>> DuongThus { get; set; }
        private string _GroupDataField;
        private string _GroupDisplayField;

        private string _GroupDataFields;
        private string _GroupDisplayFields;

        public string GroupDataField
        {
            get
            {
                if (string.IsNullOrEmpty(_GroupDataField))
                {
                    _GroupDataField = "Huyen";
                }
                return _GroupDataField;
            }
            set { _GroupDataField = value; }
        }

        public string GroupDisplayField
        {
            get
            {
                if (string.IsNullOrEmpty(_GroupDisplayField))
                {
                    _GroupDisplayField = "TenHuyen";
                }
                return _GroupDisplayField;
            }
            set { _GroupDisplayField = value; }
        }

        public string GroupDataFields
        {
            get
            {
                if (string.IsNullOrEmpty(_GroupDataFields))
                {
                    _GroupDataFields = "Huyen";
                }
                return _GroupDataFields;
            }
            set { _GroupDataFields = value; }
        }

        public string GroupDisplayFields
        {
            get
            {
                if (string.IsNullOrEmpty(_GroupDisplayFields))
                {
                    _GroupDisplayFields = "TenHuyen";
                }
                return _GroupDisplayFields;
            }
            set { _GroupDisplayFields = value; }
        }

        public List<ArmChartData> GroupedItem { get; set; }

        public void SetGroupedItem(List<T> ListItemNoPaging)
        {
            Type type = typeof(T);
            if (type.GetProperty(GroupDataField) != null && type.GetProperty(GroupDisplayField) != null)
            {
                this.GroupedItem = ListItemNoPaging
                .GroupBy(p => p.GetType().GetProperty(GroupDataField).GetValue(p, null))
                .Select(p => new ArmChartData()
                {
                    GroupDisplayField = p.Select(sp => type.GetProperty(GroupDisplayField).GetValue(sp)).FirstOrDefault().ToString(),
                    Count = p.Count()
                }).ToList();
            }
            else
            {
                this.GroupedItem = new List<ArmChartData>();
            }
        }

        public void SetGroupedItemNew(IQueryable<T> source)
        {
            Type type = typeof(T);
            if (type.GetProperty(GroupDisplayField) != null && type.GetProperty(GroupDisplayField) != null)
            {
                var propertyInfo = type.GetProperty(GroupDisplayField);
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

                // Create a lambda expression to extract the field value
                Expression propertyAccess = Expression.Property(parameter, propertyInfo);
                var lambda = Expression.Lambda(propertyAccess, parameter);

                var groupByExpression = Expression.Call(
                    typeof(Queryable),
                    "GroupBy",
                    new[] { typeof(T), propertyInfo.PropertyType },
                    source.Expression,
                    lambda
                );
                var grouped = source.Provider.CreateQuery<IGrouping<object, T>>(groupByExpression);
                this.GroupedItem = grouped.Select(p => new ArmChartData
                {
                    GroupDisplayField = p.Key.ToString(),
                    Count = p.Count()
                }).ToList();
            }
            else
            {
                this.GroupedItem = new List<ArmChartData>();
            }
        }

        public Type GetItemType()
        {
            return typeof(T);
        }

        public Dictionary<string, string> ExportProperties { get; set; }
        public string ExportFileName { get; set; }

        public MemoryStream GetExportBuffer()
        {
            EPPlusSupplier<T> supplier = new EPPlusSupplier<T>();
            supplier.properties = ExportProperties;
            supplier.startColumn = 1;
            supplier.startRow = 2;
            supplier.fileName = "Danh sách đối tượng";

            var stream = supplier.CreateExcelFile(this.ListItem, null);
            var buffer = stream as MemoryStream;
            return buffer;
        }

        public int Count { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }

    public class ArmChartData
    {
        public string GroupDisplayField { get; set; }
        public int Count { get; set; }
    }

    public class PageListResultBOV2<T> where T : class
    {
        public List<T> data { get; set; }
        public int recordsTotal { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int recordsFiltered { get; set; }
        public int draw { get; set; }
        public string error { get; set; }
    }

    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }

        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}