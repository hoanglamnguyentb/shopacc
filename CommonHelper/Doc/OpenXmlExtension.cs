using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Doc
{
    public class OpenXmlExtension
    {
        public List<OpenXmlElement> Elements = new List<OpenXmlElement>();
        public void GetChildElements(OpenXmlElement ele)
        {
            var children = ele.ChildElements;
            Elements.AddRange(children);
            foreach (var child in children)
            {
                GetChildElements(child);
            }
        }
    }
}
