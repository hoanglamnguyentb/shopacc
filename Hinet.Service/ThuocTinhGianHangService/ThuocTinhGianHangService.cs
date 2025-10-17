using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhmucRepository;
using Hinet.Repository.ThuocTinhGianHangRepository;
using Hinet.Service.Common;
using Hinet.Service.ThuocTinhGianHangService.Dto;
using Hinet.Service.ThuocTinhService.Dto;
using log4net;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;




namespace Hinet.Service.ThuocTinhGianHangService
{
    public class ThuocTinhGianHangService : EntityService<ThuocTinhGianHang>, IThuocTinhGianHangService
    {
        IUnitOfWork _unitOfWork;
        IThuocTinhGianHangRepository _ThuocTinhGianHangRepository;
        ILog _loger;
        IMapper _mapper;
        IDM_DulieuDanhmucRepository _dM_DulieuDanhmucRepository;

        public ThuocTinhGianHangService(IUnitOfWork unitOfWork,
                IThuocTinhGianHangRepository ThuocTinhGianHangRepository,
                ILog loger,
                IMapper mapper,
                IDM_DulieuDanhmucRepository dM_DulieuDanhmucRepository)
            : base(unitOfWork, ThuocTinhGianHangRepository)
        {
            _unitOfWork = unitOfWork;
            _ThuocTinhGianHangRepository = ThuocTinhGianHangRepository;
            _loger = loger;
            _mapper = mapper;
            _dM_DulieuDanhmucRepository = dM_DulieuDanhmucRepository;
        }

        public PageListResultBO<ThuocTinhGianHangDto> GetDaTaByPage(ThuocTinhGianHangSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from ThuocTinhGianHangtbl in _ThuocTinhGianHangRepository.GetAllAsQueryable()

                        select new ThuocTinhGianHangDto
                        {
                            GianHangId = ThuocTinhGianHangtbl.GianHangId,
                            NhomDanhMucId = ThuocTinhGianHangtbl.NhomDanhMucId,
                            TenThuocTinh = ThuocTinhGianHangtbl.TenThuocTinh,
                            KieuDuLieu = ThuocTinhGianHangtbl.KieuDuLieu,
                            NhomDanhmucCode = ThuocTinhGianHangtbl.NhomDanhmucCode,
                            CreatedBy = ThuocTinhGianHangtbl.CreatedBy,
                            UpdatedBy = ThuocTinhGianHangtbl.UpdatedBy,
                            Id = ThuocTinhGianHangtbl.Id,
                            CreatedID = ThuocTinhGianHangtbl.CreatedID,
                            UpdatedID = ThuocTinhGianHangtbl.UpdatedID,
                            DeleteId = ThuocTinhGianHangtbl.DeleteId,
                            CreatedDate = ThuocTinhGianHangtbl.CreatedDate,
                            UpdatedDate = ThuocTinhGianHangtbl.UpdatedDate,
                            DeleteTime = ThuocTinhGianHangtbl.DeleteTime,
                            IsDelete = ThuocTinhGianHangtbl.IsDelete

                        };

            if (searchModel != null)
            {
                if (searchModel.GianHangIdFilter != null)
                {
                    query = query.Where(x => x.GianHangId == searchModel.GianHangIdFilter);
                }
                if (searchModel.NhomDanhMucIdFilter != null)
                {
                    query = query.Where(x => x.NhomDanhMucId == searchModel.NhomDanhMucIdFilter);
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
            var resultmodel = new PageListResultBO<ThuocTinhGianHangDto>();
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

        public ThuocTinhGianHang GetById(long id)
        {
            return _ThuocTinhGianHangRepository.GetById(id);
        }

        public void DeleteByGianHangId(long gianHangId)
        {
            var list = _ThuocTinhGianHangRepository.GetQueryable().Where(x => x.GianHangId == gianHangId);
            _ThuocTinhGianHangRepository.DeleteRange(list);
            _ThuocTinhGianHangRepository.Save();
        }

        public List<ThuocTinhGianHangDto> GetDaTaByGianHangId(int gianHangId)
        {
            var queryDanhMuc = _dM_DulieuDanhmucRepository.GetAllAsQueryable();

            var query = from ThuocTinhtbl in _ThuocTinhGianHangRepository.GetQueryable().Where(x => x.GianHangId == gianHangId)

                        select new ThuocTinhGianHangDto
                        {
                            GianHangId = ThuocTinhtbl.GianHangId,
                            TenThuocTinh = ThuocTinhtbl.TenThuocTinh,
                            KieuDuLieu = ThuocTinhtbl.KieuDuLieu,
                            NhomDanhmucCode = ThuocTinhtbl.NhomDanhmucCode,
                            NhomDanhMucId = ThuocTinhtbl.NhomDanhMucId,
                            Id = ThuocTinhtbl.Id,
                            ListDuLieuDanhMuc = queryDanhMuc.Where(x => x.GroupId == ThuocTinhtbl.NhomDanhMucId).ToList(),
                            IsRequired = ThuocTinhtbl.IsRequired,
                            PlaceHolder = ThuocTinhtbl.PlaceHolder,
                        };
            return query.ToList();
        }
    }
}
