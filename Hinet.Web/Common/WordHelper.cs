using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;
using System;
using System.IO;

namespace Hinet.Web.Common
{
    public static class WordHelper
    {
        public static byte[] HtmlToWord(String html)
        {
            const string filename = "test.docx";
            if (File.Exists(filename)) File.Delete(filename);

            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(
                       generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    HtmlConverter converter = new HtmlConverter(mainPart);
                    Body body = mainPart.Document.Body;

                    var paragraphs = converter.Parse(html);
                    for (int i = 0; i < paragraphs.Count; i++)
                    {
                        body.Append(paragraphs[i]);
                    }

                    mainPart.Document.Save();
                }

                return generatedDocument.ToArray();
            }
        }

        /// <summary>
        /// @author:duynn
        /// @since: 19/07/2019
        /// </summary>
        /// <param name="wordFileName"></param>
        /// <param name="pdfFileName"></param>
        public static void ConvertWordToPDF(string wordFileName, string pdfFileName)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application wordApp =
                new Microsoft.Office.Interop.Word.Application();
                object missingValue = System.Reflection.Missing.Value;

                wordApp.Visible = false;
                wordApp.ScreenUpdating = false;
                // Cast as Object for word Open method
                object filename = (object)wordFileName;
                // Use the dummy value as a placeholder for optional arguments

                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue);

                doc.Activate();

                object outputFileName = pdfFileName;
                object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

                // Save document into PDF Format
                doc.SaveAs(ref outputFileName, ref fileFormat, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue,
                 ref missingValue, ref missingValue, ref missingValue);

                object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                ((Microsoft.Office.Interop.Word._Document)doc).Close(ref saveChanges, ref missingValue, ref missingValue);
                doc = null;
                // word has to be cast to type _Application so that it will find
                // the correct Quit method.
                ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref missingValue, ref missingValue, ref missingValue);
                wordApp = null;
            }
            catch (Exception ex)
            {
                //CommonLog.Error(ex.Message, ex);
            }
        }
    }
}