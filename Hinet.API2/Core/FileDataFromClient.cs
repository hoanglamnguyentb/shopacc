using CommonHelper.String;
using System;
using System.IO;
using System.Linq;

namespace Hinet.API2.Core
{
    public class FileDataFromClient
    {
        public (bool, string) save(string pathFolder, string rootPath, string extentionList)
        {
            var arrName = fileName.Split('.');
            var extention = '.' + arrName[arrName.Length - 1];

            try
            {
                if (!string.IsNullOrEmpty(extentionList))
                {
                    var listExtention = extentionList.Split(',');
                    if (!listExtention.Contains(extention.ToLower()))
                    {
                        return (false, "Định dạng không được chấp nhận");
                    }
                }
                if (!string.IsNullOrEmpty(data))
                {
                    var nameOfFile = fileName;
                    if (!Directory.Exists(rootPath + pathFolder))
                    {
                        Directory.CreateDirectory(rootPath + pathFolder);
                    }
                    var pathNameSave = rootPath + pathFolder + nameOfFile;
                    if (System.IO.File.Exists(pathNameSave))
                    {
                        nameOfFile = fileName.GetNewFileName();
                        pathNameSave = rootPath + pathFolder + nameOfFile;
                    }
                    Byte[] bytes = Convert.FromBase64String(data.Substring(data.LastIndexOf(',') + 1));
                    File.WriteAllBytes(pathNameSave, bytes);
                    return (true, pathFolder + nameOfFile);
                }
                else
                {
                    return (false, "Không có dữ liệu để lưu");
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public string fileName { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string data { get; set; }
    }
}