using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.BinhLuanRepository;
using Hinet.Service.BinhLuanService.Dto;
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




namespace Hinet.Service.BinhLuanService
{
    public class BinhLuanService : EntityService<BinhLuan>, IBinhLuanService
    {
        IUnitOfWork _unitOfWork;
        IBinhLuanRepository _BinhLuanRepository;
	ILog _loger;
        IMapper _mapper;


        
        public BinhLuanService(IUnitOfWork unitOfWork, 
		IBinhLuanRepository BinhLuanRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, BinhLuanRepository)
        {
            _unitOfWork = unitOfWork;
            _BinhLuanRepository = BinhLuanRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<BinhLuanDto> GetDaTaByPage(BinhLuanSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from BinhLuantbl in _BinhLuanRepository.GetAllAsQueryable()

                        select new BinhLuanDto
                        {
                            Id = BinhLuantbl.Id,
                            NguoiBinhLuanId = BinhLuantbl.NguoiBinhLuanId,
							DoiTuongId = BinhLuantbl.DoiTuongId,
							LoaiDoiTuong = BinhLuantbl.LoaiDoiTuong,
							NoiDung = BinhLuantbl.NoiDung,
							Diem = BinhLuantbl.Diem,
							ParentId = BinhLuantbl.ParentId,
							TrangThai = BinhLuantbl.TrangThai,
                            CreatedDate = BinhLuantbl.CreatedDate,
                            CreatedBy = BinhLuantbl.CreatedBy,
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.NguoiBinhLuanIdFilter!=null)
		{
			query = query.Where(x => x.NguoiBinhLuanId==searchModel.NguoiBinhLuanIdFilter);
		}
		if (searchModel.DoiTuongIdFilter!=null)
		{
			query = query.Where(x => x.DoiTuongId==searchModel.DoiTuongIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.LoaiDoiTuongFilter))
		{
			query = query.Where(x => x.LoaiDoiTuong.Contains(searchModel.LoaiDoiTuongFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.NoiDungFilter))
		{
			query = query.Where(x => x.NoiDung.Contains(searchModel.NoiDungFilter));
		}
		if (searchModel.DiemFilter!=null)
		{
			query = query.Where(x => x.Diem==searchModel.DiemFilter);
		}
		if (searchModel.ParentIdFilter!=null)
		{
			query = query.Where(x => x.ParentId==searchModel.ParentIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
		{
			query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
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
            var resultmodel = new PageListResultBO<BinhLuanDto>();
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

        public BinhLuan GetById(long id)
        {
            return _BinhLuanRepository.GetById(id);
        }
    

    }
}
