using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Hinet.Repository;
using Hinet.Repository.AppUserRepository;
using Hinet.Repository.DonHangRepository;
using Hinet.Repository.GiaoDichRepository;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Repository.VatPhamRepository;
using Hinet.Service.Common;
using Hinet.Service.Constant;
using Hinet.Service.GiaoDichService.Dto;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;




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
        IDonHangRepository _donHangRepository;
        IVatPhamRepository _vatPhamRepository;

        public GiaoDichService(IUnitOfWork unitOfWork,
                IGiaoDichRepository GiaoDichRepository,
                ILog loger,
                IMapper mapper,
                IAppUserRepository appUserRepository,
                ITaiKhoanRepository taiKhoanRepository,
                IDonHangRepository donHangRepository,
                IVatPhamRepository vatPhamRepository)
            : base(unitOfWork, GiaoDichRepository)
        {
            _unitOfWork = unitOfWork;
            _GiaoDichRepository = GiaoDichRepository;
            _loger = loger;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _taiKhoanRepository = taiKhoanRepository;
            _donHangRepository = donHangRepository;
            _vatPhamRepository = vatPhamRepository;
        }

        public PageListResultBO<GiaoDichDto> GetDaTaByPage(GiaoDichSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from GiaoDichtbl in _GiaoDichRepository.GetAllAsQueryable()
                        join user in _appUserRepository.GetAllAsQueryable()
                        on GiaoDichtbl.NguoiGiaoDich equals user.Id

                        join taiKhoan in _taiKhoanRepository.GetAllAsQueryable()
                        on GiaoDichtbl.DoiTuongId equals taiKhoan.Id into taiKhoanGrp
                        from taiKhoan in taiKhoanGrp.DefaultIfEmpty()

                        join gianHang in _donHangRepository.GetAllAsQueryable()
                        on GiaoDichtbl.DoiTuongId equals gianHang.Id into gianHangGrp
                        from gianHang in gianHangGrp.DefaultIfEmpty()

                        join vatPham in _vatPhamRepository.GetAllAsQueryable()
                        on gianHang.VatPhamId equals vatPham.Id into vatPhamGrp
                        from vatPham in vatPhamGrp.DefaultIfEmpty()

                        select new GiaoDichDto
                        {
                            NguoiGiaoDich = GiaoDichtbl.NguoiGiaoDich,
                            DoiTuongId = GiaoDichtbl.DoiTuongId,
                            LoaiDoiTuong = GiaoDichtbl.LoaiDoiTuong,
                            LoaiGiaoDich = GiaoDichtbl.LoaiGiaoDich,
                            TrangThai = GiaoDichtbl.TrangThai,
                            PhuongThucThanhToan = GiaoDichtbl.PhuongThucThanhToan,
                            NgayGiaoDich = GiaoDichtbl.NgayGiaoDich,
                            NgayXuLy = GiaoDichtbl.NgayXuLy,
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
                            NguoiGiaoDichTxt = user.FullName ?? user.UserName,
                            TaiKhoanTxt = taiKhoan.Code,
                            NoiDung = GiaoDichtbl.NoiDung,
                            SoTien = GiaoDichtbl.SoTien,
                            TenTaiKhoanCanNap = GiaoDichtbl.TenTaiKhoanCanNap,
                            MatKhauTaiKhoanNap = GiaoDichtbl.MatKhauTaiKhoanNap,
                            NoiDungChuyenKhoan = GiaoDichtbl.NoiDungChuyenKhoan,
                            MaGiaoDich = GiaoDichtbl.MaGiaoDich,
                        };
            query = query.Where(x => x.TrangThai != TrangThaiGiaoDichConstant.KHOITAO);

            if (searchModel != null)
            {
                if (searchModel.UserIdFilter != null)
                {
                    query = query.Where(x => x.NguoiGiaoDich == searchModel.UserIdFilter);
                }
                if (searchModel.DoiTuongIdFilter != null)
                {
                    query = query.Where(x => x.DoiTuongId == searchModel.DoiTuongIdFilter);
                }
                if (!string.IsNullOrEmpty(searchModel.LoaiDoiTuongFilter))
                {
                    query = query.Where(x => x.LoaiDoiTuong.Contains(searchModel.LoaiDoiTuongFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.LoaiGiaoDichFilter))
                {
                    query = query.Where(x => x.LoaiGiaoDich.Equals(searchModel.LoaiGiaoDichFilter));
                }
                if (searchModel.ListLoaiGiaoDichFilter != null)
                {
                    query = query.Where(x => searchModel.ListLoaiGiaoDichFilter.Contains(x.LoaiGiaoDich));
                }
                if (!string.IsNullOrEmpty(searchModel.TrangThaiFilter))
                {
                    query = query.Where(x => x.TrangThai.Contains(searchModel.TrangThaiFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.PhuongThucThanhToanFilter))
                {
                    query = query.Where(x => x.PhuongThucThanhToan.Contains(searchModel.PhuongThucThanhToanFilter));
                }
                if (searchModel.NgayGiaoDichFilter != null)
                {
                    var date = searchModel.NgayGiaoDichFilter.Value.Date;
                    query = query.Where(x => DbFunctions.TruncateTime(x.NgayGiaoDich) == date);
                }

                if (searchModel.NgayXuLyFilter != null)
                {
                    var date = searchModel.NgayXuLyFilter.Value.Date;
                    query = query.Where(x => x.NgayXuLy.HasValue &&
                                             DbFunctions.TruncateTime(x.NgayXuLy.Value) == date);
                }
                if (searchModel.TuNgayFilter != null)
                {
                    var tuNgay = searchModel.TuNgayFilter.Value.Date;
                    query = query.Where(x => DbFunctions.TruncateTime(x.NgayGiaoDich) >= tuNgay);
                }

                if (searchModel.DenNgayFilter != null)
                {
                    var denNgay = searchModel.DenNgayFilter.Value.Date;
                    query = query.Where(x => DbFunctions.TruncateTime(x.NgayGiaoDich) <= denNgay);
                }
                if (!string.IsNullOrEmpty(searchModel.KeyWord))
                {
                    var keyWord = searchModel.KeyWord.ToUpper();
                    query = query.Where(x => x.NoiDungChuyenKhoan.ToUpper().Contains(keyWord)
                    || x.NoiDungChuyenKhoan.ToUpper().Contains(keyWord)
                    || x.NguoiGiaoDichTxt.ToUpper().Contains(keyWord)
                    || x.NguoiGiaoDich.ToString().ToUpper().Contains(keyWord));
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
                resultmodel.CurrentPage = 1;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = dataPageList;
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.CurrentPage = pageIndex;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = dataPageList.ToList();
            }
            return resultmodel;
        }

        public GiaoDichDto GetDtoById(long id)
        {
            var query = from GiaoDichtbl in _GiaoDichRepository.GetAllAsQueryable()
                        join user in _appUserRepository.GetAllAsQueryable()
                        on GiaoDichtbl.NguoiGiaoDich equals user.Id

                        join taiKhoan in _taiKhoanRepository.GetAllAsQueryable()
                        on GiaoDichtbl.DoiTuongId equals taiKhoan.Id into taiKhoanGrp
                        from taiKhoan in taiKhoanGrp.DefaultIfEmpty()

                        join gianHang in _donHangRepository.GetAllAsQueryable()
                        on GiaoDichtbl.DoiTuongId equals gianHang.Id into gianHangGrp
                        from gianHang in gianHangGrp.DefaultIfEmpty()

                        join vatPham in _vatPhamRepository.GetAllAsQueryable()
                        on gianHang.VatPhamId equals vatPham.Id into vatPhamGrp
                        from vatPham in vatPhamGrp.DefaultIfEmpty()

                        select new GiaoDichDto
                        {
                            NguoiGiaoDich = GiaoDichtbl.NguoiGiaoDich,
                            DoiTuongId = GiaoDichtbl.DoiTuongId,
                            LoaiDoiTuong = GiaoDichtbl.LoaiDoiTuong,
                            LoaiGiaoDich = GiaoDichtbl.LoaiGiaoDich,
                            TrangThai = GiaoDichtbl.TrangThai,
                            PhuongThucThanhToan = GiaoDichtbl.PhuongThucThanhToan,
                            NgayGiaoDich = GiaoDichtbl.NgayGiaoDich,
                            NgayXuLy = GiaoDichtbl.NgayXuLy,
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
                            NguoiGiaoDichTxt = user.FullName ?? user.UserName,
                            TaiKhoanTxt = taiKhoan.Code,
                            NoiDung = GiaoDichtbl.NoiDung,
                            SoTien = GiaoDichtbl.SoTien,
                            TenTaiKhoanCanNap = GiaoDichtbl.TenTaiKhoanCanNap,
                            MatKhauTaiKhoanNap = GiaoDichtbl.MatKhauTaiKhoanNap,
                            NoiDungChuyenKhoan = GiaoDichtbl.NoiDungChuyenKhoan,
                            MaGiaoDich = GiaoDichtbl.MaGiaoDich,
                        };
            query = query.Where(x => x.Id == id);
            return query.FirstOrDefault();
        }

        public GiaoDich GetById(long id)
        {
            return _GiaoDichRepository.GetById(id);
        }

        public List<TopNapTheVM> GetTopNapTheThang(int top = 5)
        {
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            var query = _GiaoDichRepository.GetAllAsQueryable()
                   .Where(x => x.TrangThai == TrangThaiGiaoDichConstant.DATHANHTOAN && x.CreatedDate >= startOfMonth && x.CreatedDate < endOfMonth)
                   .GroupBy(x => x.CreatedBy)
                   .Select(g => new TopNapTheVM
                   {
                       UserName = g.Key,
                       TongSoTien = g.Sum(x => x.SoTien)
                   })
                   .OrderByDescending(x => x.TongSoTien)
                   .Take(top)
                   .ToList();

            return query;
        }
    }
}
