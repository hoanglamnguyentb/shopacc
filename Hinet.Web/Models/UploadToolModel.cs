using Hinet.Model.Entities;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Models
{
    public class UploadToolModel
    {
        public List<TaiLieuDinhKem> LstTaiLieu { get; set; }
        public bool IsModify { get; set; }
        public string LoaiTaiLieu { get; set; }
        public List<SelectListItem> LoaiTaiLieu_DDL { get; set; }
    }

    public class UploadFileMultiVM
    {
        public List<HttpPostedFileBase> TaiLieuDinhKemTool { get; set; }
        public List<string> name_TaiLieuDinhKemTool { get; set; }
    }
}