using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hinet.Web.Areas.SiteConfigArea.Models
{
    public class CreateVM
    {
		public string Description { get; set; }
		public string Keywords { get; set; }
		public string OgTitle { get; set; }
		public string OgUrl { get; set; }
		public string OgDescription { get; set; }
		public string OgImage { get; set; }
		public string SiteTitle { get; set; }
		public string Favicon { get; set; }
		public string Logo { get; set; }
        public bool? KichHoat { get; set; }
        public HttpPostedFileBase FileOgImage { get; set; }
        public HttpPostedFileBase FileFavicon { get; set; }
        public HttpPostedFileBase FileLogo { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string PrimaryHoverColor { get; set; }
        public string TextTitleColor { get; set; }
        public string TextColor { get; set; }
        public string LinkColor { get; set; }
        public string LinkHoverColor { get; set; }
        public string ThongBao { get; set; }
        public string MoTa { get; set; }
        public string LinkFacebook { get; set; }
        public string SoDienThoai { get; set; }

    }
}