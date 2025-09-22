using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hinet.Web.Models
{
    public class UpLoadFileSingle
    {
        public string Title { get; set; } = "Tài liệu đính kèm";
        public string FileExtension { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Type { get; set; } = string.Empty;
    }

}