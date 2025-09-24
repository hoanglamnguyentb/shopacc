using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.TinTucRepository;
using Hinet.Service.TinTucService.Dto;
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




namespace Hinet.Service.TinTucService
{
    public class TinTucService : EntityService<TinTuc>, ITinTucService
    {
        IUnitOfWork _unitOfWork;
        ITinTucRepository _TinTucRepository;
	ILog _loger;
        IMapper _mapper;


        
        public TinTucService(IUnitOfWork unitOfWork, 
		ITinTucRepository TinTucRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, TinTucRepository)
        {
            _unitOfWork = unitOfWork;
            _TinTucRepository = TinTucRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<TinTucDto> GetDaTaByPage(TinTucSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from TinTuctbl in _TinTucRepository.GetAllAsQueryable()

                        select new TinTucDto
                        {
							Slug = TinTuctbl.Slug,
							TieuDe = TinTuctbl.TieuDe,
							NoiDung = TinTuctbl.NoiDung,
							AnhBia = TinTuctbl.AnhBia,
							TacGia = TinTuctbl.TacGia,
							TrangThai = TinTuctbl.TrangThai,
							ThoiGianXuatBan = TinTuctbl.ThoiGianXuatBan,
							CreatedDate = TinTuctbl.CreatedDate,
							CreatedBy = TinTuctbl.CreatedBy,
							CreatedID = TinTuctbl.CreatedID,
							UpdatedDate = TinTuctbl.UpdatedDate,
							UpdatedBy = TinTuctbl.UpdatedBy,
							UpdatedID = TinTuctbl.UpdatedID,
							IsDelete = TinTuctbl.IsDelete,
							DeleteTime = TinTuctbl.DeleteTime,
							DeleteId = TinTuctbl.DeleteId,
							Id = TinTuctbl.Id
                            
                        };

            if (searchModel != null)
            {
		if (!string.IsNullOrEmpty(searchModel.SlugFilter))
		{
			query = query.Where(x => x.Slug.Contains(searchModel.SlugFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.TieuDeFilter))
		{
			query = query.Where(x => x.TieuDe.Contains(searchModel.TieuDeFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.NoiDungFilter))
		{
			query = query.Where(x => x.NoiDung.Contains(searchModel.NoiDungFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.AnhBiaFilter))
		{
			query = query.Where(x => x.AnhBia.Contains(searchModel.AnhBiaFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.TacGiaFilter))
		{
			query = query.Where(x => x.TacGia.Contains(searchModel.TacGiaFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
		{
			query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
		}
		if (searchModel.ThoiGianXuatBanFilter!=null)
		{
			query = query.Where(x => x.ThoiGianXuatBan==searchModel.ThoiGianXuatBanFilter);
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
            var resultmodel = new PageListResultBO<TinTucDto>();
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

        public TinTuc GetById(long id)
        {
            return _TinTucRepository.GetById(id);
        }
    

    }
}
