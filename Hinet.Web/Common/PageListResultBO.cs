using System.Collections.Generic;

namespace Hinet.Web.Common
{
    public class PageListResultBO<T> where T : class
    {
        public List<T> ListItem { get; set; }
        public int Count { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}