using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Service.TaiKhoanService.Dto;
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




namespace Hinet.Service.TaiKhoanService
{
    public class TaiKhoanService : EntityService<TaiKhoan>, ITaiKhoanService
    {
        IUnitOfWork _unitOfWork;
        ITaiKhoanRepository _TaiKhoanRepository;
	ILog _loger;
        IMapper _mapper;


        
        public TaiKhoanService(IUnitOfWork unitOfWork, 
		ITaiKhoanRepository TaiKhoanRepository, 
		ILog loger,

            	IMapper mapper	
            )
            : base(unitOfWork, TaiKhoanRepository)
        {
            _unitOfWork = unitOfWork;
            _TaiKhoanRepository = TaiKhoanRepository;
            _loger = loger;
            _mapper = mapper;



        }

        public PageListResultBO<TaiKhoanDto> GetDaTaByPage(TaiKhoanSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from TaiKhoantbl in _TaiKhoanRepository.GetAllAsQueryable()

                        select new TaiKhoanDto
                        {
							Code = TaiKhoantbl.Code,
							GameId = TaiKhoantbl.GameId,
							TrangThai = TaiKhoantbl.TrangThai,
							UserName = TaiKhoantbl.UserName,
							Password = TaiKhoantbl.Password,
							GiaGoc = TaiKhoantbl.GiaGoc,
							GiaKhuyenMai = TaiKhoantbl.GiaKhuyenMai,
							Mota = TaiKhoantbl.Mota,
							ViTri = TaiKhoantbl.ViTri,
							CreatedDate = TaiKhoantbl.CreatedDate,
							CreatedBy = TaiKhoantbl.CreatedBy,
							CreatedID = TaiKhoantbl.CreatedID,
							UpdatedDate = TaiKhoantbl.UpdatedDate,
							UpdatedBy = TaiKhoantbl.UpdatedBy,
							UpdatedID = TaiKhoantbl.UpdatedID,
							IsDelete = TaiKhoantbl.IsDelete,
							DeleteTime = TaiKhoantbl.DeleteTime,
							DeleteId = TaiKhoantbl.DeleteId,
							Id = TaiKhoantbl.Id
                            
                        };

            if (searchModel != null)
            {
		if (!string.IsNullOrEmpty(searchModel.CodeFilter))
		{
			query = query.Where(x => x.Code.Contains(searchModel.CodeFilter));
		}
		if (searchModel.GameIdFilter!=null)
		{
			query = query.Where(x => x.GameId==searchModel.GameIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
		{
			query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.UserNameFilter))
		{
			query = query.Where(x => x.UserName.Contains(searchModel.UserNameFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.PasswordFilter))
		{
			query = query.Where(x => x.Password.Contains(searchModel.PasswordFilter));
		}
		if (searchModel.GiaGocFilter!=null)
		{
			query = query.Where(x => x.GiaGoc==searchModel.GiaGocFilter);
		}
		if (searchModel.GiaKhuyenMaiFilter!=null)
		{
			query = query.Where(x => x.GiaKhuyenMai==searchModel.GiaKhuyenMaiFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.MotaFilter))
		{
			query = query.Where(x => x.Mota.Contains(searchModel.MotaFilter));
		}
		if (searchModel.ViTriFilter!=null)
		{
			query = query.Where(x => x.ViTri==searchModel.ViTriFilter);
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
            var resultmodel = new PageListResultBO<TaiKhoanDto>();
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

        public TaiKhoan GetById(long id)
        {
            return _TaiKhoanRepository.GetById(id);
        }
    

    }
}
