using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Repository;
using Hinet.Repository.DonHangGiaTriThuocTinhRepository;
using Hinet.Repository.DonHangRepository;
using Hinet.Repository.GianHangRepository;
using Hinet.Repository.MaGiamGiaRepository;
using Hinet.Repository.VatPhamRepository;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DonHangService.Dto;
using Hinet.Service.TaiKhoanService.Dto;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;




namespace Hinet.Service.DonHangService
{
    public class DonHangService : EntityService<DonHang>, IDonHangService
    {
        IUnitOfWork _unitOfWork;
        IDonHangRepository _DonHangRepository;
	    ILog _loger;
        IMapper _mapper;
        IGianHangRepository _gianHangRepository;
        IVatPhamRepository _vatPhamRepository;
        IMaGiamGiaRepository _maGiamGiaRepository;
        IDonHangGiaTriThuocTinhRepository _donHangGiaTriThuocTinhRepository;

        public DonHangService(IUnitOfWork unitOfWork,
                IDonHangRepository DonHangRepository,
                ILog loger,
                IMapper mapper,
                IGianHangRepository gianHangRepository,
                IVatPhamRepository vatPhamRepository,
                IMaGiamGiaRepository maGiamGiaRepository,
                IDonHangGiaTriThuocTinhRepository donHangGiaTriThuocTinhRepository)
            : base(unitOfWork, DonHangRepository)
        {
            _unitOfWork = unitOfWork;
            _DonHangRepository = DonHangRepository;
            _loger = loger;
            _mapper = mapper;
            _gianHangRepository = gianHangRepository;
            _vatPhamRepository = vatPhamRepository;
            _maGiamGiaRepository = maGiamGiaRepository;
            _donHangGiaTriThuocTinhRepository = donHangGiaTriThuocTinhRepository;
        }

        public PageListResultBO<DonHangDto> GetDaTaByPage(DonHangSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from DonHangtbl in _DonHangRepository.GetAllAsQueryable()

                        select new DonHangDto
                        {
							DonHangId = DonHangtbl.DonHangId,
							VatPhamId = DonHangtbl.VatPhamId,
							MaGiamGiaId = DonHangtbl.MaGiamGiaId,
							GiaGoc = DonHangtbl.GiaGoc,
							GiaKhuyenMai = DonHangtbl.GiaKhuyenMai,
							TrangThai = DonHangtbl.TrangThai,
							QrUrl = DonHangtbl.QrUrl,
							CreatedBy = DonHangtbl.CreatedBy,
							UpdatedBy = DonHangtbl.UpdatedBy,
							Id = DonHangtbl.Id,
							CreatedDate = DonHangtbl.CreatedDate,
							UpdatedDate = DonHangtbl.UpdatedDate,
							DeleteTime = DonHangtbl.DeleteTime,
							IsDelete = DonHangtbl.IsDelete,
							CreatedID = DonHangtbl.CreatedID,
							UpdatedID = DonHangtbl.UpdatedID,
							DeleteId = DonHangtbl.DeleteId
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.DonHangIdFilter!=null)
		{
			query = query.Where(x => x.DonHangId==searchModel.DonHangIdFilter);
		}
		if (searchModel.VatPhamIdFilter!=null)
		{
			query = query.Where(x => x.VatPhamId==searchModel.VatPhamIdFilter);
		}
		if (searchModel.MaGiamGiaIdFilter!=null)
		{
			query = query.Where(x => x.MaGiamGiaId==searchModel.MaGiamGiaIdFilter);
		}
		if (searchModel.GiaGocFilter!=null)
		{
			query = query.Where(x => x.GiaGoc==searchModel.GiaGocFilter);
		}
		if (searchModel.GiaKhuyenMaiFilter!=null)
		{
			query = query.Where(x => x.GiaKhuyenMai==searchModel.GiaKhuyenMaiFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
		{
			query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.QrUrlFilter))
		{
			query = query.Where(x => x.QrUrl.Contains(searchModel.QrUrlFilter));
		}


                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }
            var resultmodel = new PageListResultBO<DonHangDto>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = dataPageList;
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = dataPageList.ToList();
            }
            return resultmodel;
        }

        public DonHang GetById(long id)
        {
            return _DonHangRepository.GetById(id);
        }

        public DonHangDto GetDtoById(long id)
        {
            var query = from dh in _DonHangRepository.GetQueryable()
                        .Where(x => x.Id == id)

                        join gianHang in _gianHangRepository.GetQueryable()
                        on dh.GianHangId equals gianHang.Id into gianHangGrp
                        from gianHang in gianHangGrp.DefaultIfEmpty()

                        join vatPham in _vatPhamRepository.GetQueryable()
                        on dh.VatPhamId equals vatPham.Id

                        join maGiamGia in _maGiamGiaRepository.GetAllAsQueryable()
                        on dh.MaGiamGiaId equals maGiamGia.Id into maGiamGiaGrp
                        from maGiamGia in maGiamGiaGrp.DefaultIfEmpty()

                        join gttt in _donHangGiaTriThuocTinhRepository.GetAllAsQueryable()
                        on dh.Id equals gttt.DonHangId into gtttGrp

                        select new DonHangDto
                        {
                            Id = dh.Id,
                            VatPhamId = dh.VatPhamId,
                            VatPham = vatPham,
                            GianHangId = dh.GianHangId,
                            GianHang = gianHang,
                            MaGiamGia = maGiamGia,
                            GiaGoc = dh.GiaGoc,
                            GiaKhuyenMai = dh.GiaKhuyenMai,
                            SoLuong = dh.SoLuong,
                            TongTien = dh.TongTien,
                            TrangThai = dh.TrangThai,
                            QrUrl = dh.QrUrl,
                            GhiChu = dh.GhiChu,
                            NoiDungChuyenKhoan = dh.NoiDungChuyenKhoan,
                            MaGiaoDich = dh.MaGiaoDich,
                            ListGTTT = gtttGrp.ToList(),
                        };
            return query.FirstOrDefault();
        }
    }
}
