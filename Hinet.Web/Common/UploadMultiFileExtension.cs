using CommonHelper.String;
using DocumentFormat.OpenXml.Packaging;
using Hangfire.Logging;
using Hinet.Model.Entities;
using Hinet.Repository.HUYENRepository;
using Hinet.Service.Common;
using log4net.Core;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Hinet.Web.Common
{
    public class UploadMultiFileExtension<T>
    {
        public ILog _Ilog;

        public UploadMultiFileExtension(ILog logger)
        {
            _logger = logger;
            this.hUYENRepository = hUYENRepository;
        }

        public bool UploadFileEdit(T obj, FormCollection form, List<Dictionary<string, string>> dsKeyForm)
        {
            try
            {
                var listFileGiayPhepDinhKem = _taiLieuDinhKemService.GetListTaiLieuAllByType(Constant.MODULE_CONSTANT.GIAYPHEPDINHKEM_ANTEN, obj.Id);
                var listFileVanBanDinhKem = _taiLieuDinhKemService.GetListTaiLieuAllByType(Constant.MODULE_CONSTANT.VANBANDINHKEM_ANTEN, obj.Id);

                //cập nhật file biểu mẫu
                var fileTypesDict = new Dictionary<string, string>(){
                    { Constant.MODULE_CONSTANT.GIAYPHEPDINHKEM_ANTEN, form["UploadFileTaiLieuAntenDinhKemEd"]},
                };
                var fileTypesDictVanBan = new Dictionary<string, string>() {
                    { Constant.MODULE_CONSTANT.VANBANDINHKEM_ANTEN, form["UploadFileTaiLieuVanBanAntenDinhKemEd"]} };

                Type type = typeof(T);
                PropertyInfo propertyInfo = type.GetProperty("Id");

                foreach (var fileType in dsKeyForm)
                {
                    foreach (KeyValuePair<string, string> pair in fileType)
                    {
                        var fileNames = pair.Value;
                        var itemType = pair.Key;

                        if (propertyInfo != null)
                        {
                        }
                        var itemPropertyId = propertyInfo.GetValue(obj);
                        long idProperty = (long)itemPropertyId;
                        var itemId = idProperty;

                        if (string.IsNullOrEmpty(fileNames))
                        {
                            if (listFileGiayPhepDinhKem != null && listFileGiayPhepDinhKem.Count > 0)
                            {
                                var tailieuGiayPhep = listFileGiayPhepDinhKem[0];
                                tailieuGiayPhep.SoKyHieu = model.SoKiHieuGiayPhep;
                                tailieuGiayPhep.NgayPhatHanh = model.NgayThangGiayPhep;

                                _taiLieuDinhKemService.Update(tailieuGiayPhep);
                            }

                            continue;
                        }

                        var LstNames = fileNames.Split('#');
                        var folderPathToSave = System.IO.Path.Combine(Server.MapPath($"~/Uploads/{itemType}/") + obj.Id);
                        if (!Directory.Exists(folderPathToSave))
                        {
                            Directory.CreateDirectory(folderPathToSave);
                        }
                        var groupTaiLieuDinhKem = new List<TaiLieuDinhKem>();
                        foreach (var itemStr in LstNames)
                        {
                            if (string.IsNullOrWhiteSpace(itemStr))
                            {
                                continue;
                            }
                            //Chuyển file về thư mục lưu trữ
                            var sourceFile = System.IO.Path.Combine(Server.MapPath($"~/Uploads/Temp/{itemType}/") + itemStr);
                            var destFile = System.IO.Path.Combine(folderPathToSave, itemStr);
                            var contentType = MimeMapping.GetMimeMapping(sourceFile);
                            if (itemType == Constant.MODULE_CONSTANT.GIAYPHEPDINHKEM_ANTEN)
                            {
                                if (contentType.StartsWith("application/vnd.openxmlformats-officedocument") && contentType.StartsWith("application/msword"))
                                {
                                    var docx = DocX.Load(sourceFile);
                                    var countTable = docx.Tables.Count;
                                    for (int i = 0; i < countTable; i++)
                                    {
                                        Novacode.Table t = docx.Tables[i];
                                        if (t.TableCaption == null)
                                        {
                                            continue;
                                        }
                                        var rowcount = t.RowCount;
                                        for (int k = rowcount - 1; k >= 1; k--)
                                        {
                                            t.RemoveRow(k);
                                        }
                                        t.InsertColumn();
                                        Novacode.Row firstrow = t.Rows[0];
                                        firstrow.Cells[t.ColumnCount - 1].Paragraphs.First().InsertText("[[AddRow_" + i + "]]");
                                        firstrow.Cells[t.ColumnCount - 1].Paragraphs.First().Alignment = Alignment.center;
                                        firstrow.Cells[t.ColumnCount - 1].Width = 5;
                                        t.InsertRow(1);
                                        Novacode.Row myRow = t.Rows[1];
                                        for (int j = 0; j < t.ColumnCount - 1; j++)
                                        {
                                            var key_name = "[[Table_" + StringUtilities.RemoveUnicode((t.TableCaption + "_" + t.Paragraphs[j].Text)).Replace(" ", "_") + "__1]]";
                                            myRow.Cells[j].Paragraphs.First().InsertText(" " + key_name);
                                        }
                                        myRow.Cells.Last().Paragraphs.First().InsertText($"[[DeleteRow_{i}]]");
                                        myRow.Cells.Last().Paragraphs.First().Alignment = Alignment.center;
                                    }
                                    docx.SaveAs(sourceFile);

                                    byte[] byteArray = System.IO.File.ReadAllBytes(sourceFile);
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        memoryStream.Write(byteArray, 0, byteArray.Length);
                                        using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                                        {
                                            string documentText;
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
                                            var html = HtmlConverter.ConvertToHtml(doc, settings);
                                            //obj.HtmlContent = html.ToString();
                                            //var regex = new Regex(@"\[\[[\w\.]*\]\]");
                                            //var matches = regex.Matches(obj.HtmlContent);
                                            //var tmpMatches = matches.Cast<Match>().Select(m => m.Value.Trim()).ToArray();
                                            //obj.KeyStored = string.Join(",", tmpMatches);
                                            //obj.KeyStored = obj.KeyStored.Replace("[[", "");
                                            //obj.KeyStored = obj.KeyStored.Replace("]]", "");
                                            _QlAntensService.Save(obj);
                                        }
                                    }
                                }
                                else if (contentType.StartsWith("image/"))
                                {
                                    var imageUrl = $"/Uploads/{itemType}/{itemId}/{itemStr}";
                                    //obj.HtmlContent += $"<img src=\"{imageUrl}\" alt=\"{itemStr}\" />";
                                }
                            }
                            else
                            {
                                var html = "";
                                // Create a stream from the Word document
                                byte[] byteArray = System.IO.File.ReadAllBytes(sourceFile);
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    memoryStream.Write(byteArray, 0, byteArray.Length);
                                    using (WordprocessingDocument wDoc = WordprocessingDocument.Open(memoryStream, true))
                                    {
                                        var settings = new HtmlConverterSettings()
                                        {
                                            FabricateCssClasses = false, // Generates CSS classes for styling
                                            CssClassPrefix = "my-", // Prefix for CSS class names
                                            RestrictToSupportedLanguages = false, // Allows unsupported languages
                                            RestrictToSupportedNumberingFormats = false
                                        };
                                        html = OpenXmlPowerTools.HtmlConverter.ConvertToHtml(wDoc, settings).ToString();
                                    }
                                }
                                _QlAntensService.Save(obj);
                            }

                            System.IO.File.Copy(sourceFile, destFile, true);
                            var fileResult = new TaiLieuDinhKem()
                            {
                                TenTaiLieu = itemStr,
                                MoTa = Path.GetExtension(sourceFile),
                                SoKyHieu = model.SoKiHieuGiayPhep,
                                NgayPhatHanh = model.NgayThangGiayPhep,
                                UserId = CurrentUserInfo.Id,
                                DuongDanFile = $"/{itemType}/" + itemId + "/" + itemStr,
                                Item_ID = itemId,
                                DinhDangFile = contentType,
                                LoaiTaiLieu = itemType,
                                CreatedDate = DateTime.Now,
                            };
                            groupTaiLieuDinhKem.Add(fileResult);

                            try
                            {
                                //Xóa file ở thư mục temp
                                System.IO.File.Delete(Server.MapPath($"~/Uploads/Temp/{itemType}/") + itemStr);
                            }
                            catch (System.IO.IOException ex)
                            {
                                _Ilog.Error(ex.Message, ex);
                                continue;
                            }
                        }

                        if (listFileGiayPhepDinhKem.Count() > 0)
                        {
                            _taiLieuDinhKemService.DeleteRange(listFileGiayPhepDinhKem);
                        }
                        _taiLieuDinhKemService.InsertRange(groupTaiLieuDinhKem);
                        _QlAntensService.Update(obj);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}