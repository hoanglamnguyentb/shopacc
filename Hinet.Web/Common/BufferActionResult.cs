using System.IO;
using System.Web.Mvc;

namespace Hinet.Web.Common
{
    public class BufferActionResult : ActionResult
    {
        private readonly MemoryStream _excelData;
        private readonly string _fileName;

        public BufferActionResult(MemoryStream excelData, string fileName)
        {
            _excelData = excelData;
            _fileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            context.HttpContext.Response.AddHeader("Content-Disposition", $"attachment; filename=\"{_fileName}\"");
            context.HttpContext.Response.BinaryWrite(_excelData.ToArray());
            context.HttpContext.Response.End();
        }
    }
}