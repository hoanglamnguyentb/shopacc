namespace Hinet.API2.Models
{
    public class GetBySCTVM
    {
        public string Key { get; set; }
        public int? ManageType { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}