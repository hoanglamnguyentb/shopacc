using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.BannerRepository;
using Hinet.Service.BannerService.Dto;
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




namespace Hinet.Service.BannerService
{
    public class BannerService : EntityService<Banner>, IBannerService
    {
        IUnitOfWork _unitOfWork;
        IBannerRepository _BannerRepository;
	ILog _loger;
        IMapper _mapper;


        
        public BannerService(IUnitOfWork unitOfWork, 
		IBannerRepository BannerRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, BannerRepository)
        {
            _unitOfWork = unitOfWork;
            _BannerRepository = BannerRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<BannerDto> GetDaTaByPage(BannerSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from Bannertbl in _BannerRepository.GetAllAsQueryable()

                        select new BannerDto
                        {
							Name = Bannertbl.Name,
							DuongDanAnh = Bannertbl.DuongDanAnh,
							Link = Bannertbl.Link,
							KichHoat = Bannertbl.KichHoat,
							CreatedDate = Bannertbl.CreatedDate,
							CreatedBy = Bannertbl.CreatedBy,
							CreatedID = Bannertbl.CreatedID,
							UpdatedDate = Bannertbl.UpdatedDate,
							UpdatedBy = Bannertbl.UpdatedBy,
							UpdatedID = Bannertbl.UpdatedID,
							IsDelete = Bannertbl.IsDelete,
							DeleteTime = Bannertbl.DeleteTime,
							DeleteId = Bannertbl.DeleteId,
							Id = Bannertbl.Id,
                            STT = Bannertbl.STT
                        };

            if (searchModel != null)
            {
		if (!string.IsNullOrEmpty(searchModel.NameFilter))
		{
			query = query.Where(x => x.Name.Contains(searchModel.NameFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.DuongDanAnhFilter))
		{
			query = query.Where(x => x.DuongDanAnh.Contains(searchModel.DuongDanAnhFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.LinkFilter))
		{
			query = query.Where(x => x.Link.Contains(searchModel.LinkFilter));
		}
		if (searchModel.KichHoatFilter!=null)
		{
			query = query.Where(x => x.KichHoat==searchModel.KichHoatFilter);
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
            var resultmodel = new PageListResultBO<BannerDto>();
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

        public Banner GetById(long id)
        {
            return _BannerRepository.GetById(id);
        }
    

    }
}
