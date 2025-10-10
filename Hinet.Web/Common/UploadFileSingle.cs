using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.TaiLieuDinhKemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MassTransit.ValidationResultExtensions;
using System.Web.Hosting;
using System.IO;

namespace Hinet.Web.Common
{
    public class UploadFileSingle
    {
        public static string UploadFile(HttpPostedFileBase FileDinhKem, string allowedExtensions, string uploadPath)
        {
            if (FileDinhKem != null && FileDinhKem.ContentLength > 0)
            {
                //uploadPath = uploadPath.StartsWith("/") ? uploadPath.Substring(1) : uploadPath;

                var resultUpload = UploadProvider.SaveFile(FileDinhKem, null, allowedExtensions, null, uploadPath, HostingEnvironment.MapPath("/"));

                if (resultUpload.status)
                {
                    return resultUpload.path;
                }
            }
            return null;
        }

        public static string EditFile(string FileDinhKemOld, string allowedExtensions, HttpPostedFileBase FileDinhKem, bool isFileDinhKemDeleted, string Path)
        {
            string resultLink = "";

            if (isFileDinhKemDeleted)
            {
                // Nếu file bị xóa, xóa file cũ khỏi hệ thống
                UploadFileSingle.DeleteFile(FileDinhKemOld);
                resultLink = null; // Cập nhật trong cơ sở dữ liệu
            }
            else if (FileDinhKem != null && FileDinhKem.ContentLength > 0)
            {
                //Xoá file cũ
                UploadFileSingle.DeleteFile(FileDinhKemOld);
                resultLink = UploadFileSingle.UploadFile(FileDinhKem, allowedExtensions, Path);
            }
            else
            {
                resultLink = FileDinhKemOld;
            }
            return resultLink;
        }

        public static void DeleteFile(string oldFileDinhKem)
        {
            if (!string.IsNullOrEmpty(oldFileDinhKem))
            {
                var oldFilePath = HostingEnvironment.MapPath("~/" + oldFileDinhKem);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
        }
    }
}