using CommonHelper.Upload;
using Hinet.Model.Entities;
using Hinet.Service.TaiLieuDinhKemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hinet.Web.Common
{
    public class UploadCommon
    {
        private static ITaiLieuDinhKemService _taiLieuDinhKemService;

        public UploadCommon(ITaiLieuDinhKemService taiLieuDinhKemService)
        {
            _taiLieuDinhKemService = taiLieuDinhKemService;
        }

        public bool SaveMutipleFile(List<HttpPostedFileBase> listFile, string name, string extensionList, long? maxSize, string folder, string path, long itemId, string itemType)
        {
            foreach (var file in listFile)
            {
                var result = UploadProvider.SaveFile(file, string.Empty, extensionList, maxSize, folder, path);
                if (result.status)
                {
                    //lưu bảng TaiLieuDinhKem
                    TaiLieuDinhKem taiLieuDinhKem = new TaiLieuDinhKem();
                    taiLieuDinhKem.TenTaiLieu = result.filename;
                    taiLieuDinhKem.DuongDanFile = result.path;
                    taiLieuDinhKem.Item_ID = itemId;
                    taiLieuDinhKem.LoaiTaiLieu = itemType;
                    _taiLieuDinhKemService.Save(taiLieuDinhKem);
                }
            }
            return true;
        }

        public bool SaveMutipleFileAndName(List<HttpPostedFileBase> listFile, List<string> name, string extensionList,
            long? maxSize, string folder, string path, long itemId, string itemType)
        {
            for (var i = 0; i < listFile.Count(); i++)
            {
                if (listFile[i] != null)
                {
                    var result = UploadProvider.SaveFile(listFile[i], name[i], extensionList, maxSize, folder, path);
                    if (result.status)
                    {
                        var arrName = listFile[i].FileName.Split('.');
                        var extention = '.' + arrName[arrName.Length - 1];

                        //lưu bảng TaiLieuDinhKem
                        TaiLieuDinhKem taiLieuDinhKem = new TaiLieuDinhKem();
                        taiLieuDinhKem.TenTaiLieu = name[i];
                        taiLieuDinhKem.DuongDanFile = "UpLoads/" + result.path;
                        taiLieuDinhKem.Item_ID = itemId;
                        taiLieuDinhKem.KichThuoc = listFile[i].ContentLength / 1024;

                        taiLieuDinhKem.NgayPhatHanh = DateTime.Now;
                        taiLieuDinhKem.DinhDangFile = extention;
                        taiLieuDinhKem.LoaiTaiLieu = itemType;
                        _taiLieuDinhKemService.Save(taiLieuDinhKem);
                    }
                }
            }
            return true;
        }

        public bool SaveMutipleFile_LoaiFile(List<HttpPostedFileBase> listFile, List<string> name, string extensionList,
                long? maxSize, string folder, string path, long itemId, string itemType, List<string> loaiFile)
        {
            for (var i = 0; i < listFile.Count(); i++)
            {
                if (listFile[i] != null)
                {
                    var result = UploadProvider.SaveFile(listFile[i], name[i], extensionList, maxSize, folder, path);
                    if (result.status)
                    {
                        //lưu bảng TaiLieuDinhKem
                        TaiLieuDinhKem taiLieuDinhKem = new TaiLieuDinhKem();
                        taiLieuDinhKem.TenTaiLieu = result.filename;
                        taiLieuDinhKem.DuongDanFile = result.path;
                        taiLieuDinhKem.Item_ID = itemId;
                        taiLieuDinhKem.LoaiTaiLieu = itemType + "/" + loaiFile[i];
                        _taiLieuDinhKemService.Save(taiLieuDinhKem);
                    }
                }
            }
            return true;
        }

        public bool SaveFile(HttpPostedFileBase file, string name, string extensionList, long? maxSize, string folder, string path, long itemId, string itemType)
        {
            var result = UploadProvider.SaveFile(file, string.Empty, extensionList, maxSize, folder, path);
            if (result.status)
            {
                //lưu bảng TaiLieuDinhKem
                TaiLieuDinhKem taiLieuDinhKem = new TaiLieuDinhKem();
                taiLieuDinhKem.TenTaiLieu = result.filename;
                taiLieuDinhKem.DuongDanFile = result.path;
                taiLieuDinhKem.Item_ID = itemId;
                taiLieuDinhKem.LoaiTaiLieu = itemType;
                _taiLieuDinhKemService.Save(taiLieuDinhKem);
            }
            return true;
        }
    }
}