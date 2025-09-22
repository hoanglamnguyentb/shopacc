using System.Collections.Generic;

namespace Hinet.Web.Models
{
    public class ModelStateViewModel
    {
        public string key { get; set; }
        public string value { get; set; }
        public List<string> error { get; set; }
    }
}