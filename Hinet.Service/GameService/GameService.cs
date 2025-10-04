using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhMucGameRepository;
using Hinet.Repository.DanhMucGameTaiKhoanRepository;
using Hinet.Repository.GameRepository;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Repository.TaiLieuDinhKemRepository;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.GameService.Dto;
using Hinet.Service.TaiKhoanService.Dto;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Dynamic;

namespace Hinet.Service.GameService
{
    public class GameService : EntityService<Game>, IGameService
    {
        IUnitOfWork _unitOfWork;
        IGameRepository _GameRepository;
        ILog _loger;
        IMapper _mapper;
        IDanhMucGameTaiKhoanRepository _danhMucGameTaiKhoanRepository;
        IDanhMucGameRepository _danhMucGameRepository;
        ITaiKhoanRepository _taiKhoanRepository;
        ITaiLieuDinhKemRepository _taiLieuDinhKemRepository;
        public GameService(IUnitOfWork unitOfWork,
                IGameRepository GameRepository,
                ILog loger,
                IMapper mapper,
                IDanhMucGameRepository danhMucGameRepository,
                IDanhMucGameTaiKhoanRepository danhMucGameTaiKhoanRepository,
                ITaiKhoanRepository taiKhoanRepository,
                ITaiLieuDinhKemRepository taiLieuDinhKemRepository)
            : base(unitOfWork, GameRepository)
        {
            _unitOfWork = unitOfWork;
            _GameRepository = GameRepository;
            _loger = loger;
            _mapper = mapper;
            _danhMucGameRepository = danhMucGameRepository;
            _danhMucGameTaiKhoanRepository = danhMucGameTaiKhoanRepository;
            _taiKhoanRepository = taiKhoanRepository;
            _taiLieuDinhKemRepository = taiLieuDinhKemRepository;
        }

