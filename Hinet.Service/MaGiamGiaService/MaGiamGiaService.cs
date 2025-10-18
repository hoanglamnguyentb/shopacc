using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.GianHangRepository;
using Hinet.Repository.MaGiamGiaRepository;
using Hinet.Service.Common;
using Hinet.Service.MaGiamGiaService.Dto;
using log4net;
using PagedList;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Interop;




namespace Hinet.Service.MaGiamGiaService
{
    public class MaGiamGiaService : EntityService<MaGiamGia>, IMaGiamGiaService
    {
        IUnitOfWork _unitOfWork;
        IMaGiamGiaRepository _MaGiamGiaRepository;
        ILog _loger;
        IMapper _mapper;
        IGianHangRepository _gianHangRepository;

        public MaGiamGiaService(IUnitOfWork unitOfWork,
                IMaGiamGiaRepository MaGiamGiaRepository,
                ILog loger,
                IMapper mapper,
                IGianHangRepository gianHangRepository)
            : base(unitOfWork, MaGiamGiaRepository)
        {
            _unitOfWork = unitOfWork;
            _MaGiamGiaRepository = MaGiamGiaRepository;
            _loger = loger;
            _mapper = mapper;
            _gianHangRepository = gianHangRepository;
        }

        public PageListResultBO<MaGiamGiaDto> GetDaTaByPage(MaGiamGiaSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var baseQuery = _MaGiamGiaRepository.GetAllAsQueryable();

            if (searchModel != null)
            {
                if (searchModel.TuNgayFilter != null)
                    baseQuery = baseQuery.Where(x => x.TuNgay == searchModel.TuNgayFilter);

                if (searchModel.DenNgayFilter != null)
                    baseQuery = baseQuery.Where(x => x.DenNgay == searchModel.DenNgayFilter);

                if (searchModel.ToanHeThongFilter != null)
                    baseQuery = baseQuery.Where(x => x.ToanHeThong == searchModel.ToanHeThongFilter);

                if (searchModel.TrangThaiFilter != null)
                    baseQuery = baseQuery.Where(x => x.TrangThai == searchModel.TrangThaiFilter);

                if (!string.IsNullOrEmpty(searchModel.ThongTinFilter))
                    baseQuery = baseQuery.Where(x => x.ThongTin.Contains(searchModel.ThongTinFilter));

                if (!string.IsNullOrEmpty(searchModel.GianHangApDungFilter))
                    baseQuery = baseQuery.Where(x => x.GianHangApDung.Contains(searchModel.GianHangApDungFilter));

                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                    baseQuery = baseQuery.OrderBy(searchModel.sortQuery); // Dynamic LINQ hoạt động ở IQueryable
                else
                    baseQuery = baseQuery.OrderByDescending(x => x.Id);
            }
            else
            {
                baseQuery = baseQuery.OrderByDescending(x => x.Id);
            }

            var allGianHang = _gianHangRepository.GetAllAsQueryable().ToList();

            var query = baseQuery
                .AsEnumerable() // Từ đây trở đi là LINQ to Objects
                .Select(MaGiamGiatbl => new MaGiamGiaDto
                {
                    Id = MaGiamGiatbl.Id,
                    SoLuong = MaGiamGiatbl.SoLuong,
                    TuNgay = MaGiamGiatbl.TuNgay,
                    DenNgay = MaGiamGiatbl.DenNgay,
                    ToanHeThong = MaGiamGiatbl.ToanHeThong,
                    TrangThai = MaGiamGiatbl.TrangThai,
                    ThongTin = MaGiamGiatbl.ThongTin,
                    GianHangApDung = MaGiamGiatbl.GianHangApDung,
                    CreatedBy = MaGiamGiatbl.CreatedBy,
                    UpdatedBy = MaGiamGiatbl.UpdatedBy,
                    CreatedDate = MaGiamGiatbl.CreatedDate,
                    UpdatedDate = MaGiamGiatbl.UpdatedDate,
                    IsDelete = MaGiamGiatbl.IsDelete,
                    CreatedID = MaGiamGiatbl.CreatedID,
                    UpdatedID = MaGiamGiatbl.UpdatedID,
                    DeleteId = MaGiamGiatbl.DeleteId,
                    DeleteTime = MaGiamGiatbl.DeleteTime,
                    ListGianHang = allGianHang
                        .Where(g => (MaGiamGiatbl.GianHangApDung ?? "")
                            .Split(',')
                            .Contains(g.Id.ToString()))
                        .ToList()
                });

            var resultmodel = new PageListResultBO<MaGiamGiaDto>();

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

        public MaGiamGia GetById(long id)
        {
            return _MaGiamGiaRepository.GetById(id);
        }


    }
}
