using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Service.BinhLuanService;
using Hinet.Service.BinhLuanService.Dto;
using Hinet.Service.GiaoDichService.Dto;
using Hinet.Web.Areas.BinhLuanArea.Models;
using Hinet.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Web.Controllers
{
    public class BinhLuanController : EndUserController
    {
        private readonly IBinhLuanService _binhLuanService;
        private readonly IMapper _map;
        public BinhLuanController(IBinhLuanService binhLuanService, IMapper map)
        {
            _binhLuanService = binhLuanService;
            _map = map;
        }

        public ActionResult ThemBinhLuanAsync(CreateVM model)
        {
            var obj = _map.Map<CreateVM, BinhLuan>(model);
            obj.NguoiBinhLuanId = CurrentUserId.GetValueOrDefault();
            _binhLuanService.Create(obj);
            var searchModel = new BinhLuanSearchDto
            {
                DoiTuongIdFilter = model.DoiTuongId,
                LoaiDoiTuongFilter = model.LoaiDoiTuong,
                NguoiBinhLuanIdFilter = CurrentUserId.GetValueOrDefault()
            };
            var data = _binhLuanService.GetDaTaByPage(searchModel, 1, -1).ListItem;
            ViewBag.DoiTuongId = model.DoiTuongId;
            ViewBag.LoaiDoiTuong = model.LoaiDoiTuong;
            return PartialView("_DanhSachBinhLuanPartial", data);
        }

        [AllowAnonymous]
        public ActionResult DanhSach(long doiTuongId, string loaiDoiTuong)
        {
            var searchModel = new BinhLuanSearchDto
            {
                DoiTuongIdFilter = doiTuongId,
                LoaiDoiTuongFilter = loaiDoiTuong,
            };
            var data = _binhLuanService.GetDaTaByPage(searchModel, 1, -1).ListItem;
            ViewBag.DoiTuongId = doiTuongId;
            ViewBag.LoaiDoiTuong = loaiDoiTuong;
            return PartialView("_DanhSachBinhLuanPartial", data);
        }

    }
}