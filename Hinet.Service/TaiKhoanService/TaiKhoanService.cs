using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhMucGameRepository;
using Hinet.Repository.DanhMucGameTaiKhoanRepository;
using Hinet.Repository.TaiKhoanRepository;
using Hinet.Service.Common;
using Hinet.Service.TaiKhoanService.Dto;
using log4net;
using PagedList;
using System;
using System.Linq;
using System.Linq.Dynamic;




namespace Hinet.Service.TaiKhoanService
{
    public class TaiKhoanService : EntityService<TaiKhoan>, ITaiKhoanService
    {
        IUnitOfWork _unitOfWork;
        ITaiKhoanRepository _TaiKhoanRepository;
        ILog _loger;
        IMapper _mapper;
        IDanhMucGameTaiKhoanRepository _danhMucGameTaiKhoanRepository;
        IDanhMucGameRepository _danhMucGameRepository;

        public TaiKhoanService(IUnitOfWork unitOfWork,
                ITaiKhoanRepository TaiKhoanRepository,
                ILog loger,
                IMapper mapper,
                IDanhMucGameTaiKhoanRepository danhMucGameTaiKhoanRepository,
                IDanhMucGameRepository danhMucGameRepository)
            : base(unitOfWork, TaiKhoanRepository)
        {
            _unitOfWork = unitOfWork;
            _TaiKhoanRepository = TaiKhoanRepository;
            _loger = loger;
            _mapper = mapper;
            _danhMucGameTaiKhoanRepository = danhMucGameTaiKhoanRepository;
            _danhMucGameRepository = danhMucGameRepository;
        }

        public PageListResultBO<TaiKhoanDto> GetDaTaByPage(TaiKhoanSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var danhMucQuery = _danhMucGameRepository.GetAllAsQueryable();

            var query = from tk in _TaiKhoanRepository.GetAllAsQueryable()
                        join map in _danhMucGameTaiKhoanRepository.GetAllAsQueryable()
                            on tk.Id equals map.TaiKhoanId into mapGrp
                        select new TaiKhoanDto
                        {
                            Id = tk.Id,
                            Code = tk.Code,
                            GameId = tk.GameId,
                            TrangThai = tk.TrangThai,
                            UserName = tk.UserName,
                            Password = tk.Password,
                            GiaGoc = tk.GiaGoc,
                            GiaKhuyenMai = tk.GiaKhuyenMai,
                            Mota = tk.Mota,
                            ViTri = tk.ViTri,
                            CreatedDate = tk.CreatedDate,
                            CreatedBy = tk.CreatedBy,
                            CreatedID = tk.CreatedID,
                            UpdatedDate = tk.UpdatedDate,
                            UpdatedBy = tk.UpdatedBy,
                            UpdatedID = tk.UpdatedID,
                            IsDelete = tk.IsDelete,
                            DeleteTime = tk.DeleteTime,
                            DeleteId = tk.DeleteId,

                            // Dùng subquery join danh mục game với mapping
                            ListDanhMucGame = (from m in mapGrp
                                               join dm in danhMucQuery
                                                   on m.DanhMucGameId equals dm.Id
                                               select dm).ToList()
                        };

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.CodeFilter))
                {
                    query = query.Where(x => x.Code.Contains(searchModel.CodeFilter));
                }
                if (searchModel.GameIdFilter != null)
                {
                    query = query.Where(x => x.GameId == searchModel.GameIdFilter);
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
                if (searchModel.GiaGocFilter != null)
                {
                    query = query.Where(x => x.GiaGoc == searchModel.GiaGocFilter);
                }
                if (searchModel.GiaKhuyenMaiFilter != null)
                {
                    query = query.Where(x => x.GiaKhuyenMai == searchModel.GiaKhuyenMaiFilter);
                }
                if (!string.IsNullOrEmpty(searchModel.MotaFilter))
                {
                    query = query.Where(x => x.Mota.Contains(searchModel.MotaFilter));
                }
                if (searchModel.ViTriFilter != null)
                {
                    query = query.Where(x => x.ViTri == searchModel.ViTriFilter);
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
