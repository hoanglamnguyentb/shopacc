using Hinet.Model.Entities;

namespace Hinet.Web.Models
{
    public class ViewTaiLieuVM
    {
        public TaiLieuDinhKem Attachment { set; get; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public bool IsImage { get; set; }
        public bool CanShowPDF { get; set; }
        public int type { get; set; }
    }
}