using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.MaGiamGiaRepository;
using Hinet.Service.MaGiamGiaService.Dto;
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




namespace Hinet.Service.MaGiamGiaService
{
    public class MaGiamGiaService : EntityService<MaGiamGia>, IMaGiamGiaService
    {
        IUnitOfWork _unitOfWork;
        IMaGiamGiaRepository _MaGiamGiaRepository;
	ILog _loger;
        IMapper _mapper;


        
        public MaGiamGiaService(IUnitOfWork unitOfWork, 
		IMaGiamGiaRepository MaGiamGiaRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, MaGiamGiaRepository)
        {
            _unitOfWork = unitOfWork;
            _MaGiamGiaRepository = MaGiamGiaRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<MaGiamGiaDto> GetDaTaByPage(MaGiamGiaSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from MaGiamGiatbl in _MaGiamGiaRepository.GetAllAsQueryable()

                        select new MaGiamGiaDto
                        {
							SoLuong = MaGiamGiatbl.SoLuong,
							TuNgay = MaGiamGiatbl.TuNgay,
							DenNgay = MaGiamGiatbl.DenNgay,
							ToanHeThong = MaGiamGiatbl.ToanHeThong,
							TrangThai = MaGiamGiatbl.TrangThai,
							ThongTin = MaGiamGiatbl.ThongTin,
							GianHangApDung = MaGiamGiatbl.GianHangApDung,
							CreatedBy = MaGiamGiatbl.CreatedBy,
							UpdatedBy = MaGiamGiatbl.UpdatedBy,
							Id = MaGiamGiatbl.Id,
							IsDelete = MaGiamGiatbl.IsDelete,
							CreatedID = MaGiamGiatbl.CreatedID,
							UpdatedID = MaGiamGiatbl.UpdatedID,
							DeleteId = MaGiamGiatbl.DeleteId,
							CreatedDate = MaGiamGiatbl.CreatedDate,
							UpdatedDate = MaGiamGiatbl.UpdatedDate,
							DeleteTime = MaGiamGiatbl.DeleteTime
                            
                        };

            if (searchModel != null)
            {
		if (searchModel.SoLuongFilter!=null)
		{
			query = query.Where(x => x.SoLuong==searchModel.SoLuongFilter);
		}
		if (searchModel.TuNgayFilter!=null)
		{
			query = query.Where(x => x.TuNgay==searchModel.TuNgayFilter);
		}
		if (searchModel.DenNgayFilter!=null)
		{
			query = query.Where(x => x.DenNgay==searchModel.DenNgayFilter);
		}
		if (searchModel.ToanHeThongFilter!=null)
		{
			query = query.Where(x => x.ToanHeThong==searchModel.ToanHeThongFilter);
		}
		if (searchModel.TrangThaiFilter!=null)
		{
			query = query.Where(x => x.TrangThai==searchModel.TrangThaiFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.ThongTinFilter))
		{
			query = query.Where(x => x.ThongTin.Contains(searchModel.ThongTinFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.GianHangApDungFilter))
		{
			query = query.Where(x => x.GianHangApDung.Contains(searchModel.GianHangApDungFilter));
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
            var resultmodel = new PageListResultBO<MaGiamGiaDto>();
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

        public MaGiamGia GetById(long id)
        {
            return _MaGiamGiaRepository.GetById(id);
        }
    

    }
}
