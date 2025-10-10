using AutoMapper;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhmucRepository;
using Hinet.Service.Common;
using Hinet.Service.DM_NhomDanhmucService.DTO;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace Hinet.Service.DM_NhomDanhmucService
{
    public class DM_NhomDanhmucService : EntityService<DM_NhomDanhmuc>, IDM_NhomDanhmucService
    {
        private IUnitOfWork _unitOfWork;
        private IDM_NhomDanhmucRepository _dM_NhomDanhmucRepository;
        private IDM_DulieuDanhmucRepository categoryDataRepository;
        private ILog _loger;
        private IMapper mapper;

        public DM_NhomDanhmucService(IUnitOfWork unitOfWork,
            IDM_NhomDanhmucRepository dmNhomDanhmucRepository,
            IDM_DulieuDanhmucRepository categoryDataRepository,
            IMapper mapper,
        ILog loger) : base(unitOfWork, dmNhomDanhmucRepository)
        {
            _unitOfWork = unitOfWork;
            _dM_NhomDanhmucRepository = dmNhomDanhmucRepository;
            this.categoryDataRepository = categoryDataRepository;
            this.mapper = mapper;
            _loger = loger;
        }

        public PageListResultBO<DM_NhomDanhmucDTO> GetDataByPage(DM_NhomDanhmucSearchDTO searchParams, int pageIndex = 1, int pageSize = 10)
        {
            var query = (from nhomDanhmuc in this._dM_NhomDanhmucRepository.GetAllAsQueryable()
                         select new DM_NhomDanhmucDTO()
                         {
                             Id = nhomDanhmuc.Id,
                             GroupCode = nhomDanhmuc.GroupCode,
                             GroupName = nhomDanhmuc.GroupName
                         });
            if (searchParams != null)
            {
                if (!String.IsNullOrEmpty(searchParams.QueryCode))
                {
                    query = query.Where(x => x.GroupCode.Contains(searchParams.QueryCode));
                }
                if (!String.IsNullOrEmpty(searchParams.QueryName))
                {
                    query = query.Where(x => x.GroupName.Contains(searchParams.QueryName));
                }

                if (!string.IsNullOrEmpty(searchParams.sortQuery))
                {
                    query = query.OrderBy(searchParams.sortQuery);
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

            var result = new PageListResultBO<DM_NhomDanhmucDTO>();
            if (pageSize == -1)
            {
                var pagedList = query.ToList();
                result.Count = pagedList.Count;
                result.TotalPage = 1;
                result.ListItem = pagedList;
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                result.Count = dataPageList.TotalItemCount;
                result.TotalPage = dataPageList.PageCount;
                result.ListItem = dataPageList.ToList();
            }
            return result;
        }

        public bool CheckGroupCodeExisted(string groupCode)
        {
            return this._dM_NhomDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupCode.Equals(groupCode)).Any();
        }

        /// <summary>
        /// @author:duynn
        /// @description: lấy danh mục bằng nhóm
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetDataByCode(string code)
        {
            var result = (from data in this.categoryDataRepository.GetAllAsQueryable()
                          join groupData in this._dM_NhomDanhmucRepository.GetAllAsQueryable()
                          on data.GroupId equals groupData.Id
                          where groupData.GroupCode == code
                          select data).AsEnumerable()
                          .Select(data => mapper.Map<DM_DulieuDanhmuc, SelectListItem>(data)).ToList();
            return result;
        }

        /// <summary>
        /// Lấy dữ liệu theo mã loại
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<DM_DulieuDanhmuc> GetByCode(string code)
        {
            var result = (from data in this.categoryDataRepository.GetAllAsQueryable()
                          join groupData in this._dM_NhomDanhmucRepository.GetAllAsQueryable()
                          on data.GroupId equals groupData.Id
                          where groupData.GroupCode == code
                          select data).ToList();
            return result;
        }/// <summary>

         /// Lấy ra id của loại danh mục
         /// </summary>
         /// <param name="groupCode">Code của loại danh mục</param>
         /// <returns></returns>
        public long GetIdByGroupCode(string groupCode)
        {
            var result = (from data in _dM_NhomDanhmucRepository.GetAllAsQueryable()
                          where data.GroupCode.Contains(groupCode)
                          select data.Id).FirstOrDefault();
            return result;
        }

        public DM_NhomDanhmuc GetNhomDanhMucByGroupCode(string groupCode)
        {
            return _dM_NhomDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupCode.Equals(groupCode)).FirstOrDefault();
        }
    }
}