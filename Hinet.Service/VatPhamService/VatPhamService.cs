using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.VatPhamRepository;
using Hinet.Service.VatPhamService.Dto;
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




namespace Hinet.Service.VatPhamService
{
    public class VatPhamService : EntityService<VatPham>, IVatPhamService
    {
        IUnitOfWork _unitOfWork;
        IVatPhamRepository _VatPhamRepository;
	ILog _loger;
        IMapper _mapper;


        
        public VatPhamService(IUnitOfWork unitOfWork, 
		IVatPhamRepository VatPhamRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, VatPhamRepository)
        {
            _unitOfWork = unitOfWork;
            _VatPhamRepository = VatPhamRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<VatPhamDto> GetDaTaByPage(VatPhamSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from VatPhamtbl in _VatPhamRepository.GetAllAsQueryable()

                        select new VatPhamDto
                        {
							GianHangId = VatPhamtbl.GianHangId,
							GiaGoc = VatPhamtbl.GiaGoc,
							STT = VatPhamtbl.STT,
							Name = VatPhamtbl.Name,
							DuongDanAnh = VatPhamtbl.DuongDanAnh,
							MoTa = VatPhamtbl.MoTa,
							Slug = VatPhamtbl.Slug,
							CreatedBy = VatPhamtbl.CreatedBy,
							UpdatedBy = VatPhamtbl.UpdatedBy,
							Id = VatPhamtbl.Id,
							CreatedDate = VatPhamtbl.CreatedDate,
							UpdatedDate = VatPhamtbl.UpdatedDate,
							DeleteTime = VatPhamtbl.DeleteTime,
							IsDelete = VatPhamtbl.IsDelete,
							CreatedID = VatPhamtbl.CreatedID,
							UpdatedID = VatPhamtbl.UpdatedID,
							DeleteId = VatPhamtbl.DeleteId
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.GianHangIdFilter!=null)
		{
			query = query.Where(x => x.GianHangId==searchModel.GianHangIdFilter);
		}
		if (searchModel.GiaGocFilter!=null)
		{
			query = query.Where(x => x.GiaGoc==searchModel.GiaGocFilter);
		}
		if (searchModel.STTFilter!=null)
		{
			query = query.Where(x => x.STT==searchModel.STTFilter);
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
		if (!string.IsNullOrEmpty(searchModel.SlugFilter))
		{
			query = query.Where(x => x.Slug.Contains(searchModel.SlugFilter));
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
            var resultmodel = new PageListResultBO<VatPhamDto>();
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

        public VatPham GetById(long id)
        {
            return _VatPhamRepository.GetById(id);
        }
    

    }
}
