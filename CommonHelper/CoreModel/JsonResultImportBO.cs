using System.Collections.Generic;

namespace CommonHelper
{
    public class JsonResultImportBO<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<T> ListData { get; set; }
        public List<List<string>> ListFalse { get; set; }

        public JsonResultImportBO(bool state)
        {
            Status = state;
        }

        public long IDGroup { get; set; }
        public Dictionary<string, object> LstParam { get; set; }
    }
}