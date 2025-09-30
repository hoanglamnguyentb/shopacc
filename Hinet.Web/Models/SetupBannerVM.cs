using System;

namespace Hinet.Web.Models
{
    public class SetupBannerVM
    {
        public long Id { get; set; }
        public string CodePage { get; set; }
        public DateTime? TimeShow { get; set; }
        public bool? IsShow { get; set; }
        public long IdBanner { get; set; }
        public int OrderNumber { get; set; }
    }
}