using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hinet.Web.Common
{
    public class ExportWordHelperV2
    {
        public static string ExportWork<T>(T dto, string pathDocument, string prefix, string server) where T : class
        {
            string templateFilePath = $"{server}Uploads\\{pathDocument}";
            string tempFolderPath = $"{server}Temp\\Uploads\\ExportTemp";
            // Ensure temp folder exists
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            // Define output folder path and ensure it exists
            string outputFolderPath = $"{server}Uploads\\Export";
            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }

            // Generate file name with timestamp
            //var file = pathDocument;
            string path = $"{server}Uploads\\{pathDocument}";
            string fileName = prefix + "_" + Path.GetFileName(path);

            // Initialize the ExportWordHelper with the generic DTO
            var wordHelper = new ExportWordHelper<T>(path, tempFolderPath, outputFolderPath, fileName);

            // Perform the export operation
            var export = wordHelper.Export(dto, "");
            if (export.exportSuccess)
            {
                return export.exportResultFileName;
            }

            return null;

        }
    }
}