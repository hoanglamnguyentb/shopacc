using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.GianHangRepository;
using Hinet.Service.GianHangService.Dto;
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




namespace Hinet.Service.GianHangService
{
    public class GianHangService : EntityService<GianHang>, IGianHangService
    {
        IUnitOfWork _unitOfWork;
        IGianHangRepository _GianHangRepository;
	ILog _loger;
        IMapper _mapper;


        
        public GianHangService(IUnitOfWork unitOfWork, 
		IGianHangRepository GianHangRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, GianHangRepository)
        {
            _unitOfWork = unitOfWork;
            _GianHangRepository = GianHangRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<GianHangDto> GetDaTaByPage(GianHangSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from GianHangtbl in _GianHangRepository.GetAllAsQueryable()

                        select new GianHangDto
                        {
							STT = GianHangtbl.STT,
							Name = GianHangtbl.Name,
							MoTa = GianHangtbl.MoTa,
							KichHoat = GianHangtbl.KichHoat,
							Slug = GianHangtbl.Slug,
							AnhBia = GianHangtbl.AnhBia,
							CreatedBy = GianHangtbl.CreatedBy,
							UpdatedBy = GianHangtbl.UpdatedBy,
							CreatedDate = GianHangtbl.CreatedDate,
							UpdatedDate = GianHangtbl.UpdatedDate,
							DeleteTime = GianHangtbl.DeleteTime,
							IsDelete = GianHangtbl.IsDelete,
							CreatedID = GianHangtbl.CreatedID,
							UpdatedID = GianHangtbl.UpdatedID,
							DeleteId = GianHangtbl.DeleteId,
							Id = GianHangtbl.Id
                            
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
		if (searchModel.KichHoatFilter != null)
		{
			query = query.Where(x => x.KichHoat == searchModel.KichHoatFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.SlugFilter))
		{
			query = query.Where(x => x.Slug.Contains(searchModel.SlugFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.AnhBiaFilter))
		{
			query = query.Where(x => x.AnhBia.Contains(searchModel.AnhBiaFilter));
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
            var resultmodel = new PageListResultBO<GianHangDto>();
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

        public GianHang GetById(long id)
        {
            return _GianHangRepository.GetById(id);
        }

    }
}
