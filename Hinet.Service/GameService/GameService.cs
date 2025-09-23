using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.GameRepository;
using Hinet.Service.GameService.Dto;
using Hinet.Service.Common;
using System.Linq.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using AutoMapper;
using Hinet.Service.Constant;




namespace Hinet.Service.GameService
{
    public class GameService : EntityService<Game>, IGameService
    {
        IUnitOfWork _unitOfWork;
        IGameRepository _GameRepository;
	ILog _loger;
        IMapper _mapper;


        
        public GameService(IUnitOfWork unitOfWork, 
		IGameRepository GameRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, GameRepository)
        {
            _unitOfWork = unitOfWork;
            _GameRepository = GameRepository;
            _loger = loger;
            _mapper = mapper;



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
							Id = Gametbl.Id
                            
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
    

    }
}
