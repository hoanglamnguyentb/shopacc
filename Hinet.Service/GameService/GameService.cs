using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhMucGameRepository;
using Hinet.Repository.DanhMucGameTaiKhoanRepository;
using Hinet.Repository.GameRepository;
using Hinet.Service.Common;
using Hinet.Service.DanhMucGameService.Dto;
using Hinet.Service.GameService.Dto;
using log4net;
using PagedList;
using System.Collections.Generic;
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
        public GameService(IUnitOfWork unitOfWork,
                IGameRepository GameRepository,
                ILog loger,
                IMapper mapper,
                IDanhMucGameRepository danhMucGameRepository,
                IDanhMucGameTaiKhoanRepository danhMucGameTaiKhoanRepository)
            : base(unitOfWork, GameRepository)
        {
            _unitOfWork = unitOfWork;
            _GameRepository = GameRepository;
            _loger = loger;
            _mapper = mapper;
            _danhMucGameRepository = danhMucGameRepository;
            _danhMucGameTaiKhoanRepository = danhMucGameTaiKhoanRepository;
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
                            ViTriHienThi = game.ViTriHienThi,
                            ListDanhMucGame = danhMucGameGrp.Select(dm => new DanhMucGameDto
                            {
                                Id = dm.Id,
                                Name = dm.Name,
                                DuongDanAnh = dm.DuongDanAnh,
                                MoTa = dm.MoTa,
                                SoLuongTaiKhoan = (from tkdm in danhMucGameQuery
                                                   where tkdm.DanhMucGameId == dm.Id
                                                   select tkdm.TaiKhoanId).Count()
                            }).ToList()
                        };

            return query.ToList();
        }

    }
}
