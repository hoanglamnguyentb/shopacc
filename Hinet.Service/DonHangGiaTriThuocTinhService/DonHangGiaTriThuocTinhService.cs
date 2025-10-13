using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DonHangGiaTriThuocTinhRepository;
using Hinet.Service.DonHangGiaTriThuocTinhService.Dto;
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




namespace Hinet.Service.DonHangGiaTriThuocTinhService
{
    public class DonHangGiaTriThuocTinhService : EntityService<DonHangGiaTriThuocTinh>, IDonHangGiaTriThuocTinhService
    {
        IUnitOfWork _unitOfWork;
        IDonHangGiaTriThuocTinhRepository _DonHangGiaTriThuocTinhRepository;
	ILog _loger;
        IMapper _mapper;


        
        public DonHangGiaTriThuocTinhService(IUnitOfWork unitOfWork, 
		IDonHangGiaTriThuocTinhRepository DonHangGiaTriThuocTinhRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, DonHangGiaTriThuocTinhRepository)
        {
            _unitOfWork = unitOfWork;
            _DonHangGiaTriThuocTinhRepository = DonHangGiaTriThuocTinhRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<DonHangGiaTriThuocTinhDto> GetDaTaByPage(DonHangGiaTriThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from DonHangGiaTriThuocTinhtbl in _DonHangGiaTriThuocTinhRepository.GetAllAsQueryable()

                        select new DonHangGiaTriThuocTinhDto
                        {
							DonHangId = DonHangGiaTriThuocTinhtbl.DonHangId,
							ThuocTinhId = DonHangGiaTriThuocTinhtbl.ThuocTinhId,
							ThuocTinhTxt = DonHangGiaTriThuocTinhtbl.ThuocTinhTxt,
							GiaTri = DonHangGiaTriThuocTinhtbl.GiaTri,
							GiaTriTxt = DonHangGiaTriThuocTinhtbl.GiaTriTxt,
							KieuDuLieu = DonHangGiaTriThuocTinhtbl.KieuDuLieu,
							CreatedBy = DonHangGiaTriThuocTinhtbl.CreatedBy,
							UpdatedBy = DonHangGiaTriThuocTinhtbl.UpdatedBy,
							CreatedDate = DonHangGiaTriThuocTinhtbl.CreatedDate,
							UpdatedDate = DonHangGiaTriThuocTinhtbl.UpdatedDate,
							DeleteTime = DonHangGiaTriThuocTinhtbl.DeleteTime,
							IsDelete = DonHangGiaTriThuocTinhtbl.IsDelete,
							Id = DonHangGiaTriThuocTinhtbl.Id,
							CreatedID = DonHangGiaTriThuocTinhtbl.CreatedID,
							UpdatedID = DonHangGiaTriThuocTinhtbl.UpdatedID,
							DeleteId = DonHangGiaTriThuocTinhtbl.DeleteId
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.DonHangIdFilter!=null)
		{
			query = query.Where(x => x.DonHangId==searchModel.DonHangIdFilter);
		}
		if (searchModel.ThuocTinhIdFilter!=null)
		{
			query = query.Where(x => x.ThuocTinhId==searchModel.ThuocTinhIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.ThuocTinhTxtFilter))
		{
			query = query.Where(x => x.ThuocTinhTxt.Contains(searchModel.ThuocTinhTxtFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.GiaTriFilter))
		{
			query = query.Where(x => x.GiaTri.Contains(searchModel.GiaTriFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.GiaTriTxtFilter))
		{
			query = query.Where(x => x.GiaTriTxt.Contains(searchModel.GiaTriTxtFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.KieuDuLieuFilter))
		{
			query = query.Where(x => x.KieuDuLieu.Contains(searchModel.KieuDuLieuFilter));
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
            var resultmodel = new PageListResultBO<DonHangGiaTriThuocTinhDto>();
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

        public DonHangGiaTriThuocTinh GetById(long id)
        {
            return _DonHangGiaTriThuocTinhRepository.GetById(id);
        }
    

    }
}
