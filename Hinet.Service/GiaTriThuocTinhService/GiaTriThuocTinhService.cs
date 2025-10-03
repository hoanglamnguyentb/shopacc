using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.GiaTriThuocTinhRepository;
using Hinet.Service.GiaTriThuocTinhService.Dto;
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




namespace Hinet.Service.GiaTriThuocTinhService
{
    public class GiaTriThuocTinhService : EntityService<GiaTriThuocTinh>, IGiaTriThuocTinhService
    {
        IUnitOfWork _unitOfWork;
        IGiaTriThuocTinhRepository _GiaTriThuocTinhRepository;
	ILog _loger;
        IMapper _mapper;


        
        public GiaTriThuocTinhService(IUnitOfWork unitOfWork, 
		IGiaTriThuocTinhRepository GiaTriThuocTinhRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, GiaTriThuocTinhRepository)
        {
            _unitOfWork = unitOfWork;
            _GiaTriThuocTinhRepository = GiaTriThuocTinhRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<GiaTriThuocTinhDto> GetDaTaByPage(GiaTriThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from GiaTriThuocTinhtbl in _GiaTriThuocTinhRepository.GetAllAsQueryable()

                        select new GiaTriThuocTinhDto
                        {
							TaiKhoanId = GiaTriThuocTinhtbl.TaiKhoanId,
							ThuocTinhId = GiaTriThuocTinhtbl.ThuocTinhId,
							ThuocTinhTxt = GiaTriThuocTinhtbl.ThuocTinhTxt,
							GiaTri = GiaTriThuocTinhtbl.GiaTri,
							GiaTriText = GiaTriThuocTinhtbl.GiaTriText,
							CreatedDate = GiaTriThuocTinhtbl.CreatedDate,
							CreatedBy = GiaTriThuocTinhtbl.CreatedBy,
							CreatedID = GiaTriThuocTinhtbl.CreatedID,
							UpdatedDate = GiaTriThuocTinhtbl.UpdatedDate,
							UpdatedBy = GiaTriThuocTinhtbl.UpdatedBy,
							UpdatedID = GiaTriThuocTinhtbl.UpdatedID,
							IsDelete = GiaTriThuocTinhtbl.IsDelete,
							DeleteTime = GiaTriThuocTinhtbl.DeleteTime,
							DeleteId = GiaTriThuocTinhtbl.DeleteId,
							Id = GiaTriThuocTinhtbl.Id
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.TaiKhoanIdFilter!=null)
		{
			query = query.Where(x => x.TaiKhoanId==searchModel.TaiKhoanIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.ThuocTinhIdFilter))
		{
			query = query.Where(x => x.ThuocTinhId.Contains(searchModel.ThuocTinhIdFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.ThuocTinhTxtFilter))
		{
			query = query.Where(x => x.ThuocTinhTxt.Contains(searchModel.ThuocTinhTxtFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.GiaTriFilter))
		{
			query = query.Where(x => x.GiaTri.Contains(searchModel.GiaTriFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.GiaTriTextFilter))
		{
			query = query.Where(x => x.GiaTriText.Contains(searchModel.GiaTriTextFilter));
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
            var resultmodel = new PageListResultBO<GiaTriThuocTinhDto>();
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

        public GiaTriThuocTinh GetById(long id)
        {
            return _GiaTriThuocTinhRepository.GetById(id);
        }
    

    }
}
