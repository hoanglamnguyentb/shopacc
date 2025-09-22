using Hinet.Model.Entities;
using Hinet.Service.Common;
using Hinet.Service.DM_DulieuDanhmucService.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hinet.Service.DM_DulieuDanhmucService
{
    public interface IDM_DulieuDanhmucService : IEntityService<DM_DulieuDanhmuc>
    {
        PageListResultBO<DM_DulieuDanhmucDTO> GetDataByPage(long danhMucId, DM_DulieuDanhmucSearchDTO searchParams, int pageIndex = 1, int pageSize = 10);

        List<DM_DulieuDanhmuc> GetListDataByGroupId(long groupId);

        bool CheckCodeExisted(long? groupId, string code);

        List<DM_DulieuDanhmuc> GetByCodeGroup(string GroupCode);

        List<SelectListItem> GetDropdownlistValueId(string GroupCode, string SelectedValue);

        List<SelectListItem> GetDropdownlist(string GroupCode, string SelectedValue);

        List<SelectListItem> GetDropdownlistID(string GroupCode, long? SelectedValue);

        //List<SelectListItem> EndUserGetDropdownlist(long UserId);
        List<SelectListItem> GetDropdownlistByCountry(long danhMucId, string GroupCode, string SelectedValue);

        DM_DulieuDanhmuc GetByIdName(string GroupName, string Code);

        List<SelectListItem> GetDropdownlistByGhiChu(string grpCode, string GroupCodeGhiChu, string SelectedValue);

        List<SelectListItem> GetDropdownByGroupId(long GroupId);

        List<SelectListItem> GetDropdownByGroupId(long GroupId, string selectedValue);

        string GetNameByCodeAndGroupId(string GroupCode, long GroupId);

        List<SelectListItem> GetDropDownListByCodeGroup(string GroupCode, string selected = null);

        List<SelectListItem> GetDropDownListByGroup(string GroupCode, long? selected = 0);

        string GetNameCode(string Code, string GroupCode);

        List<DM_DulieuDanhmuc> getByLstId(List<long> lst);

        List<long> GetLstIdByCode(List<string> lst, string groupCode);

        List<SelectListItem> GetDropdownlistIDMulti(string GroupCode, List<long> SelectedValue);

        string GetName(string code);

        string GetCode(string Name);

        List<string> ListName(string groupCode, bool hasAdd = false);

        List<SelectListItem> GetDropdownlistCode(string GroupCode, string SelectedValue);

        PageListResultBO<SelectListItem> GetDataToShowImportCategory(ShowValueTableSVM searchModel);

        PageListResultBO<DM_QuanTri_DulieuDanhmucDTO> GetData_QuanTri_ByPage(DM_QuanTri_DulieuDanhmucSearchDTO searchParams, int pageIndex = 1, int pageSize = 20);

        CauHinhHeThong GetCauHinhHeThong();

        List<SelectListItem> GetNoteByCode(string code);

        List<DM_DulieuDanhmuc> GetListByGroupCode(string GroupCode);

        List<SelectListItem> GetDropDownListByCodeGroupShowCode(string GroupCode, string selected = null);
        List<SelectListItem> GetDropdownlistMultiValue(string GroupCode, List<string> SelectedValue);
    }
}