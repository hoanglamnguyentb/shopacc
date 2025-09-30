namespace Hinet.Service.Common
{
    public class SearchBase
    {
        public string sortQuery { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 20;
    }
}