using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.GiaoDichRepository;
using Hinet.Service.GiaoDichService.Dto;
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
using Hinet.Repository.AppUserRepository;
using Hinet.Repository.TaiKhoanRepository;




namespace Hinet.Service.GiaoDichService
{
    public class GiaoDichService : EntityService<GiaoDich>, IGiaoDichService
    {
        IUnitOfWork _unitOfWork;
        IGiaoDichRepository _GiaoDichRepository;
        ILog _loger;
        IMapper _mapper;
        IAppUserRepository _appUserRepository;
		ITaiKhoanRepository _taiKhoanRepository;

		public GiaoDichService(IUnitOfWork unitOfWork,
				IGiaoDichRepository GiaoDichRepository,
				ILog loger,
				IMapper mapper,
				IAppUserRepository appUserRepository,
				ITaiKhoanRepository taiKhoanRepository)
			: base(unitOfWork, GiaoDichRepository)
		{
			_unitOfWork = unitOfWork;
			_GiaoDichRepository = GiaoDichRepository;
			_loger = loger;
			_mapper = mapper;
			_appUserRepository = appUserRepository;
			_taiKhoanRepository = taiKhoanRepository;
		}

		public PageListResultBO<GiaoDichDto> GetDaTaByPage(GiaoDichSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from GiaoDichtbl in _GiaoDichRepository.GetAllAsQueryable()
                        join user in _appUserRepository.GetAllAsQueryable()
						on GiaoDichtbl.UserId equals user.Id
						join taiKhoan in _taiKhoanRepository.GetAllAsQueryable()
						on GiaoDichtbl.DoiTuongId equals taiKhoan.Id into taiKhoanGrp
						from taiKhoan in taiKhoanGrp.DefaultIfEmpty()

						select new GiaoDichDto
                        {
							UserId = GiaoDichtbl.UserId,
							DoiTuongId = GiaoDichtbl.DoiTuongId,
							LoaiDoiTuong = GiaoDichtbl.LoaiDoiTuong,
							LoaiGiaoDich = GiaoDichtbl.LoaiGiaoDich,
							TrangThai = GiaoDichtbl.TrangThai,
							PhuongThucThanhToan = GiaoDichtbl.PhuongThucThanhToan,
							NgayGiaoDich = GiaoDichtbl.NgayGiaoDich,
							NgayThanhToan = GiaoDichtbl.NgayThanhToan,
							CreatedDate = GiaoDichtbl.CreatedDate,
							CreatedBy = GiaoDichtbl.CreatedBy,
							CreatedID = GiaoDichtbl.CreatedID,
							UpdatedDate = GiaoDichtbl.UpdatedDate,
							UpdatedBy = GiaoDichtbl.UpdatedBy,
							UpdatedID = GiaoDichtbl.UpdatedID,
							IsDelete = GiaoDichtbl.IsDelete,
							DeleteTime = GiaoDichtbl.DeleteTime,
							DeleteId = GiaoDichtbl.DeleteId,
							Id = GiaoDichtbl.Id,
							NguoiGiaoDich = user.UserName,
							TaiKhoanTxt = taiKhoan.Code,
						};

            if (searchModel != null)
            {
		if (searchModel.UserIdFilter!=null)
		{
			query = query.Where(x => x.UserId==searchModel.UserIdFilter);
		}
		if (searchModel.DoiTuongIdFilter!=null)
		{
			query = query.Where(x => x.DoiTuongId==searchModel.DoiTuongIdFilter);
		}
		if (!string.IsNullOrEmpty(searchModel.LoaiDoiTuongFilter))
		{
			query = query.Where(x => x.LoaiDoiTuong.Contains(searchModel.LoaiDoiTuongFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.LoaiGiaoDichFilter))
		{
			query = query.Where(x => x.LoaiGiaoDich.Contains(searchModel.LoaiGiaoDichFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
		{
			query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
		}
		if (!string.IsNullOrEmpty(searchModel.PhuongThucThanhToanFilter))
		{
			query = query.Where(x => x.PhuongThucThanhToan.Contains(searchModel.PhuongThucThanhToanFilter));
		}
		if (searchModel.NgayGiaoDichFilter!=null)
		{
			query = query.Where(x => x.NgayGiaoDich==searchModel.NgayGiaoDichFilter);
		}
		if (searchModel.NgayThanhToanFilter!=null)
		{
			query = query.Where(x => x.NgayThanhToan==searchModel.NgayThanhToanFilter);
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
            var resultmodel = new PageListResultBO<GiaoDichDto>();
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

        public GiaoDich GetById(long id)
        {
            return _GiaoDichRepository.GetById(id);
        }
    

    }
}
