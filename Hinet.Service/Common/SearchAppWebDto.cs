using System;

namespace Hinet.Service.Common
{
    public class SearchAppWebDto
    {
        public long id { get; set; }
        public string Domain { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string CompanyTaxCode { get; set; }
        public DateTime LastDate { get; set; }
    }
}