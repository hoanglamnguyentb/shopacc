using CommonHelper.String;
using Hinet.Model.Common;
using Hinet.Model.Entities;
using Hinet.Repository;
using Hinet.Repository.DanhmucRepository;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService.DTO;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using static Hinet.Service.Common.Constant;

namespace Hinet.Service.DM_DulieuDanhmucService
{
    public class DM_DulieuDanhmucService : EntityService<DM_DulieuDanhmuc>, IDM_DulieuDanhmucService
    {
        private IUnitOfWork _unitOfWork;
        private IDM_DulieuDanhmucRepository _dM_DulieuDanhmucRepository;
        private IDM_NhomDanhmucRepository _nhomDanhmucRepository;
        private ILog _loger;

        public DM_DulieuDanhmucService(IUnitOfWork unitOfWork, IDM_DulieuDanhmucRepository dM_DulieuDanhmucRepository, IDM_NhomDanhmucRepository nhomDanhmucRepository, ILog loger) : base(unitOfWork, dM_DulieuDanhmucRepository)
        {
            _unitOfWork = unitOfWork;
            _dM_DulieuDanhmucRepository = dM_DulieuDanhmucRepository;
            _nhomDanhmucRepository = nhomDanhmucRepository;
            _loger = loger;
        }

        /// <summary>
        /// Lấy danh sách theo tên
        /// </summary>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public List<DM_DulieuDanhmuc> GetByCodeGroup(string GroupCode)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<DM_DulieuDanhmuc>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).OrderBy(x => x.Priority).ToList();
            return listData;
        }

