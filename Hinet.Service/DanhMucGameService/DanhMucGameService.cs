using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhMucGameRepository;
using Hinet.Service.DanhMucGameService.Dto;
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




namespace Hinet.Service.DanhMucGameService
{
    public class DanhMucGameService : EntityService<DanhMucGame>, IDanhMucGameService
    {
        IUnitOfWork _unitOfWork;
        IDanhMucGameRepository _DanhMucGameRepository;
	ILog _loger;
        IMapper _mapper;


        
        public DanhMucGameService(IUnitOfWork unitOfWork, 
		IDanhMucGameRepository DanhMucGameRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, DanhMucGameRepository)
        {
            _unitOfWork = unitOfWork;
            _DanhMucGameRepository = DanhMucGameRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<DanhMucGameDto> GetDaTaByPage(DanhMucGameSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from DanhMucGametbl in _DanhMucGameRepository.GetAllAsQueryable()

                        select new DanhMucGameDto
                        {
							GameId = DanhMucGametbl.GameId,
							Name = DanhMucGametbl.Name,
							DuongDanAnh = DanhMucGametbl.DuongDanAnh,
							MoTa = DanhMucGametbl.MoTa,
							CreatedDate = DanhMucGametbl.CreatedDate,
							CreatedBy = DanhMucGametbl.CreatedBy,
							CreatedID = DanhMucGametbl.CreatedID,
							UpdatedDate = DanhMucGametbl.UpdatedDate,
							UpdatedBy = DanhMucGametbl.UpdatedBy,
							UpdatedID = DanhMucGametbl.UpdatedID,
							IsDelete = DanhMucGametbl.IsDelete,
							DeleteTime = DanhMucGametbl.DeleteTime,
							DeleteId = DanhMucGametbl.DeleteId,
							Id = DanhMucGametbl.Id,
                            LaLoaiDongGia = DanhMucGametbl.LaLoaiDongGia,
                            GiaGoc = DanhMucGametbl.GiaGoc,
                            GiaKhuyenMai = DanhMucGametbl.GiaKhuyenMai,
                        };

            if (searchModel != null)
            {
		if (searchModel.GameIdFilter!=null)
		{
			query = query.Where(x => x.GameId==searchModel.GameIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.NameFilter))
		{
			query = query.Where(x => x.Name.Contains(searchModel.NameFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.DuongDanAnhFilter))
		{
			query = query.Where(x => x.DuongDanAnh.Contains(searchModel.DuongDanAnhFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.MoTaFilter))
		{
			query = query.Where(x => x.MoTa.Contains(searchModel.MoTaFilter));
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
            var resultmodel = new PageListResultBO<DanhMucGameDto>();
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

        public DanhMucGame GetById(long id)
        {
            return _DanhMucGameRepository.GetById(id);
        }
    

        public List<DanhMucGame> GetDanhMucByGame(long gameId)
        {
            return _DanhMucGameRepository.FindBy(x => x.GameId == gameId).ToList();
        }

        public DanhMucGame GetBySlug(string slug)
        {
            return _DanhMucGameRepository
                .GetAllAsQueryable()
                .FirstOrDefault(x => x.Slug == slug);
        }
    }
}
