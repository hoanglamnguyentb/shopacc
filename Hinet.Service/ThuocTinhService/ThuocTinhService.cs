using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.ThuocTinhRepository;
using Hinet.Service.ThuocTinhService.Dto;
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




namespace Hinet.Service.ThuocTinhService
{
    public class ThuocTinhService : EntityService<ThuocTinh>, IThuocTinhService
    {
        IUnitOfWork _unitOfWork;
        IThuocTinhRepository _ThuocTinhRepository;
	ILog _loger;
        IMapper _mapper;


        
        public ThuocTinhService(IUnitOfWork unitOfWork, 
		IThuocTinhRepository ThuocTinhRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, ThuocTinhRepository)
        {
            _unitOfWork = unitOfWork;
            _ThuocTinhRepository = ThuocTinhRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<ThuocTinhDto> GetDaTaByPage(ThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from ThuocTinhtbl in _ThuocTinhRepository.GetAllAsQueryable()

                        select new ThuocTinhDto
                        {
							GameId = ThuocTinhtbl.GameId,
							TenThuocTinh = ThuocTinhtbl.TenThuocTinh,
							KieuDuLieu = ThuocTinhtbl.KieuDuLieu,
							DmNhomDanhmuc = ThuocTinhtbl.DmNhomDanhmuc,
							CreatedDate = ThuocTinhtbl.CreatedDate,
							CreatedBy = ThuocTinhtbl.CreatedBy,
							CreatedID = ThuocTinhtbl.CreatedID,
							UpdatedDate = ThuocTinhtbl.UpdatedDate,
							UpdatedBy = ThuocTinhtbl.UpdatedBy,
							UpdatedID = ThuocTinhtbl.UpdatedID,
							IsDelete = ThuocTinhtbl.IsDelete,
							DeleteTime = ThuocTinhtbl.DeleteTime,
							DeleteId = ThuocTinhtbl.DeleteId,
							Id = ThuocTinhtbl.Id
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.GameIdFilter!=null)
		{
			query = query.Where(x => x.GameId==searchModel.GameIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.TenThuocTinhFilter))
		{
			query = query.Where(x => x.TenThuocTinh.Contains(searchModel.TenThuocTinhFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.KieuDuLieuFilter))
		{
			query = query.Where(x => x.KieuDuLieu.Contains(searchModel.KieuDuLieuFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.DmNhomDanhmucFilter))
		{
			query = query.Where(x => x.DmNhomDanhmuc.Contains(searchModel.DmNhomDanhmucFilter));
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
            var resultmodel = new PageListResultBO<ThuocTinhDto>();
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

        public ThuocTinh GetById(long id)
        {
            return _ThuocTinhRepository.GetById(id);
        }

        public void DeleteByGameId(long gameId)
        {
            var list = _ThuocTinhRepository.GetQueryable().Where(x => x.GameId == gameId);
            _ThuocTinhRepository.DeleteRange(list);
            _ThuocTinhRepository.Save();
        }
    }
}
