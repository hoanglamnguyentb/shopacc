using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.AppUserRepository;
using Hinet.Repository.DanhMucGameRepository;
using Hinet.Repository.DichVuRepository;
using Hinet.Repository.GameRepository;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Repository.TinTucRepository;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.TinTucService.Dto;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;




namespace Hinet.Service.TinTucService
{
    public class TinTucService : EntityService<TinTuc>, ITinTucService
    {
        IUnitOfWork _unitOfWork;
        ITinTucRepository _TinTucRepository;
        ILog _loger;
        IMapper _mapper;
        ITaiKhoanRepository _taiKhoanRepository;
        IDichVuRepository _dichVuRepository;
        IGameRepository _gameRepository;
        IDanhMucGameRepository _danhMucGameRepository;
        IAppUserRepository _appUserRepository;

        public TinTucService(IUnitOfWork unitOfWork,
                ITinTucRepository TinTucRepository,
                ILog loger,
                IMapper mapper,
                ITaiKhoanRepository taiKhoanRepository,
                IDichVuRepository dichVuRepository,
                IDanhMucGameRepository danhMucGameRepository,
                IGameRepository gameRepository,
                IAppUserRepository appUserRepository)
            : base(unitOfWork, TinTucRepository)
        {
            _unitOfWork = unitOfWork;
            _TinTucRepository = TinTucRepository;
            _loger = loger;
            _mapper = mapper;
            _taiKhoanRepository = taiKhoanRepository;
            _dichVuRepository = dichVuRepository;
            _danhMucGameRepository = danhMucGameRepository;
            _gameRepository = gameRepository;
            _appUserRepository = appUserRepository;
        }

        public PageListResultBO<TinTucDto> GetDaTaByPage(TinTucSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from TinTuctbl in _TinTucRepository.GetAllAsQueryable()

                        select new TinTucDto
                        {
                            Slug = TinTuctbl.Slug,
                            TieuDe = TinTuctbl.TieuDe,
                            NoiDung = TinTuctbl.NoiDung,
                            AnhBia = TinTuctbl.AnhBia,
                            TacGia = TinTuctbl.TacGia,
                            TrangThai = TinTuctbl.TrangThai,
                            ThoiGianXuatBan = TinTuctbl.ThoiGianXuatBan,
                            CreatedDate = TinTuctbl.CreatedDate,
                            CreatedBy = TinTuctbl.CreatedBy,
                            CreatedID = TinTuctbl.CreatedID,
                            UpdatedDate = TinTuctbl.UpdatedDate,
                            UpdatedBy = TinTuctbl.UpdatedBy,
                            UpdatedID = TinTuctbl.UpdatedID,
                            IsDelete = TinTuctbl.IsDelete,
                            DeleteTime = TinTuctbl.DeleteTime,
                            DeleteId = TinTuctbl.DeleteId,
                            Id = TinTuctbl.Id

                        };

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.SlugFilter))
                {
                    query = query.Where(x => x.Slug.Contains(searchModel.SlugFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.TieuDeFilter))
                {
                    query = query.Where(x => x.TieuDe.Contains(searchModel.TieuDeFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.NoiDungFilter))
                {
                    query = query.Where(x => x.NoiDung.Contains(searchModel.NoiDungFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.TacGiaFilter))
                {
                    query = query.Where(x => x.TacGia.Contains(searchModel.TacGiaFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
                {
                    query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
                }
                if (searchModel.ThoiGianXuatBanFilter != null)
                {
                    var date = searchModel.ThoiGianXuatBanFilter.Value.Date;
                    query = query.Where(x => DbFunctions.TruncateTime(x.ThoiGianXuatBan) == date);
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
            var resultmodel = new PageListResultBO<TinTucDto>();
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

        public TinTuc GetById(long id)
        {
            return _TinTucRepository.GetById(id);
        }

        public TinTucDto GetBySlug(string slug)
        {
            var query = from TinTuctbl in _TinTucRepository.GetAllAsQueryable().Where(x => x.Slug.Equals(slug))

                        join game in _gameRepository.GetAllAsQueryable()
                        on TinTuctbl.GameId  equals game.Id into gameGrp
                        from game in gameGrp.DefaultIfEmpty() 

                        select new TinTucDto
                        {
                            Slug = TinTuctbl.Slug,
                            TieuDe = TinTuctbl.TieuDe,
                            NoiDung = TinTuctbl.NoiDung,
                            AnhBia = TinTuctbl.AnhBia,
                            TacGia = TinTuctbl.TacGia,
                            TrangThai = TinTuctbl.TrangThai,
                            ThoiGianXuatBan = TinTuctbl.ThoiGianXuatBan,
                            CreatedDate = TinTuctbl.CreatedDate,
                            CreatedBy = TinTuctbl.CreatedBy,
                            CreatedID = TinTuctbl.CreatedID,
                            UpdatedDate = TinTuctbl.UpdatedDate,
                            UpdatedBy = TinTuctbl.UpdatedBy,
                            UpdatedID = TinTuctbl.UpdatedID,
                            IsDelete = TinTuctbl.IsDelete,
                            DeleteTime = TinTuctbl.DeleteTime,
                            DeleteId = TinTuctbl.DeleteId,
                            Id = TinTuctbl.Id,
                            Game = game,
                        };
            return query.FirstOrDefault();
        }

        public List<TinTuc> GetListTinTucLienQuan(long id)
        {
            var tinTuc = GetById(id);
            if (tinTuc == null)
            {
                return new List<TinTuc>();
            }
            var query = _TinTucRepository.GetQueryable()
                    .Where(tt => tt.Id != id && tt.TrangThai == TrangThaiTinTucConstant.XUATBAN);

            if (tinTuc.DichVuId.HasValue || tinTuc.GameId.HasValue)
            {
                query = query.Where(tt =>
                    (tinTuc.DichVuId.HasValue && tt.DichVuId == tinTuc.DichVuId) ||
                    (tinTuc.GameId.HasValue && tt.GameId == tinTuc.GameId)
                );
            }

            return query
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList();
            }


        public List<DanhMucGame> GetListDMGameLienQuan(long id)
        {
            var tinTuc = GetById(id);
            if (tinTuc == null || !tinTuc.DichVuId.HasValue)
            {
                return _danhMucGameRepository.GetQueryable()
                    .OrderBy(tt => Guid.NewGuid())
                    .Take(10)
                    .ToList();
            }

            return _danhMucGameRepository.GetQueryable()
                .Where(x => x.GameId == tinTuc.GameId)
                .OrderBy(tt => Guid.NewGuid())
                .Take(10)
                .ToList();
        }

        public List<DichVu> GetListDichVuLienQuan(long id)
        {
            var tinTuc = GetById(id);
            if (tinTuc == null || !tinTuc.DichVuId.HasValue)
            {
                return new List<DichVu>();
            }

            return _dichVuRepository.GetQueryable()
                .Where(x => x.Id == tinTuc.DichVuId)
                .OrderBy(tt => Guid.NewGuid())
                .Take(10)
                .ToList();
        }

    }
}