        /// <summary>
        /// Lấy danh sách dropdown
        /// </summary>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public List<SelectListItem> GetDropdownlist(string GroupCode, string SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Code,
                    Selected = SelectedValue == x.Code
                }).ToList();

            return listData;
        }

        public List<SelectListItem> GetDropdownlistByGhiChu(string grpCode, string GroupCodeGhiChu, string SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(grpCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.Note == GroupCodeGhiChu).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Code,
                    Selected = SelectedValue == x.Code
                }).ToList();

            return listData;
        }

        public List<SelectListItem> GetDropdownlistValueId(string GroupCode, string SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = SelectedValue == x.Id.ToString()
                }).ToList();

            return listData;
        }

        public List<SelectListItem> GetDropdownlistID(string GroupCode, long? SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = SelectedValue == x.Id
                }).ToList();

            return listData;
        }

        public List<SelectListItem> GetDropdownlistCode(string GroupCode, string SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Code,
                    Selected = SelectedValue == x.Code
                }).ToList();

            return listData;
        }

        public List<SelectListItem> GetDropdownlistIDMulti(string GroupCode, List<long> SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList();
            listData.ForEach(x => x.Selected = SelectedValue != null ? SelectedValue.Contains(x.Value.ToLongOrZero()) : false);
            return listData;
        }
        
        public List<SelectListItem> GetDropdownlistMultiValue(string GroupCode, List<string> SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Code,

                }).ToList();
            listData.ForEach(x => x.Selected = SelectedValue != null ? SelectedValue.Contains(x.Value) : false);
            return listData;
        }
        public List<SelectListItem> GetDropdownlistByCountry(long danhMucId, string GroupCode, string SelectedValue)
        {
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(GroupCode)).FirstOrDefault();
            if (group == null)
            {
                return new List<SelectListItem>();
            }
            var listData = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).
                Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Code,
                    Selected = SelectedValue == x.Code
                }).ToList();

            return listData;
        }

        public PageListResultBO<SelectListItem> GetDataToShowImportCategory(ShowValueTableSVM searchModel)
        {
            var result = new PageListResultBO<SelectListItem>();
            var lstWhereCondition = new List<string>();
            if (!string.IsNullOrEmpty(searchModel.GiaTriNhapFilter))
            {
                lstWhereCondition.Add(string.Format("{0} like N'%{1}%'", searchModel.Value, searchModel.GiaTriNhapFilter));
            }

            if (!string.IsNullOrEmpty(searchModel.GiaTriHienThiFilter))
            {
                lstWhereCondition.Add(string.Format("{0} like N'%{1}%'", searchModel.Text, searchModel.GiaTriHienThiFilter));
            }

            var stringQuery = lstWhereCondition.Any() ? " where " + string.Join(" AND ", lstWhereCondition) : "";
            var skipcount = (searchModel.pageIndex - 1) * searchModel.pageSize;
            var querySelect = $"select {searchModel.Value} as Value,{searchModel.Text} as Text from {searchModel.TableName} {stringQuery} order by Id asc offset {skipcount} rows fetch next {searchModel.pageSize} row only";
            var queryCount = $"select count(*) from {searchModel.TableName} {stringQuery}";
            int countRow = 0;
            if (searchModel.Value.ToUpper() == "ID")
            {
                var lstItem = this._unitOfWork.Context().Database.SqlQuery<ShowKeyValueModelNumber>(querySelect).ToList();
                countRow = this._unitOfWork.Context().Database.SqlQuery<int>(queryCount).FirstOrDefault();
                result.ListItem = lstItem.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value.ToString()
                }).ToList();
            }
            else
            {
                var lstItem = this._unitOfWork.Context().Database.SqlQuery<ShowKeyValueModelTxt>(querySelect).ToList();
                countRow = this._unitOfWork.Context().Database.SqlQuery<int>(queryCount).FirstOrDefault();
                result.ListItem = lstItem.Select(x => new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                }).ToList();
            }

            result.Count = countRow;
            result.CurrentPage = searchModel.pageIndex;
            result.TotalPage = countRow.GetTotalPage(searchModel.pageSize);
            return result;
        }

        public PageListResultBO<DM_DulieuDanhmucDTO> GetDataByPage(long danhMucId, DM_DulieuDanhmucSearchDTO searchParams, int pageIndex = 1, int pageSize = 10)
        {
            var query = (from dulieuDanhmuc in this._dM_DulieuDanhmucRepository.GetAllAsQueryable()
                         where dulieuDanhmuc.GroupId == danhMucId
                         select new DM_DulieuDanhmucDTO()
                         {
                             Id = dulieuDanhmuc.Id,
                             Code = dulieuDanhmuc.Code,
                             Name = dulieuDanhmuc.Name,
                             Note = dulieuDanhmuc.Note,
                             Priority = dulieuDanhmuc.Priority
                         });
            if (searchParams != null)
            {
                if (!string.IsNullOrEmpty(searchParams.sortQuery))
                {
                    query = query.OrderBy(searchParams.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }
                if (!String.IsNullOrEmpty(searchParams.QueryCode))
                {
                    query = query.Where(x => x.Code.Contains(searchParams.QueryCode));
                }
                if (!String.IsNullOrEmpty(searchParams.QueryName))
                {
                    query = query.Where(x => x.Name.Contains(searchParams.QueryName));
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }

            var result = new PageListResultBO<DM_DulieuDanhmucDTO>();
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

        public List<DM_DulieuDanhmuc> GetListDataByGroupId(long groupId)
        {
            return this._dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == groupId).ToList();
        }

        public List<DM_DulieuDanhmuc> getByLstId(List<long> lst)
        {
            if (lst != null)
            {
                return _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => lst.Contains(x.Id)).ToList();
            }
            else
            {
                return new List<DM_DulieuDanhmuc>();
            }
        }

        public List<long> GetLstIdByCode(List<string> lst, string groupCode)
        {
            var group = _nhomDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupCode == groupCode).FirstOrDefault();

            if (lst != null)
            {
                return _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id && lst.Contains(x.Code)).Select(x => x.Id).ToList();
            }
            else
            {
                return new List<long>();
            }
        }

        public bool CheckCodeExisted(long? groupId, string code)
        {
            return this._dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == groupId && x.Code.Equals(code)).Any();
        }

        public DM_DulieuDanhmuc GetByIdName(string GroupName, string Code)
        {
            var resultmodel = new DM_DulieuDanhmuc();
            var groupNameQuery = _nhomDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupCode == GroupName).FirstOrDefault();
            if (groupNameQuery != null)
            {
                var query = from DM_DulieuDanhmuctbl in _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.Code == Code && x.GroupId == groupNameQuery.Id)
                            select DM_DulieuDanhmuctbl;
                resultmodel = query.FirstOrDefault();
                if (resultmodel == null)
                {
                    resultmodel = new DM_DulieuDanhmuc();
                    resultmodel.Name = "Không tìm thấy dữ liệu";
                }
            }
            else
            {
                resultmodel = new DM_DulieuDanhmuc();
                resultmodel.Name = "Không tìm thấy dữ liệu";
            }
            return resultmodel;
        }

        public List<SelectListItem> GetDropdownByGroupId(long GroupId)
        {
            var query = (from Categories in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                         where Categories.GroupId == GroupId
                         select new DM_DulieuDanhmucDTO
                         {
                             Code = Categories.Code,
                             Name = Categories.Name,
                             GroupId = Categories.GroupId,
                             Id = Categories.Id
                         }).ToList();
            var result = new List<SelectListItem>();

            foreach (var item in query)
            {
                result.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Code
                });
            }
            return result;
        }

        public List<SelectListItem> GetDropdownByGroupId(long GroupId, string selectedValue)
        {
            var query = (from Categories in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                         where Categories.GroupId == GroupId
                         select new DM_DulieuDanhmucDTO
                         {
                             Code = Categories.Code,
                             Name = Categories.Name,
                             GroupId = Categories.GroupId,
                             Id = Categories.Id
                         }).ToList();
            var result = new List<SelectListItem>();

            foreach (var item in query)
            {
                result.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Code,
                    Selected = selectedValue == item.Code
                });
            }
            return result;
        }

        public string GetNameByCodeAndGroupId(string GroupCode, long GroupId)
        {
            var query = (from Categories in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                         where Categories.GroupId == GroupId && Categories.Code == GroupCode
                         select new
                         {
                             Categories.Name
                         }).FirstOrDefault();
            return query.Name;
        }

        public List<SelectListItem> GetDropDownListByCodeGroup(string GroupCode, string selected = null)
        {
            var groupObj = (_nhomDanhmucRepository.GetAll().Where(x => x.GroupCode == GroupCode)).FirstOrDefault();
            long groupCodeId = groupObj.Id;
            var result = (from datatbl in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                          where datatbl.GroupId == groupCodeId
                          select new SelectListItem
                          {
                              Text = datatbl.Name,
                              Value = datatbl.Code,
                              Selected = !string.IsNullOrEmpty(selected) && datatbl.Code.Equals(selected)
                          }).ToList();
            return result;
        }

        public List<SelectListItem> GetDropDownListByCodeGroupShowCode(string GroupCode, string selected = null)
        {
            var groupObj = (_nhomDanhmucRepository.GetAll().Where(x => x.GroupCode == GroupCode)).FirstOrDefault();
            long groupCodeId = groupObj.Id;
            var result = (from datatbl in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                          where datatbl.GroupId == groupCodeId
                          select new SelectListItem
                          {
                              Text = datatbl.Name + " (" + datatbl.Note + ")",
                              Value = datatbl.Code,
                              Selected = !string.IsNullOrEmpty(selected) && datatbl.Code.Equals(selected)
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Lấy tên của mã danh mục theo mã danh mục và mã nhomd danh mục
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public string GetNameCode(string Code, string GroupCode)
        {
            var groupCodeId = _nhomDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupCode == GroupCode).Select(x => x.Id).FirstOrDefault();
            var query = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == groupCodeId && x.Code == Code).Select(x => x.Name).FirstOrDefault();
            return query;
        }

        public List<SelectListItem> GetDropDownListByGroup(string GroupCode, long? selected = 0)
        {
            var groupObj = (_nhomDanhmucRepository.GetAll().Where(x => x.GroupCode == GroupCode)).FirstOrDefault();
            if (groupObj != null)
            {
                long groupCodeId = groupObj.Id;
                var result = (from datatbl in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                              where datatbl.GroupId == groupCodeId
                              select new SelectListItem
                              {
                                  Text = datatbl.Name,
                                  Value = datatbl.Id.ToString(),
                                  Selected = (datatbl.Id == selected)
                              }).ToList();
                return result;
            }
            return new List<SelectListItem>();
        }

        public string GetName(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var search = _dM_DulieuDanhmucRepository.FindBy(x => x.Code == code).FirstOrDefault();
                if (search != null)
                {
                    return search.Name;
                }
            }

            return string.Empty;
        }

        public string GetCode(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var search = _dM_DulieuDanhmucRepository.FindBy(x => x.Name.Equals(Name)).FirstOrDefault();
                if (search != null)
                {
                    return search.Code;
                }
            }

            return string.Empty;
        }

        public List<string> ListName(string GroupCode, bool hasAdd = false)
        {
            if (!string.IsNullOrEmpty(GroupCode))
            {
                var groupObj = (_nhomDanhmucRepository.GetAll().Where(x => x.GroupCode == GroupCode)).FirstOrDefault();
                long groupCodeId = groupObj.Id;
                var result = (from datatbl in _dM_DulieuDanhmucRepository.GetAllAsQueryable()
                              where datatbl.GroupId == groupCodeId
                              select datatbl.Name).OrderBy(x => x).ToList();
                if (hasAdd)
                {
                    result.Add("Chưa rõ");
                }
                return result;
            }
            return new List<string>();
        }

        public PageListResultBO<DM_QuanTri_DulieuDanhmucDTO> GetData_QuanTri_ByPage(DM_QuanTri_DulieuDanhmucSearchDTO searchParams, int pageIndex = 1, int pageSize = 20)
        {
            var query = (from dulieuDanhmuc in this._dM_DulieuDanhmucRepository.GetAllAsQueryable()
                         join nhomDanhMuc in _nhomDanhmucRepository.GetAllAsQueryable() on dulieuDanhmuc.GroupId equals nhomDanhMuc.Id
                         select new DM_QuanTri_DulieuDanhmucDTO()
                         {
                             Id = dulieuDanhmuc.Id,
                             Code = dulieuDanhmuc.Code,
                             Name = dulieuDanhmuc.Name,
                             Note = dulieuDanhmuc.Note,
                             Priority = dulieuDanhmuc.Priority,
                             TenNhomDuLieu = nhomDanhMuc.GroupName,
                             GroupId = nhomDanhMuc.Id,
                             GroupCode = nhomDanhMuc.GroupCode
                         });
            if (searchParams != null)
            {
                if (!string.IsNullOrEmpty(searchParams.sortQuery))
                {
                    query = query.OrderBy(searchParams.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }
                if (!String.IsNullOrEmpty(searchParams.QueryCode))
                {
                    query = query.Where(x => x.Code.Contains(searchParams.QueryCode));
                }
                if (searchParams.IdNhomDuLieuFilter != null)
                {
                    query = query.Where(x => x.GroupId == searchParams.IdNhomDuLieuFilter);
                }

                if (!String.IsNullOrEmpty(searchParams.QueryName))
                {
                    query = query.Where(x => x.Name.Contains(searchParams.QueryName));
                }
            }
            query = query.OrderBy(x => x.GroupId);

            var result = new PageListResultBO<DM_QuanTri_DulieuDanhmucDTO>();
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

        public CauHinhHeThong GetCauHinhHeThong()
        {
            var groupCode = CAUHINH_HETHONG_CONSTANT.CAUHINH_HETHONG;
            var group = _nhomDanhmucRepository.FindBy(x => x.GroupCode.Equals(groupCode)).FirstOrDefault() ?? new
                DM_NhomDanhmuc();
            var listCauHinhHeThong = _dM_DulieuDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupId == group.Id).OrderBy(x => x.Priority)
                .ToList();

            var entityCauHinhHeThong = new CauHinhHeThong()
            {
                TenHeThong = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.TEN_HETHONG).Select(x => x.Name).FirstOrDefault(),
                MaTinh = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.MA_TINH).Select(x => x.Name).FirstOrDefault(),
                TenDonVi = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.TEN_DONVI).Select(x => x.Name).FirstOrDefault(),
                TenTinh = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.TEN_TINH).Select(x => x.Name).FirstOrDefault(),
                Email = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.EMAIL).Select(x => x.Name).FirstOrDefault(),
                Fax = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.FAX).Select(x => x.Name).FirstOrDefault(),
                DienThoai = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.DIENTHOAI).Select(x => x.Name).FirstOrDefault(),
                DiaChiDonVi = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.DIACHI_DONVI).Select(x => x.Name).FirstOrDefault(),
                NamSanXuat = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.NAM_SANXUAT).Select(x => x.Name).FirstOrDefault(),
                TenMienTruyCap = listCauHinhHeThong.Where(x => x.Code == CAUHINH_HETHONG_CONSTANT.DIACHI_HETHONG).Select(x => x.Name).FirstOrDefault(),
            };
            return entityCauHinhHeThong;
        }

        public List<SelectListItem> GetNoteByCode(string code)
        {
            var groupObj = (_nhomDanhmucRepository.GetAll().Where(x => x.GroupCode == code)).FirstOrDefault();
            var data = _dM_DulieuDanhmucRepository.FindBy(x => x.GroupId == groupObj.Id);
            return data.Select(e => new SelectListItem()
            {
                Text = e.Note,
                Value = e.Code
            }).ToList();
        }

        public List<DM_DulieuDanhmuc> GetListByGroupCode(string GroupCode)
        {
            return _nhomDanhmucRepository.GetAllAsQueryable().Where(x => x.GroupCode == GroupCode).Join(_dM_DulieuDanhmucRepository.GetAllAsQueryable(), x => x.Id, y => y.GroupId, (x, y) => y).ToList();
        }




    }
}