using Hinet.Service.Common;

namespace Hinet.Service.DM_DulieuDanhmucService.DTO
{
    public class ShowValueTableSVM : SearchBase
    {
        public string TableName { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public string GiaTriNhapFilter { get; set; }
        public string GiaTriHienThiFilter { get; set; }
    }
}