        public PageListResultBO<GameDto> GetDaTaByPage(GameSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from Gametbl in _GameRepository.GetAllAsQueryable()

                        select new GameDto
                        {
                            Name = Gametbl.Name,
                            MoTa = Gametbl.MoTa,
                            TrangThai = Gametbl.TrangThai,
                            CreatedDate = Gametbl.CreatedDate,
                            CreatedBy = Gametbl.CreatedBy,
                            CreatedID = Gametbl.CreatedID,
                            UpdatedDate = Gametbl.UpdatedDate,
                            UpdatedBy = Gametbl.UpdatedBy,
                            UpdatedID = Gametbl.UpdatedID,
                            IsDelete = Gametbl.IsDelete,
                            DeleteTime = Gametbl.DeleteTime,
                            DeleteId = Gametbl.DeleteId,
                            Id = Gametbl.Id,
                            ViTriHienThi = Gametbl.ViTriHienThi,
                            STT = Gametbl.STT,

                        };

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.NameFilter))
                {
                    query = query.Where(x => x.Name.Contains(searchModel.NameFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.MoTaFilter))
                {
                    query = query.Where(x => x.MoTa.Contains(searchModel.MoTaFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
                {
                    query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
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
            var resultmodel = new PageListResultBO<GameDto>();
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

        public Game GetById(long id)
        {
            return _GameRepository.GetById(id);
        }

        public Game GetBySlug(string slug)
        {
            return _GameRepository.GetQueryable().Where(x => x.Slug.Equals(slug)).FirstOrDefault();
        }


        public List<GameDto> GetListGame()
        {
            var danhMucGameQuery = _danhMucGameTaiKhoanRepository.GetQueryable();
            var query = from game in _GameRepository.GetQueryable().OrderBy(x => x.STT)

                        join danhMucGame in _danhMucGameRepository.GetQueryable()
                        on game.Id equals danhMucGame.GameId into danhMucGameGrp

                        select new GameDto
                        {
                            Id = game.Id,
                            Name = game.Name,
                            Slug = game.Slug,
                            ViTriHienThi = game.ViTriHienThi,
                            ListDanhMucGame = danhMucGameGrp.Select(dm => new DanhMucGameDto
                            {
                                Id = dm.Id,
                                Name = dm.Name,
                                DuongDanAnh = dm.DuongDanAnh,
                                MoTa = dm.MoTa,
                                Slug = dm.Slug,
                                SoLuongTaiKhoan = (from tkdm in danhMucGameQuery
                                                   where tkdm.DanhMucGameId == dm.Id
                                                   select tkdm.TaiKhoanId).Count()
                            }).ToList()
                        };

            return query.ToList();
        }

        public GameDto GetListGameById(int id)
        {
            var danhMucGameQuery = _danhMucGameTaiKhoanRepository.GetQueryable();
            var query = from game in _GameRepository.GetQueryable().OrderBy(x => x.STT)

                        join danhMucGame in _danhMucGameRepository.GetQueryable()
                        on game.Id equals danhMucGame.GameId into danhMucGameGrp

                        select new GameDto
                        {
                            Id = game.Id,
                            Name = game.Name,
                            Slug = game.Slug,
                            ViTriHienThi = game.ViTriHienThi,
                            ListDanhMucGame = danhMucGameGrp.Select(dm => new DanhMucGameDto
                            {
                                Id = dm.Id,
                                Name = dm.Name,
                                Slug = dm.Slug,
                                DuongDanAnh = dm.DuongDanAnh,
                                MoTa = dm.MoTa,
                                SoLuongTaiKhoan = (from tkdm in danhMucGameQuery
                                                   where tkdm.DanhMucGameId == dm.Id
                                                   select tkdm.TaiKhoanId).Count()
                            }).ToList()
                        };

            return query.Where(x => x.Id == id).FirstOrDefault();
        }


        public PageListResultBO<TaiKhoanDto> GetTaiKhoanPagedByDanhMucSlug(string slug, TaiKhoanSearchDto search, int pageIndex, int pageSize)
        {
            var danhMucGame = _danhMucGameRepository.GetQueryable()
                                                       .FirstOrDefault(x => x.Slug.Equals(slug));

            var query = from tk in _taiKhoanRepository.GetQueryable()
                        .Where(x => x.TrangThai != TrangThaiTaiKhoanConstant.DABAN)

                        join danhMucGameTaiKhoan in _danhMucGameTaiKhoanRepository.GetQueryable()
                        .Where(x => x.DanhMucGameId == danhMucGame.Id)
                        on tk.Id equals danhMucGameTaiKhoan.TaiKhoanId

                        select new TaiKhoanDto
                        {
                            Id = tk.Id,
                            Code = tk.Code,
                            GameId = tk.GameId,
                            TrangThai = tk.TrangThai,
                            UserName = tk.UserName,
                            Password = tk.Password,
                            GiaGoc = tk.GiaGoc,
                            GiaKhuyenMai = tk.GiaKhuyenMai,
                            Mota = tk.Mota,
                            ViTri = tk.ViTri,
                        };

            // Filtering
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.UserNameFilter))
                {
                    query = query.Where(x => x.UserName.Contains(search.UserNameFilter));
                }

                if (!string.IsNullOrEmpty(search.CodeFilter))
                {
                    query = query.Where(x => x.Code.Contains(search.CodeFilter));
                }

                if (!string.IsNullOrEmpty(search.TrangThaiFilter))
                {
                    query = query.Where(x => x.TrangThai.Contains(search.TrangThaiFilter));
                }

                // Filter khoảng giá
                if (search.GiaMin.HasValue)
                {
                    query = query.Where(x => x.GiaKhuyenMai >= search.GiaMin.Value);
                }
                if (search.GiaMax.HasValue && search.GiaMax.Value > 0)
                {
                    query = query.Where(x => x.GiaKhuyenMai <= search.GiaMax.Value);
                }

                // Sorting
                if (!string.IsNullOrEmpty(search.sortQuery))
                {
                    switch (search.sortQuery)
                    {
                        case "price_start":
                            query = query.OrderByDescending(x => x.GiaKhuyenMai);
                            break;
                        case "price_end":
                            query = query.OrderBy(x => x.GiaKhuyenMai);
                            break;
                        case "created_at_start":
                            query = query.OrderByDescending(x => x.Id);
                            break;
                        case "created_at_end":
                            query = query.OrderBy(x => x.Id);
                            break;
                        case "random":
                            query = query.OrderBy(x => Guid.NewGuid());
                            break;
                        default:
                            query = query.OrderByDescending(x => x.Id);
                            break;
                    }
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

            // Paging
            var result = new PageListResultBO<TaiKhoanDto>();
            if (search.pageSize == -1) // l?y all
            {
                var dataList = query.ToList();
                result.Count = dataList.Count;
                result.TotalPage = 1;
                result.ListItem = dataList;
                result.CurrentPage = 1;
            }
            else
            {
                var dataPaged = query.ToPagedList(pageIndex, pageSize);
                result.Count = dataPaged.TotalItemCount;
                result.TotalPage = dataPaged.PageCount;
                result.ListItem = dataPaged.ToList();
                result.CurrentPage = pageIndex;
            }

            return result;
        }

        public List<DanhMucGameDto> GetListDanhMucGameBySlug(string gameSlug)//GameSlug
        {
            var danhMucGameQuery = _danhMucGameTaiKhoanRepository.GetQueryable();

            var query = from g in _GameRepository.GetQueryable()
                        join dm in _danhMucGameRepository.GetQueryable()
                            on g.Id equals dm.GameId
                        where g.Slug == gameSlug
                        select new DanhMucGameDto
                        {
                            Id = dm.Id,
                            Name = dm.Name,
                            Slug = dm.Slug,
                            DuongDanAnh = dm.DuongDanAnh,
                            MoTa = dm.MoTa,
                            SoLuongTaiKhoan = (from tkdm in danhMucGameQuery
                                               where tkdm.DanhMucGameId == dm.Id
                                               select tkdm.TaiKhoanId).Count()
                        };

            return query.ToList();
        }


        public List<DanhMucGameDto> GetListDanhMucGameKhac(int id, int? take)
        {
            var danhMucGameQuery = _danhMucGameTaiKhoanRepository.GetQueryable();

            var query = from dm in _danhMucGameRepository.GetQueryable()
                        where dm.Id != id   // lo?i b? danh m?c hi?n t?i
                        orderby dm.Id
                        select new DanhMucGameDto
                        {
                            Id = dm.Id,
                            Name = dm.Name,
                            Slug = dm.Slug,
                            DuongDanAnh = dm.DuongDanAnh,
                            MoTa = dm.MoTa,
                            SoLuongTaiKhoan = (from tkdm in danhMucGameQuery
                                               where tkdm.DanhMucGameId == dm.Id
                                               select tkdm.TaiKhoanId).Count()
                        };
            if(take != null)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }

        public TaiKhoanDto GetTaiKhoanByCode(string code)
        {
            var tlQuery = _taiLieuDinhKemRepository.GetQueryable();
            var query = from tk in _taiKhoanRepository.GetQueryable()
                        .Where(x => x.TrangThai != TrangThaiTaiKhoanConstant.DABAN && x.Code == code)

                        join danhMucGameTaiKhoan in _danhMucGameTaiKhoanRepository.GetQueryable()
                        on tk.Id equals danhMucGameTaiKhoan.TaiKhoanId into dmgtGrp
                        from danhMucGameTaiKhoan in dmgtGrp.DefaultIfEmpty()

                        join danhMucGame in _danhMucGameRepository.GetQueryable()
                        on danhMucGameTaiKhoan.DanhMucGameId equals danhMucGame.Id into dmgGrp
                        from danhMucGame in dmgGrp.DefaultIfEmpty()

                        join game in _GameRepository.GetQueryable()
                        on tk.GameId equals game.Id

                        select new TaiKhoanDto
                        {
                            Id = tk.Id,
                            Code = tk.Code,
                            GameId = tk.GameId,
                            TrangThai = tk.TrangThai,
                            UserName = tk.UserName,
                            Password = tk.Password,
                            GiaGoc = tk.GiaGoc,
                            GiaKhuyenMai = tk.GiaKhuyenMai,
                            Mota = tk.Mota,
                            ViTri = tk.ViTri,
                            Game = game,
                            DanhMucGame = danhMucGame,
                            TaiLieuDinhKemList = tlQuery.Where(x => x.Item_ID == tk.Id).ToList()
                        };
            return query.FirstOrDefault();
        }

        public List<TaiKhoan> GetListTaiKhoanDaXem(List<long> daXemIds)
        {
            var listTk = _taiKhoanRepository.GetQueryable()
                  .Where(x => daXemIds.Contains(x.Id))
                  .ToList();
            listTk = daXemIds.Select(id => listTk.FirstOrDefault(x => x.Id == id))
                  .Where(x => x != null)
                  .ToList();
            return listTk;
        }

        public List<TaiKhoan> GetListTaiKhoanLienQuan(long id)
        {
            var danhMucIds = _danhMucGameTaiKhoanRepository.GetQueryable()
                                    .Where(x => x.TaiKhoanId == id)
                                    .Select(x => x.DanhMucGameId)
                                    .ToList();

            var listTk = (from tkdm in _danhMucGameTaiKhoanRepository.GetQueryable()
                          join tk in _taiKhoanRepository.GetQueryable() on tkdm.TaiKhoanId equals tk.Id
                          where danhMucIds.Contains(tkdm.DanhMucGameId)
                                && tk.Id != id
                          select tk)
                 .Distinct()
                 .Take(5) 
                 .ToList();
            return listTk;
        }
    }
}
