using CommonHelper.Doc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Novacode;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CommonHelper.String
{
    public static class DocUltilities
    {
        public static WordProps GetFileWordContent(this string filePath)
        {
            var wordProps = new WordProps();
            DocX docx = DocX.Load(filePath);
            var countTable = docx.Tables.Count;
            for (int iTable = 0; iTable < countTable; iTable++)
            {
                Novacode.Table table = docx.Tables[iTable];
                if (table == null || table.TableCaption.IsNullOrEmptyString() || table.TableCaption.IsNormalized())
                {
                    continue;
                }
                /**
                 * loại bỏ các dòng trắng trên bảng
                 */
                var countRow = table.RowCount;
                for (int iRow = countRow - 1; iRow >= 1; iRow--)
                {
                    table.RemoveRow(iRow);
                }
                table.InsertRow(1);
                Novacode.Row firstRow = table.Rows[1];
                for (int iRow = 0; iRow < table.ColumnCount; iRow++)
                {
                    var key = StringUtilities.ConvertToUnsign((table.TableCaption + "_" + table.Paragraphs[iRow].Text)).ToUpper().Replace(" ", "_");
                    var columnName = "[[ISTABLE_" + key + "]]";
                    firstRow.Cells[iRow].Paragraphs.First().InsertText(" " + columnName);
                }
            }
            docx.SaveAs(filePath);
            byte[] byteArray = System.IO.File.ReadAllBytes(filePath);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    var ext = new OpenXmlExtension();
                    ext.GetChildElements(doc.MainDocumentPart.Document.Body);
                    var allElements = ext.Elements;

                    foreach (var item in allElements)
                    {
                        var isPageSize = typeof(PageSize).Equals(item.GetType());
                        if (isPageSize)
                        {
                            var pageSize = (PageSize)item;
                            wordProps.Width = Convert.ToInt32(pageSize.Width / 15); //convert twip to px
                            wordProps.Height = Convert.ToInt32(pageSize.Height / 15);
                            break;
                        }
                    }


                    var documentText = string.Empty;
                    using (StreamReader reader = new StreamReader(doc.MainDocumentPart.GetStream()))
                    {
                        documentText = reader.ReadToEnd();
                    }
                    documentText = documentText.Replace("##date##", DateTime.Today.ToShortDateString());
                    using (StreamWriter writer = new StreamWriter(doc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        writer.Write(documentText);
                    }
                    int imageCounter = 0;
                    HtmlConverterSettings settings = new HtmlConverterSettings()
                    {
                        PageTitle = "",
                        ImageHandler = imageInfo =>
                        {
                            DirectoryInfo localDirInfo = new DirectoryInfo("img");
                            if (!localDirInfo.Exists)
                                localDirInfo.Create();
                            ++imageCounter;
                            string extension = imageInfo.ContentType.Split('/')[1].ToLower();
                            ImageFormat imageFormat = null;
                            if (extension == "png")
                            {
                                extension = "gif";
                                imageFormat = ImageFormat.Gif;
                            }
                            else if (extension == "gif")
                                imageFormat = ImageFormat.Gif;
                            else if (extension == "bmp")
                                imageFormat = ImageFormat.Bmp;
                            else if (extension == "jpeg")
                                imageFormat = ImageFormat.Jpeg;
                            else if (extension == "tiff")
                            {
                                extension = "gif";
                                imageFormat = ImageFormat.Gif;
                            }
                            else if (extension == "x-wmf")
                            {
                                extension = "wmf";
                                imageFormat = ImageFormat.Wmf;
                            }
                            if (imageFormat == null)
                                return null;

                            string imageFileName = "img/image" +
                                imageCounter.ToString() + "." + extension;
                            try
                            {
                                imageInfo.Bitmap.Save(imageFileName, imageFormat);
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                return null;
                            }
                            XElement img = new XElement(Xhtml.img,
                                new XAttribute(NoNamespace.src, imageFileName),
                                imageInfo.ImgStyleAttribute,
                                imageInfo.AltText != null ?
                                    new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
                            return img;
                        }
                    };
                    XElement html = HtmlConverter.ConvertToHtml(doc, settings);
                    wordProps.Content = html.ToString();
                }
            }
            return wordProps;
        }

        public static bool IsNullOrEmptyString(this string input)
        {
            var result = string.IsNullOrEmpty(input);
            return result;
        }



        public static List<string> GetHighlightedText(this string filePath)
        {
            List<string> highlightedTexts = new List<string>();

            // Mở tài liệu Word bằng OpenXml
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                // Duyệt qua tất cả các đoạn văn (Paragraph) trong tài liệu từ OpenXml
                foreach (DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                {
                    // Duyệt qua tất cả các phần tử văn bản (Run) trong đoạn văn
                    foreach (DocumentFormat.OpenXml.Wordprocessing.Run run in paragraph.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>())
                    {
                        // Kiểm tra nếu phần tử có thuộc tính highlight
                        var highlight = run.RunProperties?.Highlight;
                        if (highlight != null && highlight.Val != null)
                        {
                            // Nếu màu highlight là "yellow" hoặc bất kỳ màu nào bạn muốn
                            if (highlight.Val.Value == HighlightColorValues.Yellow)
                            {
                                // Thêm văn bản vào danh sách kết quả
                                highlightedTexts.Add(run.InnerText);
                            }
                        }
                    }
                }
            }

            return highlightedTexts;
        }

    }

    public class WordProps
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public string Content { get; set; }

        public string Keys { get; set; }

    }





}
