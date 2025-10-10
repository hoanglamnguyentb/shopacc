using log4net;
using Hinet.Model.IdentityEntities;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhMucGameTaiKhoanRepository;
using Hinet.Service.DanhMucGameTaiKhoanService.Dto;
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




namespace Hinet.Service.DanhMucGameTaiKhoanService
{
    public class DanhMucGameTaiKhoanService : EntityService<DanhMucGameTaiKhoan>, IDanhMucGameTaiKhoanService
    {
        IUnitOfWork _unitOfWork;
        IDanhMucGameTaiKhoanRepository _DanhMucGameTaiKhoanRepository;
        ILog _loger;
        IMapper _mapper;



        public DanhMucGameTaiKhoanService(IUnitOfWork unitOfWork,
        IDanhMucGameTaiKhoanRepository DanhMucGameTaiKhoanRepository,
        ILog loger,

                IMapper mapper
            )
            : base(unitOfWork, DanhMucGameTaiKhoanRepository)
        {
            _unitOfWork = unitOfWork;
            _DanhMucGameTaiKhoanRepository = DanhMucGameTaiKhoanRepository;
            _loger = loger;
            _mapper = mapper;
        }

        public PageListResultBO<DanhMucGameTaiKhoanDto> GetDaTaByPage(DanhMucGameTaiKhoanSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from DanhMucGameTaiKhoantbl in _DanhMucGameTaiKhoanRepository.GetAllAsQueryable()

                        select new DanhMucGameTaiKhoanDto
                        {
                            DanhMucGameId = DanhMucGameTaiKhoantbl.DanhMucGameId,
                            TaiKhoanId = DanhMucGameTaiKhoantbl.TaiKhoanId,
                            CreatedDate = DanhMucGameTaiKhoantbl.CreatedDate,
                            CreatedBy = DanhMucGameTaiKhoantbl.CreatedBy,
                            CreatedID = DanhMucGameTaiKhoantbl.CreatedID,
                            UpdatedDate = DanhMucGameTaiKhoantbl.UpdatedDate,
                            UpdatedBy = DanhMucGameTaiKhoantbl.UpdatedBy,
                            UpdatedID = DanhMucGameTaiKhoantbl.UpdatedID,
                            IsDelete = DanhMucGameTaiKhoantbl.IsDelete,
                            DeleteTime = DanhMucGameTaiKhoantbl.DeleteTime,
                            DeleteId = DanhMucGameTaiKhoantbl.DeleteId,
                            Id = DanhMucGameTaiKhoantbl.Id

                        };

            if (searchModel != null)
            {
                if (searchModel.DanhMucGameIdFilter != null)
                {
                    query = query.Where(x => x.DanhMucGameId == searchModel.DanhMucGameIdFilter);
                }
                if (searchModel.TaiKhoanIdFilter != null)
                {
                    query = query.Where(x => x.TaiKhoanId == searchModel.TaiKhoanIdFilter);
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
            var resultmodel = new PageListResultBO<DanhMucGameTaiKhoanDto>();
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

        public DanhMucGameTaiKhoan GetById(long id)
        {
            return _DanhMucGameTaiKhoanRepository.GetById(id);
        }


        public void DeleteByTaiKhoanId(long taiKhoanId) 
        {
            var list = _DanhMucGameTaiKhoanRepository.GetAllAsQueryable()
                .Where(x => x.TaiKhoanId == taiKhoanId).ToList();
            _DanhMucGameTaiKhoanRepository.DeleteRange(list);
            _DanhMucGameTaiKhoanRepository.Save();
        } 

        public List<DanhMucGameTaiKhoan> GetByTaiKhoanId(long taiKhoanId)
        {
            return _DanhMucGameTaiKhoanRepository.GetAllAsQueryable()
                .Where(x => x.TaiKhoanId == taiKhoanId).ToList();
        }
    }
}
