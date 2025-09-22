using System.Collections.Generic;

namespace Hinet.API2.Core
{
    public class PageListResultAPIBO<T> where T : class
    {
        public List<T> ListItem { get; set; }
        public int Count { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public string Message { get; set; }
        public bool? Status { get; set; }
    }
}