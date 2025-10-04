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
using Hinet.Repository.DanhmucRepository;




namespace Hinet.Service.ThuocTinhService
{
    public class ThuocTinhService : EntityService<ThuocTinh>, IThuocTinhService
    {
        IUnitOfWork _unitOfWork;
        IThuocTinhRepository _ThuocTinhRepository;
	    ILog _loger;
        IMapper _mapper;
        IDM_DulieuDanhmucRepository _dM_DulieuDanhmucRepository;

        public ThuocTinhService(IUnitOfWork unitOfWork,
                IThuocTinhRepository ThuocTinhRepository,
                ILog loger,
                IMapper mapper,
                IDM_DulieuDanhmucRepository dM_DulieuDanhmucRepository)
            : base(unitOfWork, ThuocTinhRepository)
        {
            _unitOfWork = unitOfWork;
            _ThuocTinhRepository = ThuocTinhRepository;
            _loger = loger;
            _mapper = mapper;
            _dM_DulieuDanhmucRepository = dM_DulieuDanhmucRepository;
        }

        public PageListResultBO<ThuocTinhDto> GetDaTaByPage(ThuocTinhSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var queryDanhMuc = _dM_DulieuDanhmucRepository.GetAllAsQueryable();
            var query = from ThuocTinhtbl in _ThuocTinhRepository.GetAllAsQueryable()

                        select new ThuocTinhDto
                        {
							GameId = ThuocTinhtbl.GameId,
							TenThuocTinh = ThuocTinhtbl.TenThuocTinh,
							KieuDuLieu = ThuocTinhtbl.KieuDuLieu,
							NhomDanhmucCode = ThuocTinhtbl.NhomDanhmucCode,
							NhomDanhMucId = ThuocTinhtbl.NhomDanhMucId,
							CreatedDate = ThuocTinhtbl.CreatedDate,
							CreatedBy = ThuocTinhtbl.CreatedBy,
							CreatedID = ThuocTinhtbl.CreatedID,
							UpdatedDate = ThuocTinhtbl.UpdatedDate,
							UpdatedBy = ThuocTinhtbl.UpdatedBy,
							UpdatedID = ThuocTinhtbl.UpdatedID,
							IsDelete = ThuocTinhtbl.IsDelete,
							DeleteTime = ThuocTinhtbl.DeleteTime,
							DeleteId = ThuocTinhtbl.DeleteId,
							Id = ThuocTinhtbl.Id,
                            ListDuLieuDanhMuc = queryDanhMuc.Where(x => x.Id == ThuocTinhtbl.NhomDanhMucId).ToList()
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
		            if (!string.IsNullOrEmpty(searchModel.NhomDanhmucCodeFilter))
		            {
			            query = query.Where(x => x.NhomDanhmucCode.Contains(searchModel.NhomDanhmucCodeFilter));
		            }		
                    if (searchModel.NhomDanhmucIdFilter!=null)
		            {
			            query = query.Where(x => x.NhomDanhMucId==searchModel.NhomDanhmucIdFilter);
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

        public ThuocTinhDto GetDtoById(long id)
        {
            var queryDanhMuc = _dM_DulieuDanhmucRepository.GetAllAsQueryable();
            var query = from ThuocTinhtbl in _ThuocTinhRepository.GetAllAsQueryable()
                        select new ThuocTinhDto
                        {
                            GameId = ThuocTinhtbl.GameId,
                            TenThuocTinh = ThuocTinhtbl.TenThuocTinh,
                            KieuDuLieu = ThuocTinhtbl.KieuDuLieu,
                            //DmNhomDanhmuc = ThuocTinhtbl.DmNhomDanhmuc,
                            Id = ThuocTinhtbl.Id,
                            //ListDuLieuDanhMuc = queryDanhMuc.Where(x => x.Id = ThuocTinhtbl.DmNhomDanhmuc)

                        };
            return query.FirstOrDefault();
        }

        public List<ThuocTinhDto> GetDaTaByGameId(int gameId)
        {
            var queryDanhMuc = _dM_DulieuDanhmucRepository.GetAllAsQueryable();
            var query = from ThuocTinhtbl in _ThuocTinhRepository.GetQueryable().Where(x => x.GameId == gameId)

                        select new ThuocTinhDto
                        {
                            GameId = ThuocTinhtbl.GameId,
                            TenThuocTinh = ThuocTinhtbl.TenThuocTinh,
                            KieuDuLieu = ThuocTinhtbl.KieuDuLieu,
                            NhomDanhmucCode = ThuocTinhtbl.NhomDanhmucCode,
                            NhomDanhMucId = ThuocTinhtbl.NhomDanhMucId,
                            CreatedDate = ThuocTinhtbl.CreatedDate,
                            CreatedBy = ThuocTinhtbl.CreatedBy,
                            CreatedID = ThuocTinhtbl.CreatedID,
                            UpdatedDate = ThuocTinhtbl.UpdatedDate,
                            UpdatedBy = ThuocTinhtbl.UpdatedBy,
                            UpdatedID = ThuocTinhtbl.UpdatedID,
                            IsDelete = ThuocTinhtbl.IsDelete,
                            DeleteTime = ThuocTinhtbl.DeleteTime,
                            DeleteId = ThuocTinhtbl.DeleteId,
                            Id = ThuocTinhtbl.Id,
                            ListDuLieuDanhMuc = queryDanhMuc.Where(x => x.Id == ThuocTinhtbl.NhomDanhMucId).ToList()
                        };

            return query.ToList();
        }
    }
}
