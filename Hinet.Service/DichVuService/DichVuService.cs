using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DichVuRepository;
using Hinet.Service.DichVuService.Dto;
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




namespace Hinet.Service.DichVuService
{
    public class DichVuService : EntityService<DichVu>, IDichVuService
    {
        IUnitOfWork _unitOfWork;
        IDichVuRepository _DichVuRepository;
	ILog _loger;
        IMapper _mapper;


        
        public DichVuService(IUnitOfWork unitOfWork, 
		IDichVuRepository DichVuRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, DichVuRepository)
        {
            _unitOfWork = unitOfWork;
            _DichVuRepository = DichVuRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<DichVuDto> GetDaTaByPage(DichVuSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from DichVutbl in _DichVuRepository.GetAllAsQueryable()

                        select new DichVuDto
                        {
							Name = DichVutbl.Name,
							DuongDanAnh = DichVutbl.DuongDanAnh,
							Link = DichVutbl.Link,
							KichHoat = DichVutbl.KichHoat,
							CreatedDate = DichVutbl.CreatedDate,
							CreatedBy = DichVutbl.CreatedBy,
							CreatedID = DichVutbl.CreatedID,
							UpdatedDate = DichVutbl.UpdatedDate,
							UpdatedBy = DichVutbl.UpdatedBy,
							UpdatedID = DichVutbl.UpdatedID,
							IsDelete = DichVutbl.IsDelete,
							DeleteTime = DichVutbl.DeleteTime,
							DeleteId = DichVutbl.DeleteId,
							Id = DichVutbl.Id,
                            STT = DichVutbl.STT

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
            var resultmodel = new PageListResultBO<DichVuDto>();
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

        public DichVu GetById(long id)
        {
            return _DichVuRepository.GetById(id);
        }
    

    }
}
