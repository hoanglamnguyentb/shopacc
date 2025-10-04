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
        public Type GetItemType()
        {
            return typeof(T);
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